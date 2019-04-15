using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS.Scripts
{
    class FastLightProjectile : Projectile
    {
        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            speed = 300;
            damage = 10;
        }
    }
}
