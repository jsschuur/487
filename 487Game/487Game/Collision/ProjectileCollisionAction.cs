using _487Game.Components;
using _487Game.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Collision
{
    class ProjectileCollisionAction : CollisionAction
    {
        private Entity _collider;

        public ProjectileCollisionAction(Entity collider)
        {
            _collider = collider;
        }

        public override void Action(Entity collider)
        {
            ((HealthComponent)collider.GetComponent("HealthComponent"))?.TakeDamage(((ProjectileDamageComponent)(_collider.GetComponent("ProjectileDamageComponent"))).Damage);
        }
    }
}
