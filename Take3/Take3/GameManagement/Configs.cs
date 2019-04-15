using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.GameManagement
{
    class Configs
    {
        private Game game;

        public Configs(Game game)
        {
            this.game = game;
        }

        public void SetMouseVisible()
        {
            game.IsMouseVisible = true;
        }

        public void SetMouseInvisible()
        {
            game.IsMouseVisible = false;
        }
    }
}
