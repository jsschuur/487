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
                return new Rectangle((int)(transform.Position.X + buffer), 
                                     (int)(transform.Position.Y + buffer), 
                                     (int)(size.X - buffer * 2), 
                                     (int)(size.Y - buffer * 2));
            }
        }

        public Rectangle GetBox()
        {
            return new Rectangle((int)(transform.Position.X + buffer),
                                 (int)(transform.Position.Y + buffer),
                                 (int)(size.X * transform.Scale - buffer * 2),
                                 (int)(size.Y * transform.Scale - buffer * 2));
        }
    }
}
