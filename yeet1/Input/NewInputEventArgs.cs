using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yeet1.Input
{
    class NewInputEventArgs : EventArgs
    {
        public Static.Input Input;
        public GameTime gameTime;

        public NewInputEventArgs(Static.Input input, GameTime gameTime)
        {
            this.Input = input;
            this.gameTime = gameTime;
        }
    }
}
