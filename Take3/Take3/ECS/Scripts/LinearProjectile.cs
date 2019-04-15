using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Take3.ECS.Scripts
{
    class LinearProjectile : Script
    {
        public float Speed { get; set; }
        public Vector2 Direction { get; set; }

        public Transform _transform;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            _transform = (Transform)GetComponent<Transform>();
        }

        public override void Update(GameTime gameTime)
        {
            _transform.Position += Direction * Speed;
            _transform.Rotation = (float)Utility.UtilityMath.VectorMath.Vector2Angle(Direction);
        }
    }
}
