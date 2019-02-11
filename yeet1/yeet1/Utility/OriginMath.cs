using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yeet1.Utility
{
    static class OriginMath
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
    }
    static class VectorMath
    {
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

        public static Vector2 Degrees2Vector(double degrees)
        {
            return new Vector2((float)Math.Sin(Degrees2Radians(degrees)), -(float)Math.Cos(Degrees2Radians(degrees)));
        }
    }
}
