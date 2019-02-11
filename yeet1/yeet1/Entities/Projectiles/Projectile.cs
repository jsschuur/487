using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using yeet1.Utility;

namespace yeet1.Entities.Projectiles
{
    class Projectile : Entity
    {
        private float _acceleration;
        private float _speed;
        private Vector2 _direction;


        public Projectile(Texture2D texture, Vector2 origin, ProjectileInformation projectileInformation) : base(texture, origin)
        {
            _acceleration = projectileInformation.Acceleration;
            _direction = projectileInformation.Direction;
            _sourceRectangle = new Rectangle(0, 0, projectileInformation.Width, projectileInformation.Height);
            _speed = projectileInformation.Speed;
            _rotation = VectorMath.Vector2Angle(projectileInformation.Direction);
        }

        public override void Update(GameTime gameTime)
        {
            _position.X += (_speed * _direction.X * (float)gameTime.ElapsedGameTime.TotalSeconds);
            _position.Y += (_speed * _direction.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);

            _speed += _acceleration;
        }
    }
}
