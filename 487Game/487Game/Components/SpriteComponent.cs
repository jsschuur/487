using _487Game.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _487Game.Utility.UtilityMath;

namespace _487Game.Components
{
    class SpriteComponent : Component
    {
        private Texture2D _texture;

        private Rectangle _sourceRectangle;
        private Rectangle _destinationRectangle;

        private float _rotation;

        private SpriteEffects SpriteEffects;

        bool _rotatable;

        public Rectangle GetDrawRectangle { get { return _destinationRectangle; } }

        public SpriteComponent(Entity owner, Texture2D texture, int width, int height, bool rotatable) : base(owner)
        {
            _componentType = "SpriteComponent";

            _texture = texture;
            _destinationRectangle = new Rectangle(0, 0, width, height);
            _sourceRectangle = new Rectangle(0, 0, width, height);

            _rotatable = rotatable;

            SpriteEffects = SpriteEffects.None;
        }

        public void SwapFrames(int index, int yOffset)
        {
            _sourceRectangle.X = index * _sourceRectangle.Width;
            _sourceRectangle.Y = yOffset * _sourceRectangle.Height;
        }

        public override void Update(GameTime gameTime)
        {
            _destinationRectangle.X = (int)((PositionComponent)_owner?.GetComponent("PositionComponent")).X + _destinationRectangle.Width / 2;
            _destinationRectangle.Y = (int)((PositionComponent)_owner?.GetComponent("PositionComponent")).Y + _destinationRectangle.Height / 2;

            if(_rotatable)
            {
                _rotation = (float)VectorMath.Vector2Angle(((MovementComponent)_owner?.GetComponent("MovementComponent")).Direction);

                if (VectorMath.IsInRange((float)Math.PI, 0, _rotation))
                {
                    SpriteEffects = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    SpriteEffects = SpriteEffects.None;
                }
            }
                
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _destinationRectangle, _sourceRectangle, Color.White, _rotation, 
                new Vector2(_sourceRectangle.Width / 2, _sourceRectangle.Height / 2), SpriteEffects, 0f);
        }
    }
}
