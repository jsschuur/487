using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _487Game.Components
{
    class ProjectileDamageComponent : Component
    {
        private int _damage;

        public int Damage { get { return _damage; } }

        public ProjectileDamageComponent(Entity owner, int damage) : base(owner)
        {
            _componentType = "ProjectileDamageComponent";
            _damage = damage;
        }

        public override void Update(GameTime gameTime) { }
    }
}
