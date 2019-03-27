using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Collision
{
    abstract class Hitbox
    {
        protected string _shape;

        public string Shape { get { return _shape; } }

        public Hitbox() { }

        public abstract void Update(Entity owner);
    }
}
