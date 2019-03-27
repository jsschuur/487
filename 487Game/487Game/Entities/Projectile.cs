using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Entities
{
    class Projectile : Entity
    {
        private int _damage;

        public int Damage { get { return _damage; } }

        public Projectile(uint id, string type, int damage) : base(id, type)
        {
            _damage = damage;
        }
    }
}
