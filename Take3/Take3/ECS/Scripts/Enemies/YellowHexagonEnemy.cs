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
    class YellowHexagonEnemy : Enemy
    {

        private Prefabrication projectile;

        private Transform transform;
        private SpriteRenderer renderer;
        private SpriteRenderer projectileRenderer;

        private float cooldown;
        private double lastAttackTime;

        private int numProjectiles = 3;
        private int projectileWave = 0;
        private Vector2 range;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            projectile = GameManager.GetPrefab("RedProjectile");

            transform = (Transform)GetComponent<Transform>();
            renderer = (SpriteRenderer)GetComponent<SpriteRenderer>();
            projectileRenderer = (SpriteRenderer)projectile.GetComponent<SpriteRenderer>();

            range = new Vector2(135, 225);

            health = 5;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (gameTime.TotalGameTime.TotalMilliseconds >= cooldown + lastAttackTime)
            {
                var offset = VectorMath.Degrees2Radians((range.Y - range.X) / (numProjectiles + 1));
                var currentAngle = VectorMath.Degrees2Radians(range.X);


                for (int i = 0; i < numProjectiles; i++)
                {
                    cooldown = 300;
                    currentAngle += offset;
                    var projectileInstance = GameManager.Instantiate(projectile, transform.Position + renderer.Sprite.GetCenter() - projectileRenderer.Sprite.GetCenter());
                    projectileInstance.Tag = "Enemy" + projectileInstance.Tag;

                    var delayedHoming = (DelayedHomingProjectile)projectileInstance.GetComponent<DelayedHomingProjectile>();
                    delayedHoming.DelayTime = 400;
                    delayedHoming.HomingTime = 300;

                    var velocity = (Velocity)projectileInstance.GetComponent<Velocity>();
                    velocity.Direction = VectorMath.Angle2Vector(currentAngle);
                }
                projectileWave++;

                if (projectileWave > 2)
                {
                    projectileWave = 0;
                    cooldown = 1250;
                }

                lastAttackTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }
    }
}