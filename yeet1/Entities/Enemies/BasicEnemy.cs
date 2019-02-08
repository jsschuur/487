using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using yeet1.Entities.Behavior;
using yeet1.Entities.Projectiles;
using yeet1.Static;
using yeet1.Texture;

namespace yeet1.Entities.Enemies
{
    class BasicEnemy : EventEntity
    {
        private const int _spriteWidth = 64;
        private const int _spriteHeight = 64;

        private const int _destinationWidth = 64;
        private const int _destinationHeight = 64;

        private const string _textureName = "BasicEnemy";

        private Vector2 _origin;

        public Vector2 Origin { set { this._origin = value; } }

        private BasicEnemyAttack _basicEnemyAttack;

        public BasicEnemy(Texture2D texture, Vector2 origin, EntityManager entityManager) : base(texture, origin, entityManager)
        {
            _texture = texture;

            _sourceRectangle = new Rectangle(0, 0, _spriteWidth, _spriteHeight);
            _destinationRectangle = new Rectangle(0, 0, _destinationWidth, _destinationHeight);

            _origin = origin;

            _speed = 3;
            _direction.X = 1;

            _basicEnemyAttack = new BasicEnemyAttack(entityManager, this);

        }

        private void BasicEnemyAttackTransform(ref Vector2 direction)
        {
            direction.X = 0;
            direction.Y = 1;
        }

        public override void Update(GameTime gameTime)
        {
            if (_position.X > (_origin.X + 70))
            {
                _direction.X = -1;
            }
            if (_position.X < _origin.X - 70)
            {
                _direction.X = 1;
            }

            _basicEnemyAttack.Update(gameTime);

            _position.X += (_speed * _direction.X);
            _position.Y += (_speed * _direction.Y);
            UpdateSpritePosition();
        }

        class BasicEnemyAttack : EnemyAttack
        {
            public BasicEnemyAttack(EntityManager entityManager, Entity owner) : base(entityManager, owner)
            {
                _delay = 300;
                _projectileInformation = new ProjectileInformation("BasicEnemyProjectile", 16, 16, 3, 0, new Vector2(0.0f, 1.0f));
                _origin = Static.Origin.BottomCenter;
                _cooldown = 0;
                
               
            }

            public override void Transform()
            {
                
            }
        }
    }
}
