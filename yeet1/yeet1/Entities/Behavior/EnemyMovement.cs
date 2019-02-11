using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yeet1.Entities.Behavior
{
    abstract class EnemyMovement
    {
        protected Entity _owner;

        protected float _speed;
        protected Vector2 _direction;
        protected float _acceleration;

        public EnemyMovement(Entity owner)
        {
            _owner = owner;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 pos = _owner.Position;

            if(_direction.X > 0 || _direction.Y > 0)
            {
                _direction.Normalize();
            }

            pos.X += _speed * _direction.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            pos.Y += _speed * _direction.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

            _owner.Position = pos;

            Transform(gameTime);
        }


        public abstract void Transform(GameTime gameTime);
    }
}
