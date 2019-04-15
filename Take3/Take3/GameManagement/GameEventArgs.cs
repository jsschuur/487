using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.GameManagement
{
    public class GameEventArgs : EventArgs
    {
        public string Event { get; private set; } 

        public GameEventArgs(string gameEvent)
        {
            Event = gameEvent;
        }
    }
}
