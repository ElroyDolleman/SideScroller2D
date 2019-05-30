using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SideScroller2D.Input
{
    static class InputManager
    {
        public static KeyboardState CurrentKeyboardState { get { return currentState; } }
        public static KeyboardState PreviousKeyboardState { get { return previousState; } }

        public static List<PlayerInputs> PlayerInputs;

        private static List<GamePadState> gamePadStates;
        private static List<GamePadState> previousGamePadStates;
        private static KeyboardState currentState;
        private static KeyboardState previousState;

        public static void Initialize()
        {
            PlayerInputs = new List<PlayerInputs>();

            gamePadStates = new List<GamePadState>();
            for (int i = 0; i < 4; i++)
                gamePadStates.Add(GamePad.GetState((PlayerIndex)i));

            InitializePlayerInputs(0);
            //InitializePlayerInputs(1);
            //InitializePlayerInputs(2);
            //InitializePlayerInputs(3);
        }

        public static void InitializePlayerInputs(int playerIndex)
        {
            var newinputs = new PlayerInputs();

            newinputs.Jump = new Input(Keys.Space, Buttons.A, (PlayerIndex)playerIndex);

            newinputs.Up = new Input(Keys.Up, Buttons.DPadUp, (PlayerIndex)playerIndex);
            newinputs.Down = new Input(Keys.Down, Buttons.DPadDown, (PlayerIndex)playerIndex);
            newinputs.Left = new Input(Keys.Left, Buttons.DPadLeft, (PlayerIndex)playerIndex);
            newinputs.Right = new Input(Keys.Right, Buttons.DPadRight, (PlayerIndex)playerIndex);

            newinputs.UsesJoystick = false;

            if (PlayerInputs.Count > playerIndex)
                PlayerInputs[playerIndex] = newinputs;
            else
                PlayerInputs.Add(newinputs);
        }

        public static void UpdateState()
        {
            for (int i = 0; i < PlayerInputs.Count; i++)
            {
                previousGamePadStates = gamePadStates;
                gamePadStates[i] = GamePad.GetState((PlayerIndex)i);
            }

            previousState = currentState;
            currentState = Keyboard.GetState();
        }

        public static bool IsDown(Input input)
        {
            if (PlayerInputs[(int)input.PlayerIndex].UsesJoystick)
                return gamePadStates[(int)input.PlayerIndex].IsButtonDown(input.Button);

            return currentState.IsKeyDown(input.Key);
        }

        public static bool JustPressed(Input input)
        {
            if (PlayerInputs[(int)input.PlayerIndex].UsesJoystick)
                return gamePadStates[(int)input.PlayerIndex].IsButtonDown(input.Button) &&
                    previousGamePadStates[(int)input.PlayerIndex].IsButtonUp(input.Button);

            return currentState.IsKeyDown(input.Key) && previousState.IsKeyUp(input.Key);
        }

        public static bool JustReleased(Input input)
        {
            if (PlayerInputs[(int)input.PlayerIndex].UsesJoystick)
                return gamePadStates[(int)input.PlayerIndex].IsButtonUp(input.Button) &&
                    previousGamePadStates[(int)input.PlayerIndex].IsButtonDown(input.Button);

            return currentState.IsKeyUp(input.Key) && previousState.IsKeyDown(input.Key);
        }
    }
}
