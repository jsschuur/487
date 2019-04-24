using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;

namespace Take3.ECS.Scripts
{
    class HomingProjectile : Projectile
    {
        private Transform player;
        private Transform transform;
        private Velocity velocity;

        public float HomingTime { get; set; }
        private double startTime;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            transform = (Transform)GetComponent<Transform>();
            velocity = (Velocity)GetComponent<Velocity>();
            player = (Transform)(GameManager.GetObjectByTag("Player")).GetComponent<Transform>();
        }

        public override void Update(GameTime gameTime)
        {
            if (startTime == 0) startTime = gameTime.TotalGameTime.TotalMilliseconds;

            if(gameTime.TotalGameTime.TotalMilliseconds <= startTime + HomingTime)
            {
                var direction = player.Position - transform.Position;
                direction.Normalize();
                velocity.Direction = direction;
            }
        }
    }
}
