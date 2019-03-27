using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _487Game.Components
{
    class MovementComponent : Component
    {

        private float _speed;
        private float _acceleration;

        private Vector2 _direction;

        public Vector2 Direction { get { return _direction; } set { _direction = value; } }

        public float xDir { get { return _direction.X; } set { _direction.X = value; } }
        public float yDir { get { return _direction.Y; } set { _direction.Y = value; } }

        public float Speed { get { return _speed; } set { _speed = value; } }


        public MovementComponent(Entity owner) : base(owner)
        {
            _componentType = "MovementComponent";
            _direction = new Vector2(0f, 0f);
        }

        public MovementComponent(Entity owner, float speed) : base(owner)
        {
            _componentType = "MovementComponent";
            _direction = new Vector2(0f, 0f);
            _speed = speed;
        }

        public MovementComponent(Entity owner, float speed, float acceleration) : base(owner)
        {
            _componentType = "MovementComponent";
            _direction = new Vector2(0f, 0f);
            _speed = speed;
            _acceleration = acceleration;
        }

        public MovementComponent(Entity owner, Vector2 direction, float speed, float acceleration) : base(owner)
        {
            _componentType = "MovementComponent";
            _direction = direction;
            _speed = speed;
            _acceleration = acceleration;
        }

        public override void Update(GameTime gameTime)
        {

            if(_direction.X != 0 || _direction.Y != 0)
            {
                _direction.Normalize();
            }

            ((PositionComponent)_owner.GetComponent("PositionComponent")).X += _direction.X * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            ((PositionComponent)_owner.GetComponent("PositionComponent")).Y += _direction.Y * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            _speed += _acceleration;
        }
    }
}
