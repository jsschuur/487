using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Take3.ECS
{
    class TextRenderer : Renderer
    {

        public SpriteFont Font { get; set; }
        public Color TextColor { get; set; }

        public string Text { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, transform.Position, TextColor, 0.0f, Vector2.Zero, 1, SpriteEffects.None, 0.9f);
        }
    }
}
