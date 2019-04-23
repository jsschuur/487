using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS
{
    public class Prefabrication
    {
        private IList<Component> components;

        public string Tag { get; set; }


        public List<Component> Components { get { return components.ToList(); } }

        public Prefabrication()
        {
            components = new List<Component>();
            var transform = (Transform)AddComponent<Transform>();
        }
       
        public Component AddComponent<T>() where T : Component, new()
        {
            Component c = (T)Activator.CreateInstance(typeof(T));
            components.Add(c);
            return c;
        }

        public Component GetComponent<T>() where T : Component
        {
            return components.OfType<T>().First();
        }

        public bool HasComponent<T>() where T : Component
        {
            return components.OfType<T>().Any();
        }

        public void AddComponent(Component c)
        {
            components.Add(c);
        }
    }
}
