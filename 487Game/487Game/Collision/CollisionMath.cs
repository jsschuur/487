using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Collision
{
    static class CollisionMath
    {
        public static bool CheckCollision(Hitbox h1, Hitbox h2)
        {
            if (h1.Shape == "circle" && h2.Shape == "circle")
            {
                return CheckCollision((CircleHitbox)h1, (CircleHitbox)h2);
            }
            return false;
        }

        private static bool CheckCollision(CircleHitbox h1, CircleHitbox h2)
        {
            if (Math.Pow((h1.X - h2.X), 2) + Math.Pow((h1.Y - h2.Y), 2) <= Math.Pow((h1.Radius + h2.Radius), 2))
            {
                return true;
            }
            return false;
        }
    }
}
