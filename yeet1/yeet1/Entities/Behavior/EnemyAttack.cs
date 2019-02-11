using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yeet1.Entities.Projectiles;
using yeet1.Utility;

namespace yeet1.Entities.Behavior
{
    abstract class EnemyAttack
    {
        protected Entity _owner;

  
        protected event EventHandler<EntityEventArgs> FireProjectile;


        protected double _delay;

        private double _startTime;
        private double _endTime;

        protected float _cooldown;
        protected float _duration;

        private bool _offCooldown;

        protected ProjectileInformation _projectileInformation;

        private double _lastAttackTime;

        protected Static.Origin _origin;

        public EnemyAttack(EntityManager entityManager, Entity owner)
        {
            _owner = owner;
            FireProjectile += entityManager.EntityEventHandler;
        }

        public void Update(GameTime gameTime)
        {
            if(_startTime == 0)
            {
                _startTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
            if(gameTime.TotalGameTime.TotalMilliseconds - _delay > _lastAttackTime)
            {
                Transform();
                _lastAttackTime = gameTime.TotalGameTime.TotalMilliseconds;
                FireProjectile?.Invoke(_owner, new EntityEventArgs("projectile", _origin, _projectileInformation));
            }
        }

        public abstract void Transform();
    }
}
