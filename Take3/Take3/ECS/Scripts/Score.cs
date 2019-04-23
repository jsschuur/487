using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Take3.ECS.Scripts
{
    class Score : Script
    {

        private int score;
        private TextRenderer textRenderer;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            textRenderer = (TextRenderer)owner.AddComponent<TextRenderer>();

            textRenderer.Font = TextureManager.LoadFont("GameFont");
            textRenderer.TextColor = Color.CadetBlue;
            textRenderer.Text = "Score: " + score;

        }

        public void AddScore(int score)
        {
            this.score += score * 5;
            textRenderer.Text = "Score: " + this.score;
        }

    }
}
