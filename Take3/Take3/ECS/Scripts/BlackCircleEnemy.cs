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
    class BlackCircleEnemy : Enemy
    {
        private Prefabrication projectile;

        private Transform transform;
        private SpriteRenderer renderer;
        private SpriteRenderer projectileRenderer;



        private float cooldown;
        private double lastAttackTime;


        private int projectileWave;
        private Vector2 range;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            projectile = GameManager.GetPrefab("BlueProjectile");

            transform = (Transform)GetComponent<Transform>();
            renderer = (SpriteRenderer)GetComponent<SpriteRenderer>();
            projectileRenderer = (SpriteRenderer)projectile.GetComponent<SpriteRenderer>();

            range = new Vector2(135, 225);

            health = 3;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(gameTime.TotalGameTime.TotalMilliseconds >= cooldown + lastAttackTime)
            {
                cooldown = 200;
                var numProjectiles = projectileWave + 2;
                var offset = VectorMath.Degrees2Radians((range.Y - range.X) / (numProjectiles + 1));
                var currentAngle = VectorMath.Degrees2Radians(range.X);

                for(int i = 0; i < numProjectiles; i++)
                {
                    currentAngle += offset;
                    var projectileInstance = GameManager.Instantiate(projectile, transform.Position + renderer.Sprite.GetCenter() - projectileRenderer.Sprite.GetCenter());
                    projectileInstance.Tag = "Enemy" + projectileInstance.Tag;
                    var velocity = (Velocity)projectileInstance.GetComponent<Velocity>();

                    velocity.Direction = VectorMath.Angle2Vector(currentAngle + transform.Rotation);
                    var projectileScriptInstance = (Projectile)projectileInstance.GetComponent<Projectile>();
                    projectileScriptInstance.Damage = 10;
                }

                projectileWave++;

                if(projectileWave > 3)
                {
                    projectileWave = 0;
                    cooldown = 1000;
                }

                lastAttackTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }
    }
}
