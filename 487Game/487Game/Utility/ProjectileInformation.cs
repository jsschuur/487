using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Utility
{
    class ProjectileInformation
    { 
        public int Damage;

        public float Acceleration;
        public string TextureName;

        public int Width;
        public int Height;

        public float Speed;

        public ProjectileInformation() { }

        public ProjectileInformation(string textureName, int damage, float speed, int width, int height, float acceleration)
        {
            Acceleration = acceleration;
            TextureName = textureName;
            Speed = speed;
            Damage = damage;
            Height = height;
            Width = width;
        }
    }
}
