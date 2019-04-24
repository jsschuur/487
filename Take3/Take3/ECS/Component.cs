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

        private bool isActive;
        public bool IsActive { get { return isActive; } set { isActive = value; } }

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
