using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Collision
{
    class EntityCollisionAction : CollisionAction
    {
        public EntityCollisionAction() { }

        public override void Action(Entity collider)
        {
            collider.Kill();
        }
    }
}
