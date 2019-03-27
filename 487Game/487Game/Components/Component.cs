using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Components
{
    abstract class Component
    {
        protected Entity _owner;
        protected string _componentType;

        public string ComponentType { get { return _componentType; } }
        public Entity Owner { get { return _owner; } }


        public Component(Entity owner)
        {
            _owner = owner;
        }

        public abstract void Update(GameTime gameTime);
    }
}
