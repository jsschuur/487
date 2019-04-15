using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS
{
    class BoxCollider : Collider
    {
        public Rectangle Box
        {
            get
            {
                return new Rectangle((int)(_transform.Position.X + Buffer), 
                                     (int)(_transform.Position.Y + Buffer), 
                                     (int)(size.X - Buffer * 2), 
                                     (int)(size.Y - Buffer * 2));
            }
        }
    }
}
