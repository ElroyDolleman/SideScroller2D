using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.Graphics;
using SideScroller2D.Code.GameLogic.Player;
using SideScroller2D.Code.GameLogic.Player.PlayerStates;
using SideScroller2D.Code.GameLogic.Level;
using SideScroller2D.Code.Collision;
using SideScroller2D.Code.Particles;
using SideScroller2D.Code.Utilities;

namespace SideScroller2D.Code.StateManagement.States
{
    class GameState : BaseState
    {
        Player player;
        Level currentLevel;
        //ParticleSystem particleSystem;

        public GameState(StateManager stateManager)
            : base(stateManager)
        {
        }

        public override void OnContentLoaded()
        {
            player = new Player(PlayerIndex.One);
            player.Position = new Vector2(200 - 8, 300 - 48);

            currentLevel = LevelLoader.LoadLevel();

            //var dust = new Sprite(AssetsManager.GetTexture("player_dust_particles"));
            //dust.Crop = new Rectangle(32, 0, 16, 16);
            //dust.Origin = new Vector2(8, 8);

            //particleSystem = new ParticleSystem(dust, Vector2.One * 120);

            //particleSystem.MinEmitInterval = 0.5f;
            //particleSystem.MaxEmitInterval = 0.5f;

            //particleSystem.MinAcceleration = new Vector2(0, 1.5f);
            //particleSystem.MaxAcceleration = particleSystem.MinAcceleration;

            //particleSystem.MinDirection = MathHelper.ToRadians(180 - 45);
            //particleSystem.MaxDirection = MathHelper.ToRadians(180 + 45);

            //particleSystem.MinRotateSpeed = -1.5f;
            //particleSystem.MaxRotateSpeed = 1.5f;

            //particleSystem.Loop = true;

            //particleSystem.Play();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            var from = currentLevel.Grid.ToGridLocation(player.NextHitbox.Location.ToVector2() - Vector2.One * 2);
            var to = new Point(from.X + 1, from.Y + 1);

            var overlappingTiles = currentLevel.GetTiles(from, to);

            var result = CollisionManager.MoveEntity(player, overlappingTiles);

            player.OnCollision(result, overlappingTiles);
            player.UpdateInBounds();

            //particleSystem.Position = player.Position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentLevel.DrawBackground(spriteBatch);

#if DEBUG
            // Draw Colliding Tiles
            //var from = currentLevel.Grid.ToGridLocation(player.NextHitbox.Location.ToVector2() - Vector2.One * 2);
            //var to = new Point(from.X + 1, from.Y + 1);

            //for (int y = from.Y; y <= to.Y; y++)
            //{
            //    for (int x = from.X; x <= to.X; x++)
            //    {
            //        var tile = currentLevel.GetTile(x, y);

            //        if (tile == null)
            //            continue;

            //        tile.DrawFilledTile(spriteBatch, tile.Solid ? Color.Red * 0.6f : Color.Blue * 0.4f);
            //    }
            //}
#endif

            player.Draw(spriteBatch);

#if DEBUG
            // Draw Hitbox
            //spriteBatch.FillRectangle(player.Hitbox, Color.Black * 0.9f);
#endif

            //particleSystem.Update();
            //particleSystem.Draw(spriteBatch);

            currentLevel.DrawForeground(spriteBatch);
        }
    }
}