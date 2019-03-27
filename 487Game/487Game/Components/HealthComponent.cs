using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _487Game.Components
{
    class HealthComponent : Component
    {
        private int _health;
        
        public int Health { get { return _health; } set { _health = value; } }

        public HealthComponent(Entity owner, int health) : base(owner)
        {
            _componentType = "HealthComponent";
            _health = health;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }
        public override void Update(GameTime gameTime)
        {
            if(_health <= 0)
            {
                _owner.Kill();
            }
        }
    }
}
