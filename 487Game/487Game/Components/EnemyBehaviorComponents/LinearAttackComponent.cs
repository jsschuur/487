using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _487Game.Components.EnemyBehaviorComponents
{
    class LinearAttackComponent : Component
    {
        private Vector2 _direction;

        public LinearAttackComponent(Entity owner, Vector2 direction) : base(owner)
        {
            _componentType = "LinearAttackComponent";
            _direction = direction;
        }

        public override void Update(GameTime gameTime)
        {
             
        }
    }
}
