using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _487Game.Components.EnemyBehaviorComponents
{
    class LinearMovementComponent : Component
    {
        private double _delay;
        private double _lastTimeSwitched;

        private Vector2 _direction;

        public LinearMovementComponent(Entity owner, double delay, float xDir, float yDir) : base(owner)
        {
            _componentType = "LinearMovementComponent";
            _delay = delay;
            _direction = new Vector2(xDir, yDir);

            if(_direction.X != 0 || _direction.Y != 0)
            {
                _direction.Normalize();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalMilliseconds - _lastTimeSwitched >= _delay)
            {
                //switch!
                _lastTimeSwitched = gameTime.TotalGameTime.TotalMilliseconds;
                _direction *= -1;
                ((MovementComponent)_owner.GetComponent("MovementComponent")).Direction = _direction;
            }
        }
    }
}
