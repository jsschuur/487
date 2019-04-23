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

        private static Dictionary<string, Keys> keys;

        private static KeyboardState previousKeyboardState;
        private static MouseState previousMouseState;

        static Input() { }
        private Input()
        {
            keys = new Dictionary<string, Keys>();
            keys["left"] = Keys.A;
            keys["right"] = Keys.D;
            keys["up"] = Keys.W;
            keys["down"] = Keys.S;

            keys["fire"] = Keys.Space;
            keys["pause"] = Keys.P;
            keys["slow"] = Keys.LeftShift;
        }

        public static Dictionary<string, Keys> GetKeys()
        {
            return keys;
        }

        public static void SetKey(string input, Keys key)
        {
            keys[input] = key;
        }

        public static bool KeybindOccupied(Keys key)
        {
            return keys.ContainsValue(key);
        }

        public static bool KeyDown(string input)
        {
            KeyboardState state = Keyboard.GetState();
            bool isDown;

            if(state.IsKeyDown(keys[input]))
            {
                isDown = true;
            }
            else
            {
                isDown = false;
            }

            previousKeyboardState = state;
            return isDown;
        }

        public static bool SingleKeyPress(string input)
        {
            KeyboardState state = Keyboard.GetState();
            bool isDown;

            if (state.IsKeyDown(keys[input]) && !previousKeyboardState.IsKeyDown(keys[input]))
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
               previousMouseState.LeftButton == ButtonState.Pressed;
        }
    }
}
