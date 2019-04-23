using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;

namespace Take3.ECS.Scripts 
{
    class RightKeybindButton : KeybindingButton
    {
        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            keybind = "right";

            var transform = (Transform)GetComponent<Transform>();
            var keyInputInstance = GameManager.Instantiate(GameManager.GetPrefab("KeybindingInput"));
            var keyInputTransform = (Transform)keyInputInstance.GetComponent<Transform>();
            keyInputTransform.Position = transform.Position;

            keyInput = (KeyInput)keyInputInstance.GetComponent<KeyInput>();
            keyInput.Key = Input.GetKeys()["right"];
        }
    }
}
