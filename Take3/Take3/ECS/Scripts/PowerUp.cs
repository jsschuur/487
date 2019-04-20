using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS.Scripts
{
    class PowerUp : Projectile
    {
        protected Prefabrication projectile;

        public Prefabrication Projectile { get { return projectile; } }

        public override void OnCollision(GameObject collider)
        {
            if(collider.Tag == "Player")
            {
                var player = (Player)collider.GetComponent<Player>();
                player.EquipPowerUp(this);
                Die();
            }
        }
    }
}
