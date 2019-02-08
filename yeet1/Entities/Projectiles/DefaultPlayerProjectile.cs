using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using yeet1.Texture;

namespace yeet1.Entities.Projectiles
{
    class DefaultPlayerProjectile : Entity
    {
        private const int _spriteWidth = 8;
        private const int _spriteHeight = 16;

        private const int _destinationWidth = 8;
        private const int _destinationHeight = 16;

        private const string _textureName = "DefaultProjectile";

        public DefaultPlayerProjectile(Texture2D texture, Vector2 origin) : base(texture, origin)
        {
            _texture = texture;

            _destinationRectangle = new Rectangle(0, 0, _destinationWidth, _destinationHeight);
            _sourceRectangle = new Rectangle(0, 0, _spriteWidth, _spriteHeight);

            _speed = 5;
            _direction = new Vector2(0, -1);
        }

        public override void Update(GameTime gameTime)
        {
            _position.X += (_direction.X * _speed);
            _position.Y += (_direction.Y * _speed);

            UpdateSpritePosition();
        }
    }
}
