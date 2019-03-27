using _487Game.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _487Game.Utility.UtilityMath;

namespace _487Game.EnemyBehavior
{
    class EnemyAttack
    {
        private float _cooldown;
        private float _duration;

        private float _delay;
        private float _deltaAngle;
        private float _deltaAcceleration;

        private float _minAngle;
        private float _maxAngle;

        private ProjectileInformation _attackProjectile;

        private Vector2 _direction;

        public float Cooldown { get { return _cooldown; } }
        public Vector2 Direction { get { return _direction; } }
        public ProjectileInformation ProjectileInformation { get { return _attackProjectile; } }

        public EnemyAttack(float cooldown, float duration, float delay, float minAngle, float maxAngle, float deltaAngle, float deltaAcceleration, ProjectileInformation attackProjectile, Vector2 direction)
        {
            _cooldown = cooldown;
            _duration = duration;
            _delay = delay;
            _attackProjectile = attackProjectile;
            _direction = direction;
            _minAngle = minAngle;
            _maxAngle = maxAngle;
            _deltaAngle = deltaAngle;
            _deltaAcceleration = deltaAcceleration;
        }

        public void SetRange(float min, float max, float delta)
        {
            _minAngle = min;
            _maxAngle = max;
            _deltaAngle = delta;
        }

        public void SetAccelerationChange(float delta)
        {
            _deltaAcceleration = delta;
        }
        

        public void UpdateProjectile()
        {
            var currentAngle = VectorMath.Vector2Angle(_direction);
            currentAngle += VectorMath.Degrees2Radians(_deltaAngle);
            _direction = VectorMath.Angle2Vector(currentAngle);

            if(!VectorMath.IsInRange(_minAngle, _maxAngle, (float)VectorMath.Radians2Degrees(currentAngle)))
            {
                _deltaAngle *= -1;
            }

            _attackProjectile.Acceleration += _deltaAcceleration;
        }
    }
}
