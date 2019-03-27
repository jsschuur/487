using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _487Game.Components
{
    class PositionComponent : Component
    {
        private Vector2 _position;

        public PositionComponent(Entity owner) : base(owner)
        {
            _componentType = "PositionComponent";
        }

        public PositionComponent(Entity owner, Vector2 origin) : base(owner)
        {
            _componentType = "PositionComponent";
            _position = origin;
        }

        public float X { get { return _position.X; } set { _position.X = value; } }
        public float Y { get { return _position.Y; } set { _position.Y = value; } }

        public Vector2 GetPosition { get { return _position; } }

        public void SetPosition(Vector2 origin)
        {
            _position = origin;
        }
        public void SetPosition(float x, float y)
        {
            _position.X = x;
            _position.Y = y;
        }
        public override void Update(GameTime gameTime) { }
    }
}
