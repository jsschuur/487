using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Take3.GameManagement;

namespace Take3.ECS.Scripts
{
    class GreenSquareEnemy : Enemy
    {
        private Transform transform;
        private Transform player;

        private SpriteRenderer renderer;

        private Prefabrication projectile;

        private float cooldown;
        private double lastAttackTime;

        private int burstCounter;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            transform = (Transform)GetComponent<Transform>();
            renderer = (SpriteRenderer)GetComponent<SpriteRenderer>();

            player = (Transform)GameManager.GetObjectByTag("Player").GetComponent<Transform>();

            projectile = GameManager.GetPrefab("PinkSquareProjectile");

            health = 5;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(gameTime.TotalGameTime.TotalMilliseconds >= cooldown + lastAttackTime)
            {
                cooldown = 200;
                var direction = player.Position - transform.Position;
                direction.Normalize();
                var projectileInstance = GameManager.Instantiate(projectile, transform.Position + renderer.Sprite.GetCenter());
                projectileInstance.Tag = "Enemy" + projectileInstance.Tag;
                var velocityInstance = (Velocity)projectileInstance.GetComponent<Velocity>();
                velocityInstance.Direction = direction;
                burstCounter++;

                if(burstCounter > 6)
                {
                    cooldown = 1000;
                    burstCounter = 0;
                }

                lastAttackTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }
    }
}
