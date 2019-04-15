using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS
{
    public class Sprite
    {
        private Rectangle spriteRectangle;

        public Texture2D Texture { get; set; }

        public Rectangle SpriteRectangle { get { return spriteRectangle; } set { spriteRectangle = value; } }

        public int XOffset { get { return SpriteRectangle.X; } set { spriteRectangle.X = value; } }
        public int YOffset { get { return SpriteRectangle.Y; } set { spriteRectangle.Y = value; } }


        public float Scale { get; set; }
        public bool Rotatable { get; set; }

        public float LayerDepth { get; set; }

        public Sprite() { }

        public Sprite(Texture2D texture, Rectangle spriteRectangle, float scale, bool rotatable, float layerDepth)
        {
            Texture = texture;
            SpriteRectangle = spriteRectangle;
            Scale = scale;
            Rotatable = rotatable;
            LayerDepth = layerDepth;
        }

        public Vector2 GetCenter
        {
            get
            {
                return new Vector2(SpriteRectangle.Width / 2f * Scale, SpriteRectangle.Height / 2f * Scale);
            }
        }

        public Vector2 GetDimensions
        {
            get
            {
                return new Vector2(SpriteRectangle.Width * Scale, SpriteRectangle.Height * Scale); 
            }
        }
    }
}