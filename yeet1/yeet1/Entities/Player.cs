﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using yeet1.Entities.Projectiles;
using yeet1.Input;
using yeet1.Texture;
using yeet1.Utility;

namespace yeet1.Entities
{
    class Player : EventEntity
    {

        private const int _spriteWidth = 64;
        private const int _spriteHeight = 64;

        private int _cooldown = 100;
        private double _timeLastFired;

        private float _speed;
        private Vector2 _direction;

        public ProjectileInformation _currentProjectile;



        public Player(Texture2D texture, Vector2 origin, EntityManager entityManager, KeyboardManager keyboardManager) : base(texture, origin, entityManager)
        {
            keyboardManager.NewInput += NewKeyboardInput;
            _sourceRectangle = new Rectangle(0, 0, _spriteWidth, _spriteHeight);

            _speed = 250;

            _currentProjectile = new ProjectileInformation("DefaultProjectile", 8, 16, 300, 0, new Vector2(0, -1));
        }

        public override void Update(GameTime gameTime)
        {
            if(_direction.X != 0 || _direction.Y != 0)
            {
                _direction.Normalize();
            }
            

            _position.X += (_direction.X * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            _position.Y += (_direction.Y * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
           

            _direction.X = 0;
            _direction.Y = 0;
            _speed = 250;
        }

        private void FireProjectile(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalMilliseconds - _cooldown > _timeLastFired)
            {
                SendEvent(this, new EntityEventArgs("projectile", Static.Origin.TopCenter, _currentProjectile));
                _timeLastFired = gameTime.TotalGameTime.TotalMilliseconds;       
            }
        }

        private void NewKeyboardInput(object sender, NewInputEventArgs e)
        {
            switch (e.Input)
            {
                case Static.Input.Left:
                    _direction.X -= 1;
                    break;
                case Static.Input.Right:
                    _direction.X += 1;
                    break;
                case Static.Input.Up:
                    _direction.Y -= 1;
                    break;
                case Static.Input.Down:
                    _direction.Y += 1;
                    break;
                case Static.Input.None: 
                    break;
                case Static.Input.Shift:
                    _speed /= 2;
                    break;
                case Static.Input.Space:
                    FireProjectile(e.gameTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

}