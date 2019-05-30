using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Graphics;
using SideScroller2D.GameLogic.Player;
using SideScroller2D.GameLogic.Player.PlayerStates;
using SideScroller2D.GameLogic.Map;
using SideScroller2D.Collision;

namespace SideScroller2D.StateManagement
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
            player = new Player(PlayerIndex.One);
            player.Position = new Vector2(200 - 8, 300 - 48);

            currentLevel = LevelLoader.LoadLevel();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            var from = currentLevel.Grid.ToGridLocation(player.Hitbox.Location.ToVector2());
            var to = currentLevel.Grid.ToGridLocation((player.Hitbox.Location + player.Hitbox.Size).ToVector2());

            for (int y = from.Y; y <= to.Y; y++)
            {
                for (int x = from.X; x <= to.X; x++)
                {
                    var tile = currentLevel.GetTile(x, y);

                    if (tile == null || !tile.Solid)
                        continue;

                    Vector2 newPos = CollisionManager.ResolveAABB(player, tile.Hitbox).ToVector2();

                    if (player.Position.Y > newPos.Y)
                        player.ChangeState(new IdleState(player));

                    player.Position = newPos;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentLevel.DrawBackground(spriteBatch);

#if DEBUG
            var from = currentLevel.Grid.ToGridLocation(player.Hitbox.Location.ToVector2());
            var to = currentLevel.Grid.ToGridLocation((player.Hitbox.Location + player.Hitbox.Size).ToVector2());

            for (int y = from.Y; y <= to.Y; y++)
            {
                for (int x = from.X; x <= to.X; x++)
                {
                    var tile = currentLevel.GetTile(x, y);

                    if (tile == null)
                        continue;

                    tile.DrawFilledTile(spriteBatch, tile.Solid ? Color.Red * 0.6f : Color.Blue * 0.4f);
                }
            }
#endif

            player.Draw(spriteBatch);

            currentLevel.DrawForeground(spriteBatch);
        }
    }
}