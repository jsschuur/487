using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.Screen;

namespace Take3
{
    public sealed class Input
    {
        private static readonly Input input = new Input();

        private static Dictionary<string, Keys> _keys;

        private static KeyboardState _previousKeyboardState;
        private static MouseState _previousMouseState;

        static Input() { }
        private Input()
        {
            _keys = new Dictionary<string, Keys>();
            _keys["left"] = Keys.A;
            _keys["right"] = Keys.D;
            _keys["up"] = Keys.W;
            _keys["down"] = Keys.S;

            _keys["fire"] = Keys.Space;
            _keys["p"] = Keys.P;
            _keys["o"] = Keys.O;
            _keys["shift"] = Keys.LeftShift;
        }

        public static bool KeyDown(string input)
        {
            KeyboardState state = Keyboard.GetState();
            bool isDown;

            if(state.IsKeyDown(_keys[input]))
            {
                isDown = true;
            }
            else
            {
                isDown = false;
            }

            _previousKeyboardState = state;
            return isDown;
        }

        public static bool SingleKeyPress(string input)
        {
            KeyboardState state = Keyboard.GetState();
            bool isDown;

            if (state.IsKeyDown(_keys[input]) && !_previousKeyboardState.IsKeyDown(_keys[input]))
            {
                isDown = true;
            }
            else
            {
                isDown = false;
            }

            return isDown;
        }

        public static Vector2 GetMouseLocation()
        {
            var mouseLocation = Mouse.GetState().Position;      
            return new Vector2(mouseLocation.X / Resolution.Scale.X, mouseLocation.Y / Resolution.Scale.Y);
        }


        public static bool MouseDown()
        {
            return Mouse.GetState().LeftButton == ButtonState.Pressed;
        }

        public static bool Clicked()
        {
            return Mouse.GetState().LeftButton == ButtonState.Released &&
               _previousMouseState.LeftButton == ButtonState.Pressed;
        }
    }
}
