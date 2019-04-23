using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Take3.ECS.Scripts
{ 
    public class KeyInput : Script
    {

        private TextRenderer textRenderer;

        private Keys key;

        public Keys Key { get { return key; }
            set
            {
                key = value;
                textRenderer.Text = key.ToString().ToUpper();
            }
        }
        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            textRenderer = (TextRenderer)owner.AddComponent<TextRenderer>();
            textRenderer.Font = TextureManager.LoadFont("GameFont");
            textRenderer.TextColor = Color.Black;
        }
    }
}
