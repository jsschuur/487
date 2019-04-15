using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Take3.ECS.Collision;

namespace Take3.ECS.Scripts.Collision
{
    class EnemyCollision : Script
    {
        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            var collider = (Collider)GetComponent<Collider>();
            collider.OnCollision = OnCollision;
        }

        public override void OnCollision(GameObject collider)
        {
            if(collider.Tag == "playerprojectile")
            {

            }
        }
    }
}
