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
    class RedCircleEnemy : Enemy
    {

        private Prefabrication projectile;

        private Transform transform;
        private Renderer renderer;
        private Renderer projectileRenderer;

        private float currentAngle;
        private float offset = .3f;

        private float cooldown = 1000;
        private double lastAttackTime;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            projectile = GameManager.GetPrefab("PurpleDiamondProjectile");

            transform = (Transform)GetComponent<Transform>();
            renderer = (Renderer)GetComponent<Renderer>();
            projectileRenderer = (Renderer)projectile.GetComponent<Renderer>();

            Health = 20;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(gameTime.TotalGameTime.TotalMilliseconds > lastAttackTime + cooldown)
            {
                var projectileOrigin = renderer.Sprite.GetCenter + transform.Position - projectileRenderer.Sprite.GetCenter;

                for (currentAngle = 0; currentAngle < Math.PI * 2; currentAngle += offset)
                {
                    var projectileInstance = GameManager.Instantiate(projectile, projectileOrigin);
                    projectileInstance.Tag = "Enemy" + projectileInstance.Tag;
                    var instanceVelocity = (Velocity)projectileInstance.GetComponent<Velocity>();

                    instanceVelocity.Direction = VectorMath.Angle2Vector(currentAngle);
                    ((Transform)projectileInstance.GetComponent<Transform>()).Rotation = currentAngle;
                }
                lastAttackTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }
    }
}
