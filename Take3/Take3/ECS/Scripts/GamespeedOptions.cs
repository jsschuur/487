using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;
using Take3.Utility;

namespace Take3.ECS.Scripts
{
    class GamespeedOptions : Script
    {
        private int speed;
        private int numSpeeds = 5;

        private TextRenderer textRenderer;


        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            speed = 3;
            textRenderer = (TextRenderer)AddComponent<TextRenderer>();

            textRenderer.Font = TextureManager.LoadFont("GameFont");
            textRenderer.TextColor = Color.White;
            textRenderer.Text = speed.ToString();

            var switchLeftButton = (SwitchButton)GameManager.GetObjectByTag("LeftSpeedButton").GetComponent<SwitchButton>();
            var switchRightButton = (SwitchButton)GameManager.GetObjectByTag("RightSpeedButton").GetComponent<SwitchButton>();

            switchLeftButton.Switch = SwitchLeft;
            switchRightButton.Switch = SwitchRight;
        }

        public void SwitchLeft()
        {
            speed--;
            if (speed <= 0) speed = numSpeeds;
            textRenderer.Text = speed.ToString();
            Configs.SetPlayingTimeScale(speed);
        }

        public void SwitchRight()
        {
            speed++;
            if (speed > numSpeeds) speed = 1;
            textRenderer.Text = speed.ToString();
            Configs.SetPlayingTimeScale(speed);

        }
            
    }
}
