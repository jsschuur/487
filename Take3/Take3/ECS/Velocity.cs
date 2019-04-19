using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS
{
    class Velocity : Updatable
    {
        private Vector2 direction;
        private Vector2 lastPosition;

        public Transform transform;

        public float Speed;
        public float Acceleration;

        public Vector2 Direction { get { return direction; } set { direction = value; } }
        public Vector2 LastPosition { get { return lastPosition; } }

        public float XDir { get { return direction.X; } set { direction.X = value; } }
        public float YDir { get { return direction.Y; } set { direction.Y = value; } }

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            direction = new Vector2();
            lastPosition = new Vector2();

            transform = (Transform)GetComponent<Transform>();
        }

        public override void Update(GameTime gameTime)
        {
            lastPosition = transform.Position;

            if(direction.X != 0 || direction.Y != 0)
            {
                direction.Normalize();
            }
            transform.Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed += Acceleration;
        }
    }
}
