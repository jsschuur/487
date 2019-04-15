using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS
{
    class CircleCollider : Collider
    {
        public float Radius { get { return radius - Buffer; } }

        private float radius;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            
            if(size.X > size.Y)
            {
                radius = size.X / 2;
            }
            else
            {
                radius = size.Y / 2;
            }
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(_transform.X + (size.X / 2), _transform.Y + (size.Y / 2));
            }
        }
    }
}
