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
        public float Radius { get { return (radius - buffer) * transform.Scale; } }

        private float radius;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            if(size.X > size.Y)
            {
                radius = (size.Y / 2);
            }
            else
            {
                radius = (size.X / 2);
            }
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(transform.X + (size.X / 2), transform.Y + (size.Y / 2));
            }
        }

        public Vector2 GetPosition()
        {
            return new Vector2(transform.X + ((size.X * transform.Scale) / 2), transform.Y + ((size.Y * transform.Scale) / 2));
        }
    }
}
