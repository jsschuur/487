using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Utility
{
    class EntityEventArgs : EventArgs
    {
        public string EventType;

        public EntityEventArgs(string eventType)
        {
            EventType = eventType;
        }
    }
}
