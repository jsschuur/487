using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Take3.GameManagement;
using static Take3.Utility.UtilityMath;

namespace Take3.ECS.Scripts
{
    class GreenSquareEnemy : Enemy
    {

        private Prefabrication projectile;

        private Transform transform;
        private SpriteRenderer renderer;
        private SpriteRenderer projectileRenderer;

        private float cooldown = 500;
        private double lastAttackTime;

        private int projectileWave;
        private Vector2 range;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            projectile = GameManager.GetPrefab("GreenProjectile");

            transform = (Transform)GetComponent<Transform>();
            renderer = (SpriteRenderer)GetComponent<SpriteRenderer>();
            projectileRenderer = (SpriteRenderer)projectile.GetComponent<SpriteRenderer>();

            range = new Vector2(135, 225);

            health = 3;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (gameTime.TotalGameTime.TotalMilliseconds >= cooldown + lastAttackTime)
            {

                cooldown = 100;
                var projectileInstance = GameManager.Instantiate(projectile, transform.Position + renderer.Sprite.GetCenter() - projectileRenderer.Sprite.GetCenter());

                var homing = (HomingProjectile)projectileInstance.GetComponent<HomingProjectile>();
                homing.HomingTime = 500;

                projectileInstance.Tag = "Enemy" + projectileInstance.Tag;

                projectileWave++;

                if (projectileWave > 4)
                {
                    projectileWave = 0;
                    cooldown = 1000;
                }

                lastAttackTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }
    }
}

