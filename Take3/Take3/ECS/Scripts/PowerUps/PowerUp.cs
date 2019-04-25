using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS.Scripts
{
    abstract class PowerUp : Projectile
    {
        protected Prefabrication projectile;
        protected float cooldown;

        public Prefabrication Projectile { get { return projectile; } }
        public float Cooldown { get { return cooldown; } }

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            var velocity = (Velocity)GetComponent<Velocity>();
            velocity.Direction = new Vector2(0, 1);
        }
    }
}
