using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yeet1.Entities.Projectiles
{
    class ProjectileInformation
    {
        public Vector2 Direction;

        public float Acceleration;
        public string TextureName;

        public float Speed;

        public int Width;
        public int Height;

        public ProjectileInformation() { }

        public ProjectileInformation( string textureName, int width, int height, float speed, float acceleration, Vector2 direction)
        {
            Acceleration = acceleration;
            TextureName = textureName;
            Speed = speed;
            Width = width;
            Height = height;
            Direction = direction;
        }
    }
}
