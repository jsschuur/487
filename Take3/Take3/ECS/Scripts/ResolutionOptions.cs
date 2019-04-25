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
    class ResolutionOptions : Script
    {
        private Tuple<int, int>[] resolutions;

        private TextRenderer textRenderer;

        private int currentIndex;
        private int numResolutions;

        public Tuple<int, int> SelectedResolution { get { return resolutions[currentIndex]; } }

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            currentIndex = 1;

            textRenderer = (TextRenderer)owner.AddComponent<TextRenderer>();
            textRenderer.Font = TextureManager.LoadFont("GameFont");
            textRenderer.TextColor = Color.White;

            resolutions = new Tuple<int, int>[]
            {
                Tuple.Create(640, 480),
                Tuple.Create(1280, 720),
                Tuple.Create(1920, 1080),
                Tuple.Create(2650, 1440),
                Tuple.Create(3840, 2160),
            };

            textRenderer.Text = resolutions[currentIndex].Item1.ToString() + "x" + resolutions[currentIndex].Item2.ToString();

            numResolutions = resolutions.Length;
 
            var switchLeftButton = (SwitchButton)GameManager.GetObjectByTag("LeftResolutionButton").GetComponent<SwitchButton>();
            var switchRightButton = (SwitchButton)GameManager.GetObjectByTag("RightResolutionButton").GetComponent<SwitchButton>();

            switchLeftButton.Switch = SwitchLeft;
            switchRightButton.Switch = SwitchRight;



        }

        public void SwitchLeft()
        {
            currentIndex--;
            if(currentIndex < 0)
            {
                currentIndex = numResolutions- 1;
            }
            textRenderer.Text = resolutions[currentIndex].Item1.ToString() + "x" + resolutions[currentIndex].Item2.ToString();
            Configs.SetResolution(resolutions[currentIndex]);
        }

        public void SwitchRight()
        {
            currentIndex++;
            if (currentIndex >= numResolutions)
            {
                currentIndex = 0;
            }
            Configs.SetResolution(resolutions[currentIndex]);
            textRenderer.Text = resolutions[currentIndex].Item1.ToString() + "x" + resolutions[currentIndex].Item2.ToString();
        }
    }
}
