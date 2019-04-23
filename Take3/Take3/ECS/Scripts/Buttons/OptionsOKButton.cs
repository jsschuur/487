using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;
using Take3.Utility;

namespace Take3.ECS.Scripts 
{
    class OptionsOkButton : Button
    {
        public override void OnClick()
        {
            var resolutionOptions = (ResolutionOptions)GameManager.GetObjectByTag("ResolutionOptions").GetComponent<ResolutionOptions>();
            Configs.SetResolution(resolutionOptions.SelectedResolution);
            var speedOptions = (GamespeedOptions)GameManager.GetObjectByTag("GamespeedOptions").GetComponent<GamespeedOptions>();
            Configs.SetPlayingTimeScale(speedOptions.Speed);

            GameManager.SwitchState(State.Menu);
        }
    }
}
