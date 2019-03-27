using _487Game.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Entities
{
    class ProjectileEventArgs : EventArgs
    {
        public ProjectileInformation NewProjectileInformation;
        public Vector2 Direction;

        public ProjectileEventArgs(ProjectileInformation projectileInformation, Vector2 direction)
        {
            NewProjectileInformation = projectileInformation;
            Direction = direction;
        }
    }
}
