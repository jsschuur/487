using _487Game.EnemyBehavior;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Components.EnemyBehaviorComponents
{
    class EnemyAttackComponent : Component
    {
        private Vector2 _direction;
        private EnemyAttack _attack;

        private float _cooldown;

        private double _lastTimeFired;

        public EnemyAttackComponent(Entity owner, float cooldown) : base(owner)
        {
            _componentType = "EnemyAttackComponent";
            _cooldown = cooldown;
            _direction = new Vector2(0, 1);
        }

        public EnemyAttackComponent(Entity owner, EnemyAttack attack) : base(owner)
        {
            _componentType = "EnemyAttackComponent";
            _attack = attack;
            _direction = new Vector2(0, 1);
        }

        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > _attack.Cooldown + _lastTimeFired)
            {
                ((ProjectileComponent)_owner.GetComponent("ProjectileComponent")).Fire(_attack.ProjectileInformation, _attack.Direction);
                _attack.UpdateProjectile();
                _lastTimeFired = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }
    }
}
