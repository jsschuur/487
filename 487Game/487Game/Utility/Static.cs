using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Static
{
    public enum Input
    {
        Left, Up, Right, Down, Space, None, Shift, Pause, ShiftUp
    }
    public enum Direction
    {
        Left, Right, Up, Down
    }

    public enum GameState
    {
        Paused, Playing, Menu
    }

    public enum Origin
    {
        TopCenter, BottomCenter, RightCenter, LeftCenter
    }
}
