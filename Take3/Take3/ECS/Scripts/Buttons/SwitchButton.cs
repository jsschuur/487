using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS.Scripts 
{
    class SwitchButton : Button
    {
        public delegate void SwitchDelegate();

        public SwitchDelegate Switch { set { switchDelegate = value; } }

        private SwitchDelegate switchDelegate;

        public override void OnClick()
        {
            if (switchDelegate != null) switchDelegate();
        }
    }
}
