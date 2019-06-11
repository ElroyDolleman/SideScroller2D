using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SideScroller2D.Code.StateManagement
{
    abstract class BaseState
    {
        public readonly int StateID;

        protected StateManager stateManager;


        public BaseState(StateManager stateManager)
        {
            this.stateManager = stateManager;

            StateID = stateManager.AddState(this);
        }

        public virtual void OnContentLoaded()
        {

        }

        public virtual void Update()
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
