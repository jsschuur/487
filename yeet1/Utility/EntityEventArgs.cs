using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yeet1.Entities.Projectiles;

namespace yeet1.Utility
{
    class EntityEventArgs : EventArgs
    {
        public string Type;
        public Static.Origin Origin;
        public ProjectileInformation projectileInformation;

        public EntityEventArgs(string type, Static.Origin origin)
        {
            this.Type = type;
            this.Origin = origin;
            projectileInformation = null;
        }
        public EntityEventArgs(string type, Static.Origin origin, ProjectileInformation projectileInformation)
        {
            this.Type = type;
            this.Origin = origin;
            this.projectileInformation = projectileInformation;
        }
    }
}
