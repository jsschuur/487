using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;

namespace Take3.ECS.Scripts 
{
    public abstract class KeybindingButton : Button
    {
        protected string keybind;
        protected KeyInput keyInput;
        private bool isListening;

        public override void OnClick()
        {
            isListening = true;
            foreach(var keybindingButton in GameManager.GetComponents<KeybindingButton>())
            {
                keybindingButton.IsActive = false;
            }
            IsActive = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(isListening)
            {
                state = ButtonObjectState.Hovered;
                animator.SeamlessAnimationSwitch("Hovered");

                KeyboardState keyboardState = Keyboard.GetState();

                if(keyboardState.GetPressedKeys().Any())
                {

                    var newKeybind = keyboardState.GetPressedKeys().First();

                    if (!Input.KeybindOccupied(newKeybind))
                    {
                        keyInput.Key = newKeybind;
                        Input.SetKey(keybind, keyInput.Key);
                    }

                    isListening = false;
                    state = ButtonObjectState.Idle;

                    foreach (var keybindingButton in GameManager.GetComponents<KeybindingButton>())
                    {
                        keybindingButton.IsActive = true;
                    }
                }
            }
        }
    }
}
