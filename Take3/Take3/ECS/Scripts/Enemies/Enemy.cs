using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Take3.GameManagement;

namespace Take3.ECS.Scripts
{
    class Enemy : Script
    {
        protected int health { get; set; }
        
        public override void OnCollision(GameObject collider)
        {
            if(collider.Tag == "PlayerProjectile")
            {
                health--;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if(health <= 0)
            {
                Die();
            }
        }
    }
}
