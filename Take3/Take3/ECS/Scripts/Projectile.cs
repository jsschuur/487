using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS.Scripts
{
    class Projectile : Script
    {

        private Transform transform;

        protected float speed;
        public Vector2 Direction { get; set; }
        protected int damage;

        public override void OnCollision(GameObject collider)
        {
            if(collider.Tag == "ProjectileBoundary")
            {
                Die();
            }
        }

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            transform = (Transform)GetComponent<Transform>();
        }

        public override void Update(GameTime gameTime)
        {
            transform.Position += speed * Direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
