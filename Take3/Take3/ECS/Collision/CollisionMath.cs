using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS.Collision
{
    static class CollisionMath
    {
        public static bool CheckCollision(Collider c1, Collider c2)
        {
            if(c1 is CircleCollider && c2 is CircleCollider)
            {
                return CheckCollision((CircleCollider)c1, (CircleCollider)c2);
            }
            else if (c1 is CircleCollider && c2 is BoxCollider)
            {
                return CheckCollision((CircleCollider)c1, (BoxCollider)c2);
            }
            else if (c1 is BoxCollider && c2 is CircleCollider)
            {
                return CheckCollision((BoxCollider)c1, (CircleCollider)c2);
            }
            else if (c1 is BoxCollider && c2 is BoxCollider)
            {
                return CheckCollision((BoxCollider)c1, (BoxCollider)c2);
            }
            return false;
        }

        public static bool CheckCollision(CircleCollider h1, CircleCollider h2)
        {
            var h1Position = h1.GetPosition();
            var h2Position = h2.GetPosition();

            return (Math.Pow((h1Position.X - h2Position.X), 2) + Math.Pow((h1Position.Y - h2Position.Y), 2) < Math.Pow((h1.Radius + h2.Radius), 2));
        }

        public static bool CheckCollision(BoxCollider h1, CircleCollider h2)
        {
     
            var deltaX = h2.Position.X - Math.Max(h1.Box.X, Math.Min(h2.Position.X, h1.Box.Right));
            var deltaY = h2.Position.Y - Math.Max(h1.Box.Y, Math.Min(h2.Position.Y, h1.Box.Bottom));
            return (deltaX * deltaX + deltaY * deltaY) < (h2.Radius * h2.Radius);
        }

        public static bool CheckCollision(CircleCollider h1, BoxCollider h2)
        {
            var deltaX = h1.Position.X - Math.Max(h2.Box.X, Math.Min(h1.Position.X, h2.Box.Right));
            var deltaY = h1.Position.Y - Math.Max(h2.Box.Y, Math.Min(h1.Position.Y, h2.Box.Bottom));
            return (deltaX * deltaX + deltaY * deltaY) < (h1.Radius * h1.Radius);
        }

        public static bool CheckCollision(BoxCollider r1, BoxCollider r2)
        {
            return !(r1.Box.Left > r2.Box.Left + r2.Box.Width) &&
                   !(r1.Box.Left + r1.Box.Width < r2.Box.Left) &&
                   !(r1.Box.Top > r2.Box.Top + r2.Box.Height) &&
                   !(r1.Box.Top + r1.Box.Height < r2.Box.Top);
        }

        private static float Clamp(float val, float min, float max)
        {
            if (val < min) return min;
            else return max;
        }
    }
}
