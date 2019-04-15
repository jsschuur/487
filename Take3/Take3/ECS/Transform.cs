using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS
{
    public class Transform : Component
    {

        private Vector2 position;

        public Vector2 Position { get { return position; } set { position = value; } }
        
        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }

        public float Rotation { get; set; }
        
        public float Scale { get; set; }

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            Scale = 1;
        }
    }
}
