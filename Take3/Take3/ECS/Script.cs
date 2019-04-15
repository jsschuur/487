using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS
{
    public class Script : Updatable
    {
        public virtual void OnClick() { }
        public virtual void OnCollision(GameObject collider) { }

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            if (owner.HasComponent<Collider>())
            {
                ((Collider)GetComponent<Collider>()).OnCollision = OnCollision;
            }
        }
    }
}
