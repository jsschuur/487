using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.Utility;
using static Take3.Utility.UtilityMath;

namespace Take3.ECS
{
    public class SpriteRenderer : Renderer 
    {

        public Sprite Sprite { get; set; }
        

        //fix sprite get center property
        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects sp = (Sprite.Rotatable &&
                                VectorMath.IsInRange((float)Math.PI, 0, transform.Rotation)) ?
                                sp = SpriteEffects.FlipHorizontally :
                                sp = SpriteEffects.None;

             spriteBatch.Draw(Sprite.Texture,
                             transform.Position + Sprite.GetCenter(),
                             Sprite.SpriteRectangle,
                             Color.White,
                             transform.Rotation,
                             Sprite.GetCenter() / Sprite.Scale,
                             Sprite.Scale * transform.Scale,
                             sp, 
                             Sprite.LayerDepth);
        }
    }
}
