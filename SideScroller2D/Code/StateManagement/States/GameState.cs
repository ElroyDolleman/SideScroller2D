using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.Graphics;
using SideScroller2D.Code.Playable;
using SideScroller2D.Code.Levels;
using SideScroller2D.Code.Collision;
using SideScroller2D.Code.Graphics.Particles;
using SideScroller2D.Code.Utilities;

namespace SideScroller2D.Code.StateManagement.States
{
    class GameState : BaseState
    {
        Player player;
        Level currentLevel;

        public GameState(StateManager stateManager)
            : base(stateManager)
        {
        }

        public override void OnContentLoaded()
        {
            player = new Player(PlayerIndex.One, new Vector2(140-8, 300 - 48));

            currentLevel = LevelLoader.LoadLevel(LevelLoader.TestLevel02, LevelLoader.TilesetDefault);
        }

        public override void Update()
        {
            player.Update();

            var from = currentLevel.Grid.ToGridLocation(player.NextHitbox.Position - Vector2.One * 2);
            var to = new Point(from.X + 1, from.Y + 1);

            CollisionManager.MoveActor(player, currentLevel.GetColliders(from, to));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentLevel.DrawTiles(spriteBatch);

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
            // Draw Player Hitbox
            //spriteBatch.FillRectangle(player.Hitbox, Color.Black * 0.9f);
#endif
        }
    }
}