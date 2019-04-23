using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS
{
    public abstract class Renderer : Component
    {
        protected Transform transform;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            transform = (Transform)GetComponent<Transform>();
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
