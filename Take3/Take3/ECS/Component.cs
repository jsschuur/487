using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS
{
    public abstract class Component
    {
        private GameObject owner;

        public Component() { }

        public Component(Component original) { }
        
        public virtual void Initialize(GameObject owner)
        {
            this.owner = owner;
        }

        protected Component GetComponent<T>() where T : Component
        {
            return owner.GetComponent<T>();
        }

        protected void Die()
        {
            owner.Die();
        }

        protected bool HasComponent<T>() where T : Component
        {
            return owner.HasComponent<T>();
        }
    }
}
