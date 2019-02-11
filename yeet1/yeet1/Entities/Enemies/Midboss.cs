using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using yeet1.Entities.Behavior;
using yeet1.Entities.Projectiles;

namespace yeet1.Entities.Enemies
{
    class Midboss : EventEntity
    {
        private const int _spriteWidth = 128;
        private const int _spriteHeight = 128;

        private MidbossAttack midbossAttack;
        private MidbossMovement midbossMovement;

        public Midboss(Texture2D texture, Vector2 origin, EntityManager entityManager) : base(texture, origin, entityManager)
        {
            _sourceRectangle = new Rectangle(0, 0, _spriteWidth, _spriteHeight);
            midbossMovement = new MidbossMovement(this);
            midbossAttack = new MidbossAttack(entityManager, this);
        }

        public override void Update(GameTime gameTime)
        {
            midbossAttack.Update(gameTime);
            midbossMovement.Update(gameTime);
        }


        class MidbossMovement : EnemyMovement
        {
            private double _lastDirectionChange;
            private double _directionChangeDelay;

            public MidbossMovement(Entity owner) : base(owner)
            {
                _speed = 200;
                _direction = new Vector2(-1, 1);

                _directionChangeDelay = 500;
                _lastDirectionChange = 0;
            }

            public override void Transform(GameTime gameTime)
            {
                if(gameTime.TotalGameTime.TotalMilliseconds - _directionChangeDelay > _lastDirectionChange)
                {
                    if (_direction.X < 0 && _direction.Y < 0)
                    {
                        _direction.Y *= -1;
                    }
                    else if (_direction.X > 0 && _direction.Y > 0)
                    {
                        _direction.Y *= -1;
                    }
                    else if (_direction.X > 0 && _direction.Y < 0)
                    {
                        _direction.X *= -1;
                    }
                    else if (_direction.X < 0 && _direction.Y > 0)
                    {
                        _direction.X *= -1;
                    }
                    _lastDirectionChange = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }
        }
        class MidbossAttack : EnemyAttack
        {
            public MidbossAttack(EntityManager entityManager, Entity owner) : base(entityManager, owner)
            {
                _delay = 100;
                _projectileInformation = new ProjectileInformation("BasicEnemyProjectile", 16, 16, 20.0f, 15.0f, new Vector2(0.0f, 1.0f));
                _origin = Static.Origin.BottomCenter;
                _cooldown = 0;
            }

            public override void Transform()
            {
            }
        }
    }
}
