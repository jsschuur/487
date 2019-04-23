using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;

namespace Take3.ECS.Scripts
{
    class ResumeButton : Button
    {
        public delegate void ResumeGame();

        private ResumeGame resume;

        public ResumeGame Resume { set { resume = value; } }

        public override void OnClick()
        {
            resume();
        }
    }
}
