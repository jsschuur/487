using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS.Scripts
{
    class LinearMovement : Script
    {
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }

        private Transform transform;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            transform = (Transform)owner.GetComponent<Transform>();
        }


        public override void Update(GameTime gameTime)
        {
            transform.Position += Direction * Speed;
        }

    }
}
