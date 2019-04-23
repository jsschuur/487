using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;

namespace Take3.ECS.Scripts 
{
    class KeybindingOptions : Script
    {

        private Dictionary<string, Keys> input;

       

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            input = new Dictionary<string, Keys>();

            foreach(var obj in Input.GetKeys())
            {
                input.Add(obj.Key, obj.Value);
            }
        }
    }
}
