using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _487Game.Screen
{
    class EmptyHealthBar : ScreenComponent
    {
        public EmptyHealthBar(Texture2D texture) : base(texture)
        {
            _sourceRectangle.Width = _destinationRectangle.Width = 200;
            _sourceRectangle.Height = _destinationRectangle.Height = 20;

            _destinationRectangle.X = 20;
            _destinationRectangle.Y = 400;
        }

        public override void Update(GameTime gameTime) { }
    }
}
