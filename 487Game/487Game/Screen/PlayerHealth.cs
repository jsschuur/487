using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _487Game.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _487Game.Screen
{
    class PlayerHealth : ScreenComponent
    {

        private HealthComponent _playerHealthComponent;

        public PlayerHealth(Texture2D texture, HealthComponent playerHealthComponent) : base(texture)
        {
            _playerHealthComponent = playerHealthComponent;
            _destinationRectangle.X = 23;
            _destinationRectangle.Y = 403;

            _destinationRectangle.Width = _sourceRectangle.Width = 194;
            _destinationRectangle.Height = _sourceRectangle.Height = 14;

        }

        public override void Update(GameTime gameTime)
        {
            float percent = (float)(_playerHealthComponent.Health / 100f);

            _destinationRectangle.Width = (int)(194 * percent);
        }
    }
}
