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

        public bool IsActive { get { return owner.IsActive; } set { owner.IsActive = value; } }

        public Component() { }

        public virtual void Initialize(GameObject owner)
        {
            this.owner = owner;
            IsActive = true;
        }

        protected Component GetComponent<T>() where T : Component
        {
            return owner.GetComponent<T>();
        }

        protected Component AddComponent<T>() where T : Component, new()
        {
            return owner.AddComponent<T>();
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
