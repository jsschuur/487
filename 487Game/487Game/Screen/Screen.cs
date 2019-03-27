using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Screen
{
    class Screen
    {

        private List<ScreenComponent> _screenComponents;

        public Screen()
        {
            _screenComponents = new List<ScreenComponent>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var c in _screenComponents)
            {
                c.Update(gameTime);
            }
        }

        public void AddComponent(ScreenComponent component)
        {
            _screenComponents.Add(component);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var c in _screenComponents)
            {
                c.Draw(spriteBatch);
            }
        }
    }
}
