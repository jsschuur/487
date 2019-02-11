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

        private Texture2D _texture;
        private  Vector2 _textureCenter;

        protected double _rotation = 0;
        protected Vector2 _position;


        protected Rectangle _sourceRectangle;
     
        public bool IsActive { get { return _isActive; } }

        
        public Vector2 Position { get { return _position; } set { _position = value; } }

        public float X { get { return _position.X; } set { _position.X = value; } }
        public float Y { get { return _position.Y; } set { _position.Y = value; } }

        public int Width { get { return _texture.Width; } }
        public int Height { get { return _texture.Height; } }


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
        public void Kill()
        {
            _isActive = false;
        }

        public abstract void Update(GameTime gameTime);
    }
}
