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

        private BasicEnemyAttack _basicEnemyAttack;
        private BasicEnemyMovement _basicEnemyMovement;

        public BasicEnemy(Texture2D texture, Vector2 origin, EntityManager entityManager) : base(texture, origin, entityManager)
        {
            _sourceRectangle = new Rectangle(0, 0, _spriteWidth, _spriteHeight);
            _basicEnemyAttack = new BasicEnemyAttack(entityManager, this);
            _basicEnemyMovement = new BasicEnemyMovement(this);
        }

        public override void Update(GameTime gameTime)
        {
            _basicEnemyMovement.Update(gameTime);
            _basicEnemyAttack.Update(gameTime);
        }

        class BasicEnemyAttack : EnemyAttack
        {
            public BasicEnemyAttack(EntityManager entityManager, Entity owner) : base(entityManager, owner)
            {
                _delay = 300;
                _projectileInformation = new ProjectileInformation("BasicEnemyProjectile", 16, 16, 120.0f, 0, new Vector2(0.0f, 1.0f));
                _origin = Static.Origin.BottomCenter;
                _cooldown = 0;   
            }

            public override void Transform()
            {
                
            }
        }

        class BasicEnemyMovement : EnemyMovement
        {
            private Vector2 _origin;
          
            public BasicEnemyMovement(Entity owner) : base(owner)
            {
                _speed = 120;
                _direction = new Vector2(1, 0);
                _origin = owner.Position;
            }

            public override void Transform(GameTime gameTime)
            {
                if(_owner.Position.X >= _origin.X + 80 || _owner.Position.X <= _origin.X - 80)
                {
                    _direction.X *= -1;
                }
            }
        }
    }
}
