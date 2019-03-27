using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Collision
{
    abstract class CollisionAction
    {
        public abstract void Action(Entity collider);
    }
}
