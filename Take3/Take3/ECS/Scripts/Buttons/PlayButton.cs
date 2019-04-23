using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Take3.GameManagement;

namespace Take3.ECS.Scripts
{
    public class PlayButton : Button
    {
        public override void OnClick()
        {
            GameManager.SwitchState(State.Game);
        }
    }
}
