using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.UI
{
    class MenuButton
    {
        private Rectangle _buttonBoundary;

        private Dictionary<string, Texture2D> _buttonTextures;
        private Texture2D _activeTexture;

        public MenuButton(int x, int y, int width, int height)
        {
            _buttonBoundary = new Rectangle(x, y, width, height);
        }

        public bool MouseWithenBounds(int x, int y)
        {
            return (x >= _buttonBoundary.X && x <= _buttonBoundary.X + _buttonBoundary.Width
                && y >= _buttonBoundary.Y && y <= _buttonBoundary.Y + _buttonBoundary.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw()
        }
    }
}
