using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;
using Take3.Screen;

namespace Take3.Utility
{
    public sealed class Configs
    {
        private static Configs configs = new Configs();

        private static Game _game;
        private static GraphicsDeviceManager _graphics;

        static Configs() { }
        private Configs() { }

        public static void Initialize(Game game, GraphicsDeviceManager graphics)
        {
            _game = game;
            _graphics = graphics;
        }

        public static void SetResolution(Tuple<int, int> resolution)
        {
            _graphics.PreferredBackBufferWidth = resolution.Item1;
            _graphics.PreferredBackBufferHeight = resolution.Item2;
            Resolution.Update(_graphics);
        }

        public static void SetPlayingTimeScale(int option)
        {
            float speedScale = .7f + option  / 10f;
            GameManager.SetPlayingTimeScale(speedScale);
        }

        public static void SetMouseInvisible()
        {
            _game.IsMouseVisible = false;
        }

        public static void SetMouseVisible()
        {
            _game.IsMouseVisible = true;
        }
    }
}
