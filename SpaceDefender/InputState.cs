using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefender
{
    /// <summary>
    ///    Helper for reading input from keyboard, gamepad, and touch input. This class
    ///    tracks both the current and previous state of the input devices, and implements
    ///    query methods for high level input actions such as "move up through the menu"
    ///    or "pause the game".
    /// </summary>
    public class InputState
    {
        private const int MAX_INPUTS = 4;

        internal readonly GamePadState[] CurrentGamePadStates;
        internal readonly KeyboardState[] CurrentKeyboardStates;
        internal readonly bool[] GamePadWasConnected;

        internal readonly GamePadState[] LastGamePadStates;
        internal readonly KeyboardState[] LastKeyboardStates;

        internal MouseState CurrentMouseState { get; private set; }
        internal MouseState LastMouseState { get; private set; }

        internal InputState()
        {
            CurrentKeyboardStates = new KeyboardState[MAX_INPUTS];
            CurrentGamePadStates = new GamePadState[MAX_INPUTS];

            LastKeyboardStates = new KeyboardState[MAX_INPUTS];
            LastGamePadStates = new GamePadState[MAX_INPUTS];

            CurrentMouseState = new MouseState();
            LastMouseState = new MouseState();

            GamePadWasConnected = new bool[MAX_INPUTS];
        }

        /// <summary>
        ///    Reads the latest state of the keyboard and gamepad.
        /// </summary>
        internal void Update()
        {
            for (int i = 0; i < MAX_INPUTS; i++)
            {
                LastKeyboardStates[i] = CurrentKeyboardStates[i];
                LastGamePadStates[i] = CurrentGamePadStates[i];

                CurrentKeyboardStates[i] = Keyboard.GetState();
                //CurrentKeyboardStates[i] = Keyboard.GetState( (PlayerIndex) i );
                CurrentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);

                // Keep track of whether a gamepad has ever been
                // connected, so we can detect if it is unplugged.
                if (CurrentGamePadStates[i].IsConnected)
                {
                    GamePadWasConnected[i] = true;
                }
            }

            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        #region Mouse

        internal bool IsNewLeftMouseClick(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return (CurrentMouseState.LeftButton == ButtonState.Released && LastMouseState.LeftButton == ButtonState.Pressed);
        }

        internal bool IsNewRightMouseClick(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return (CurrentMouseState.RightButton == ButtonState.Released && LastMouseState.RightButton == ButtonState.Pressed);
        }

        internal bool IsNewThirdMouseClick(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return (CurrentMouseState.MiddleButton == ButtonState.Pressed && LastMouseState.MiddleButton == ButtonState.Released);
        }

        internal bool IsNewMouseScrollUp(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return (CurrentMouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue);
        }

        internal bool IsNewMouseScrollDown(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return (CurrentMouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue);
        }

        #endregion

        #region Keyboard

        /// <summary>
        ///    Helper for checking if a key was newly pressed during this update. The
        ///    controllingPlayer parameter specifies which player to read input for.
        ///    If this is null, it will accept input from any player. When a keypress
        ///    is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        internal bool IsNewKeyPress(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            return IsKeyPressed(key, controllingPlayer, out playerIndex, true);
        }

        internal bool IsKeyPressed(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            return IsKeyPressed(key, controllingPlayer, out playerIndex, false);
        }

        private bool IsKeyPressed(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex, bool isNew)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                var i = (int)playerIndex;

                bool ret;
                if (isNew)
                {
                    ret = (CurrentKeyboardStates[i].IsKeyDown(key) && LastKeyboardStates[i].IsKeyUp(key));
                }
                else
                {
                    ret = (CurrentKeyboardStates[i].IsKeyDown(key));
                }

                return ret;
            }

            // Accept input from any player.
            return (IsNewKeyPress(key, PlayerIndex.One, out playerIndex) ||
                    IsNewKeyPress(key, PlayerIndex.Two, out playerIndex) ||
                    IsNewKeyPress(key, PlayerIndex.Three, out playerIndex) ||
                    IsNewKeyPress(key, PlayerIndex.Four, out playerIndex));
        }

        internal bool IsExitGame(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Escape, controllingPlayer, out playerIndex) || IsNewButtonPress(Buttons.Back, controllingPlayer, out playerIndex);
        }

        internal bool IsF1(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.F1, controllingPlayer, out playerIndex);
        }

        internal bool IsPause(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Pause, controllingPlayer, out playerIndex);
        }

        internal bool IsLeft(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.Left, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.DPadLeft, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.LeftThumbstickLeft, controllingPlayer, out playerIndex);
        }

        internal bool IsRight(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.Right, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.DPadRight, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.LeftThumbstickRight, controllingPlayer, out playerIndex);
        }

        internal bool IsUp(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Up, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.DPadUp, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.LeftThumbstickUp, controllingPlayer, out playerIndex);
        }

        internal bool IsDown(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Down, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.DPadDown, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.LeftThumbstickDown, controllingPlayer, out playerIndex);
        }

        internal bool IsSpace(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return IsNewKeyPress(Keys.Space, controllingPlayer, out playerIndex);
        }

        internal bool IsScrollLeft(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.A, controllingPlayer, out playerIndex);
        }

        internal bool IsScrollRight(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.D, controllingPlayer, out playerIndex);
        }

        internal bool IsScrollUp(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.W, controllingPlayer, out playerIndex);
        }

        internal bool IsScrollDown(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.S, controllingPlayer, out playerIndex);
        }

        internal bool IsZoomOut(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.OemMinus, controllingPlayer, out playerIndex);
        }

        internal bool IsZoomIn(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.OemPlus, controllingPlayer, out playerIndex);
        }

        internal bool IsRotateClockwise(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.OemPeriod, controllingPlayer, out playerIndex);
        }

        internal bool IsRotateAntiClockwise(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.OemComma, controllingPlayer, out playerIndex);
        }

        internal bool IsNumPad1(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.B, controllingPlayer, out playerIndex);
        }

        internal bool IsNumPad2(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.N, controllingPlayer, out playerIndex);
        }

        internal bool IsNumPad3(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.M, controllingPlayer, out playerIndex);
        }

        internal bool IsNumPad4(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.G, controllingPlayer, out playerIndex);
        }

        internal bool IsNumPad5(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.H, controllingPlayer, out playerIndex);
        }

        internal bool IsNumPad6(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.J, controllingPlayer, out playerIndex);
        }

        internal bool IsNumPad7(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.T, controllingPlayer, out playerIndex);
        }

        internal bool IsNumPad8(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.Y, controllingPlayer, out playerIndex);
        }

        internal bool IsNumPad9(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(Keys.U, controllingPlayer, out playerIndex);
        }

        #endregion

        #region GamePad

        /// <summary>
        ///    Helper for checking if a button was newly pressed during this update.
        ///    The controllingPlayer parameter specifies which player to read input for.
        ///    If this is null, it will accept input from any player. When a button press
        ///    is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        internal bool IsNewButtonPress(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            return IsButtonPressed(button, controllingPlayer, out playerIndex, true);
        }

        internal bool IsButtonPressed(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            return IsButtonPressed(button, controllingPlayer, out playerIndex, false);
        }

        private bool IsButtonPressed(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex, bool isNew)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                var i = (int)playerIndex;

                bool ret;
                if (isNew)
                {
                    ret = (CurrentGamePadStates[i].IsButtonDown(button) && LastGamePadStates[i].IsButtonUp(button));
                }
                else
                {
                    ret = (CurrentGamePadStates[i].IsButtonDown(button));
                }

                return ret;
            }

            // Accept input from any player.
            return (IsButtonPressed(button, PlayerIndex.One, out playerIndex) ||
                    IsButtonPressed(button, PlayerIndex.Two, out playerIndex) ||
                    IsButtonPressed(button, PlayerIndex.Three, out playerIndex) ||
                    IsButtonPressed(button, PlayerIndex.Four, out playerIndex));
        }
        #endregion

    }
}