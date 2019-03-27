using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Input
{
    class KeyboardManager
    {
        private event EventHandler<NewInputEventArgs> _newInput;
        private double _timer;
        private double _cooldown;

        private KeyboardState _keyboardState;
        private KeyboardState _previousKeyboardState;
        private Keys _previousKey;

        public KeyboardManager()
        {
            _timer = 0;
            _cooldown = 0;
        }



        public event EventHandler<NewInputEventArgs> NewInput
        {
            add { _newInput += value; }
            remove { _newInput -= value; }
        }

        public void Update(GameTime gameTime)
        {
            if (_cooldown > 0)
            {
                _timer += gameTime.ElapsedGameTime.Milliseconds;
                if (_timer > gameTime.ElapsedGameTime.Milliseconds)
                {
                    _timer = 0;
                    _cooldown = 0;
                }
                else
                {
                    return;
                }
            }
            CheckInput(gameTime);
        }

        private void SendNewInput(Static.Input input, GameTime gameTime)
        {
            _newInput?.Invoke(this, new NewInputEventArgs(input, gameTime));
        }

        private void CheckKeyState(Keys key, Static.Input input, GameTime gameTime)
        {
            if (_keyboardState.IsKeyDown(key))
            {
                SendNewInput(input, gameTime);
                _previousKey = key;
            }
        }

        protected void CheckInput(GameTime gameTime)
        {
            _keyboardState = Keyboard.GetState();

            if (_keyboardState.IsKeyUp(_previousKey) && _previousKey != Keys.None)
            {
                SendNewInput(Static.Input.None, gameTime);
            }

            CheckKeyState(Keys.Space, Static.Input.Space, gameTime);

            CheckKeyState(Keys.A, Static.Input.Left, gameTime);
            CheckKeyState(Keys.W, Static.Input.Up, gameTime);
            CheckKeyState(Keys.D, Static.Input.Right, gameTime);
            CheckKeyState(Keys.S, Static.Input.Down, gameTime);


            CheckKeyState(Keys.Left, Static.Input.Left, gameTime);
            CheckKeyState(Keys.Up, Static.Input.Up, gameTime);
            CheckKeyState(Keys.Right, Static.Input.Right, gameTime);
            CheckKeyState(Keys.Down, Static.Input.Down, gameTime);

            if(_previousKeyboardState.IsKeyDown(Keys.P))
            {
                CheckKeyState(Keys.P, Static.Input.Pause, gameTime);
            }

            if(!_previousKeyboardState.IsKeyDown(Keys.LeftShift))
            {
                CheckKeyState(Keys.LeftShift, Static.Input.Shift, gameTime);
            }

            if(!_previousKeyboardState.IsKeyUp(Keys.LeftShift))
            {
                if(_keyboardState.IsKeyUp(Keys.LeftShift))
                {
                    SendNewInput(Static.Input.ShiftUp, gameTime);
                }
            }

            _previousKeyboardState = _keyboardState;

        }
    }
}
