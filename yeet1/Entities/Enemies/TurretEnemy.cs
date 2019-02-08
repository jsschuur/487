﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using yeet1.Entities.Behavior;
using yeet1.Entities.Projectiles;
using yeet1.Static;
using yeet1.Utility;

namespace yeet1.Entities.Enemies
{
    class TurretEnemy : EventEntity
    {

        private const int _spriteWidth = 32;
        private const int _spriteHeight = 32;

        private const string _textureName = "TurretEnemy";

        private Vector2 _origin;

        public Vector2 Origin { set { this._origin = value; } }

        private TurretEnemyAttack _turretEnemyAttack;

        public TurretEnemy(Texture2D texture, Vector2 origin, EntityManager entityManager) : base(texture, origin, entityManager)
        {
            _texture = texture;
            _origin = origin;

            _sourceRectangle = new Rectangle(0, 0, _spriteWidth, _spriteHeight);
            _turretEnemyAttack = new TurretEnemyAttack(entityManager, this);
            _destinationRectangle = new Rectangle(0, 0, _spriteWidth, _spriteHeight);

            _speed = 0;
        }

        public override void Update(GameTime gameTime)
        {
            _destinationRectangle.X = (int)_position.X;
            _destinationRectangle.Y = (int)_position.Y;
            _turretEnemyAttack.Update(gameTime);
        }

        class TurretEnemyAttack : EnemyAttack
        {
            private double _currentAngle;
            private double _angleOffset;

            private int _count;

            public TurretEnemyAttack(EntityManager entityManager, Entity owner) : base(entityManager, owner)
            {
                _delay = 200;
                _currentAngle = 135.0;
                _projectileInformation = new ProjectileInformation("PointedProjectile", 8, 16, 2.5f, 0, VectorMath.Degrees2Vector(_currentAngle));
                _origin = Static.Origin.BottomCenter;
                _angleOffset = 10;
                _count = 0;
            }

            public override void Transform()
            {
                if (_currentAngle < 135.0 || _currentAngle > 225.0)
                {
                    _angleOffset *= -1;
                }
                _currentAngle += _angleOffset;
                _projectileInformation.Direction = VectorMath.Degrees2Vector(_currentAngle);
                _count++;
            }
        }
    }
}
