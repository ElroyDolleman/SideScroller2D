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
    class StateManager
    {
        public SpriteBatchSettings SpriteBatchSettings { get => spriteBatchSettings; set => spriteBatchSettings = value; }

        private SpriteBatchSettings spriteBatchSettings;
        private List<BaseState> states;

        private int currentStateID = 0;

        public StateManager(SpriteBatchSettings spriteBatchSettings)
        {
            states = new List<BaseState>();

            this.spriteBatchSettings = spriteBatchSettings;
        }

        public int AddState(BaseState state)
        {
            states.Add(state);

            return states.Count - 1;
        }

        public void ChangeState(int id)
        {
#if DEBUG
            if (id >= states.Count)
            {
                Console.WriteLine("Warning: invalid state id {0}", id);
                return;
            }
            Console.WriteLine("StateManager::ChangeState {0}", id);
#endif

            currentStateID = id;
        }

        public void OnContentLoaded()
        {
            foreach (BaseState state in states)
                state.OnContentLoaded();
        }

        public void Update(GameTime gameTime)
        {
            states[currentStateID].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(spriteBatchSettings);

            states[currentStateID].Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
