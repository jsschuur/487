using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yeet1.Entities
{
    abstract class Entity
    {
        private bool _isActive = true;

        protected Texture2D _texture;
        
        protected Rectangle _destinationRectangle;
        protected Rectangle _sourceRectangle;

        protected Vector2 _textureCenter;
        protected double _rotation = 0;

        protected Vector2 _position;
        protected Vector2 _direction;

        protected float _speed;
        
        public Rectangle DestinationRectangle { get { return _destinationRectangle; } }
        public bool IsActive { get { return _isActive; } }

        public Entity(Texture2D texture, Vector2 origin)
        {
            this._texture = texture;
            this._position = origin;
            _textureCenter = new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position + _textureCenter, _sourceRectangle, Color.White, (float)_rotation, _textureCenter, 1.0f, SpriteEffects.None, 0f);
        }
        protected void UpdateSpritePosition()
        {
            _destinationRectangle.X = (int)_position.X;
            _destinationRectangle.Y = (int)_position.Y;
        }

        public void Kill()
        {
            _isActive = false;
        }

        public abstract void Update(GameTime gameTime);
    }
}
