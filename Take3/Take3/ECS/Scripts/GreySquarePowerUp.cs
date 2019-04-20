using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;

namespace Take3.ECS.Scripts
{
    class GreySquarePowerUp : PowerUp
    {

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            Damage = 20;
            projectile = GameManager.GetPrefab("GreySquareProjectile");
        }
    }
}
