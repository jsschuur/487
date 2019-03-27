using _487Game.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game
{
    class Entity
    {
        private uint _id;
        private string _type;
        private bool _isAlive;

        private Dictionary<string, Component> _components;

        public uint Id { get { return _id; } }
        public string Type { get { return _type; } }
        public bool IsAlive { get { return _isAlive; } }

        public Entity(uint id, string type)
        {
            _id = id;
            _type = type;
            _isAlive = true;
            _components = new Dictionary<string, Component>(); 
        }

        public void Kill()
        {
            _isAlive = false;
        }

        public Component GetComponent(string component)
        {
            _components.TryGetValue(component, out Component c);
            return c;
        }

        public void AddComponent(Component component)
        {
            if(!_components.ContainsKey(component.ComponentType))
            {
                _components.Add(component.ComponentType, component);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(var c in _components.Values)
            {
                c.Update(gameTime);
            }
        }
    }
}
