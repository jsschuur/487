using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.Screen
{
    public static class Resolution
    {
        private static int preferredBackBufferWidth;
        private static int preferredBackBufferHeight;

        public static Vector2 VirtualScreen = new Vector2(1280, 720);

        public static Matrix ScaleMatrix;

        public static Vector2 Scale;

        public static void Update(GraphicsDeviceManager graphics)
        {
            preferredBackBufferWidth = graphics.PreferredBackBufferWidth;
            float widthScale = preferredBackBufferWidth / VirtualScreen.X;

            preferredBackBufferHeight = graphics.PreferredBackBufferHeight;
            float heightScale = preferredBackBufferHeight / VirtualScreen.Y;

            ScaleMatrix = Matrix.CreateScale(new Vector3(widthScale, heightScale, 1));
            Scale = new Vector2(preferredBackBufferWidth / VirtualScreen.X, preferredBackBufferHeight / VirtualScreen.Y);

            graphics.ApplyChanges();
        }
    }
}
