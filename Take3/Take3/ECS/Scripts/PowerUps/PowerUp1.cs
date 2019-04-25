using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;

namespace Take3.ECS.Scripts
{
    class PowerUp1 : PowerUp
    {
        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            cooldown = 150;
            projectile = GameManager.GetPrefab("PurpleDiamondProjectile");
        }
    }
}
