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

        protected Transform _transform;

        public float Buffer { get; set; }

        public Collision OnCollision { get; set; }

        protected Vector2 size;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            _transform = (Transform)owner.GetComponent<Transform>();
            var renderer = (Renderer)owner.GetComponent<Renderer>();
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
