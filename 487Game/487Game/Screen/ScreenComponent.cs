using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Screen
{
    abstract class ScreenComponent
    {
        private Texture2D _texture;

        protected Rectangle _sourceRectangle;
        protected Rectangle _destinationRectangle;

        public ScreenComponent(Texture2D texture)
        {
            _texture = texture;
            _sourceRectangle = new Rectangle();
            _destinationRectangle = new Rectangle();
        }

        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _destinationRectangle, _sourceRectangle, Color.White);
        }
    }
}
