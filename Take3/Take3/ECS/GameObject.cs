using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS
{
    public class GameObject 
    {
        private IList<Component> _components;
        private bool isAlive;
        private uint _id;

        public string Tag { get; set; }
        public bool IsAlive { get { return isAlive; } }
        public bool IsActive { get; set; }

        public GameObject()
        {
            _components = new List<Component>();
            AddComponent<Transform>();
            isAlive = true;
        }

        public void Die()
        {
            isAlive = false;
        }

        public Component AddComponent<T>() where T : Component, new()
        {
            Component c = (T)Activator.CreateInstance(typeof(T));
            c.Initialize(this);
            _components.Add(c);
            return c;
        }

        public void AddComponent(Component c)
        {
            c.Initialize(this);
            _components.Add(c);
        }

        public Component GetComponent<T>() where T : Component
        {
            return _components.OfType<T>().First(); 
        }

        public bool HasComponent<T>() where T : Component
        {
            return _components.OfType<T>().Any();
        }

        public List<Component> GetComponents<T>() where T : Component
        {
            return _components.OfType<T>().ToList<Component>();
        }
    }
}
