using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;

namespace Take3.ECS.Scripts
{
    class DelayedHomingProjectile : Projectile
    {
        private SpriteRenderer playerRenderer;

        private Transform player;
        private Transform transform;

        private Velocity velocity;

        public float HomingTime { get; set; }
        public float DelayTime { get; set; }
        

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            playerRenderer = (SpriteRenderer)(GameManager.GetObjectByTag("Player")).GetComponent<SpriteRenderer>();
            transform = (Transform)GetComponent<Transform>();
            velocity = (Velocity)GetComponent<Velocity>();
            player = (Transform)(GameManager.GetObjectByTag("Player")).GetComponent<Transform>();
        }
 
        public override void Update(GameTime gameTime)
        {

            if (DelayTime <= gameTime.TotalGameTime.TotalMilliseconds)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds <= HomingTime + DelayTime)
                {
                    var direction = player.Position + playerRenderer.Sprite.GetCenter() - transform.Position;
                    direction.Normalize();
                    velocity.Direction = direction;
                }
            }
        }
    }
}