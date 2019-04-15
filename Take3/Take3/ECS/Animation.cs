using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS
{
    class Animation
    {
        public int NumFrames { get; set; }
        public float Delay { get; set; }
        public int YOffset { get; set; }

        public Animation() { }
        public Animation(int numFrames, float delay, int yOffset)
        {
            NumFrames = numFrames;
            Delay = delay;
            YOffset = yOffset;
        }
    }
}
