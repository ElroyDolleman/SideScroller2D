using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Graphics;

namespace SideScroller2D.StateManagement
{
    class GameState : BaseState
    {
        SpriteSheet sprite;

        public GameState(StateManager stateManager)
            : base(stateManager)
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = new SpriteSheet(content.Load<Texture2D>("character_nina"), 16, 16);
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Vector2.One * 64);
        }
    }
}
