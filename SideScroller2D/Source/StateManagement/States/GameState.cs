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
using SideScroller2D.GameLogic.Map;

namespace SideScroller2D.StateManagement
{
    class GameState : BaseState
    {
        Player player;

        public GameState(StateManager stateManager)
            : base(stateManager)
        {
            
        }

        public override void OnContentLoaded()
        {
            player = new Player(PlayerIndex.One);
            player.Position = new Vector2(200 - 8, 300 - 16);

            LevelLoader.LoadLevel();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
        }
    }
}