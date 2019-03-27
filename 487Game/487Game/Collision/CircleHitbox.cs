using _487Game.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Collision
{
    class CircleHitbox : Hitbox
    {
        private float _radius;
        private Vector2 _center;

        public float Radius { get { return _radius; } }

        public float X { get { return _center.X; } }
        public float Y { get { return _center.Y; } }

        public CircleHitbox(float radius)
        {
            _shape = "circle";
            _radius = radius;
        }

        public override void Update(Entity owner)
        {
            Vector2 ownerPos = ((PositionComponent)owner.GetComponent("PositionComponent")).GetPosition;
            Rectangle ownerRect = ((SpriteComponent)owner.GetComponent("SpriteComponent")).GetDrawRectangle;

            _center.X = ownerPos.X + (ownerRect.Width / 2);
            _center.Y = ownerPos.Y + (ownerRect.Height / 2);
        }
    }
}
