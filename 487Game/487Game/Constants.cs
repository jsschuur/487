using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game
{
    public static class Constants
    {
        public const int TargetWidth = 1280;
        public const int TargetHeight = 720;

        public const int GameplayWindowX = (int)(TargetWidth * .05);
        public const int GameplayWindowY = (int)(TargetHeight * .05);

        public const int GameplayWindowWidth = (int)((TargetWidth * .5) - (TargetWidth * .05));
        public const int GameplayWindowHeight = (int)((TargetHeight) - (TargetHeight * .05));

    }
}
