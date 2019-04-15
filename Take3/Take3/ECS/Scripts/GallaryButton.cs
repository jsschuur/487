using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS.Scripts
{
    class GallaryButton : Button
    {
        private GameObject gameObject;
        private Gallery gallery;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
        }

        public void SetObject(GameObject obj)
        {

        }

        public override void OnClick()
        {
            base.OnClick();
        }
    }
}
