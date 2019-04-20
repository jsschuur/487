using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Take3.ECS.Scripts
{
    class Enemy : Script
    {
        public int Health { get; set; }

        public override void OnCollision(GameObject collider)
        {
            if(collider.Tag == "PlayerProjectile")
            {
                var colliderProjectile = (Projectile)collider.GetComponent<Projectile>();
                Health -= colliderProjectile.Damage;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if(Health <= 0)
            {
                Die();
            }
        }
    }
}
