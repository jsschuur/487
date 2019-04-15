using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Take3.Utility.UtilityMath;

namespace Take3.ECS.Scripts
{
    class SingleSwitchLinearMovement : Script
    {
        private double _timeLastSwitched;
        private Transform _transform;

        public float Delay { get; set; }
        public float Speed { get; set; }
        public Vector2 Direction { get; set; }

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            Delay = 1000;
            Speed = 100;
            Direction = new Vector2(1, 0);
            _transform = (Transform)GetComponent<Transform>();
        }

        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalMilliseconds >= _timeLastSwitched + Delay)
            {
                Speed *= -1;
                _timeLastSwitched = gameTime.TotalGameTime.TotalMilliseconds;
            }

            _transform.Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _transform.Rotation = (float)VectorMath.Vector2Angle(Direction);
        }
    }
}
