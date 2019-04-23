using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.ECS;

namespace Take3.Utility
{
    public class UtilityMath
    {
        public static class OriginMath
        {
            public static Vector2 TopCenter(Rectangle rectangle1, Rectangle rectangle2)
            {
                return new Vector2(((rectangle1.X + (rectangle1.Width / 2)) - rectangle2.Width / 2), (rectangle1.Y - rectangle2.Height));
            }
            public static Vector2 BottomCenter(Rectangle rectangle1, Rectangle rectangle2)
            {
                return new Vector2(((rectangle1.X + (rectangle1.Width / 2)) - rectangle2.Width / 2), (rectangle1.Y + rectangle1.Height + rectangle2.Height));
            }
            public static Vector2 RightCenter(Rectangle rectangle1, Rectangle rectangle2)
            {
                return new Vector2((float)(rectangle1.X - rectangle2.Width), (float)(rectangle1.Y + rectangle1.Height - (rectangle2.Width / 2)));
            }
            public static Vector2 LeftCenter(Rectangle rectangle1, Rectangle rectangle2)
            {
                return new Vector2((float)(rectangle1.X + rectangle1.Width), (float)(rectangle1.Y + rectangle1.Height - (rectangle2.Width / 2)));
            }
            public static Vector2 Center(Rectangle rectangle1, Rectangle rectangle2)
            {
                return new Vector2((float)(rectangle1.X + rectangle1.Width / 2 - (rectangle2.Width / 2)), (float)(rectangle1.Y + rectangle1.Height - (rectangle2.Height / 2)));
            }
        }
        public static class VectorMath
        {
            public static Vector2 RotatePoint(Vector2 point, Vector2 origin, float angle)
            {
                var newX = Math.Cos(angle) * (point.X - origin.X) - Math.Sin(angle) * (point.Y - origin.Y) + origin.X;
                var newY = Math.Sin(angle) * (point.X - origin.X) + Math.Cos(angle) * (point.Y - origin.Y) + origin.Y;

                var newPoint = new Vector2((float)newX, (float)newY);
                return newPoint;
            }

            public static Vector2 Angle2Vector(double angle)
            {
                return new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle));
            }
            public static double Vector2Angle(Vector2 vector)
            {
                return Math.Atan2(vector.X, -vector.Y);
            }

            public static double Degrees2Radians(double degrees)
            {
                return degrees * (Math.PI / 180);
            }

            public static double Radians2Degrees(double radians)
            {
                return radians * (180 / Math.PI);
            }

            public static Vector2 Degrees2Vector(double degrees)
            {
                return new Vector2((float)Math.Sin(Degrees2Radians(degrees)), -(float)Math.Cos(Degrees2Radians(degrees)));
            }

            public static bool IsInRange(float from, float to, float angle)
            {
                while (to < from) to += 360;
                while (angle < from) angle += 360;
                return angle >= from && angle <= to;
            }
        }
    }
}
