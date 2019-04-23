using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS
{
    public abstract class Collider : Component
    {
        public delegate void Collision(GameObject collider);

        protected Transform transform;

        protected Vector2 size;
        protected float buffer;

        public float Buffer { get { return buffer; } set { buffer = value; } }
        public Collision OnCollision { get; set; }


        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            transform = (Transform)owner.GetComponent<Transform>();
            var renderer = (SpriteRenderer)owner.GetComponent<SpriteRenderer>();
            size = new Vector2(renderer.Sprite.SpriteRectangle.Width * renderer.Sprite.Scale, 
                               renderer.Sprite.SpriteRectangle.Height * renderer.Sprite.Scale);
        }

        public void Collide(GameObject collider)
        {
            if (OnCollision != null)
            {
                OnCollision(collider);
            }
        }
    }
}
