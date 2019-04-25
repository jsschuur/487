using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS.Scripts
{
    class Projectile : Script
    {
        public override void OnCollision(GameObject collider)
        {
            if(collider.Tag == "ProjectileBoundary" || collider.Tag == "Player" || collider.Tag == "Enemy")
            {
                Die();
            }
        }
    }
}
