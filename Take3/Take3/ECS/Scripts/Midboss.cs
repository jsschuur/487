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
    class Midboss : Script
    {
        enum MidbossState
        {
            Phase1, Phase2
        }


        private Velocity velocity;
        private Transform transform;

        private Renderer renderer;

        private GameObject projectile;
        private Transform player;

        private Vector2[] phaseOneCoordinates;
        private Vector2[] bossOutline;
        private Vector2[] starPoints;

        private float phase2AttackCooldown = 70;

        //15 degrees per second
        private double phase2RotationSpeed = 0.261799 * 3;
        private double phase2LastAttack;

        private int currentPhase1Index;

        private MidbossState midbossState;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            transform = (Transform)GetComponent<Transform>();
            transform.Position = new Vector2(360, 300);

            renderer = (Renderer)GetComponent<Renderer>();

            velocity = (Velocity)GetComponent<Velocity>();

            player = (Transform)GameManager.GetPrefab("Player").GetComponent<Transform>();
            projectile = GameManager.GetPrefab("GreenProjectile");

            projectile.Tag = "Enemy" + projectile.Tag;

            phaseOneCoordinates = new Vector2[]
            {
                new Vector2(360, 84) - renderer.Sprite.GetCenter,
                new Vector2(248, 432) - renderer.Sprite.GetCenter,
                new Vector2(541, 215) - renderer.Sprite.GetCenter,
                new Vector2(178, 215) - renderer.Sprite.GetCenter,
                new Vector2(471, 432) - renderer.Sprite.GetCenter
            };

            bossOutline = new Vector2[]
            {
                new Vector2(128, 10) * renderer.Sprite.Scale,
                new Vector2(96, 102) * renderer.Sprite.Scale,
                new Vector2(1, 101) * renderer.Sprite.Scale,
                new Vector2(77, 159) * renderer.Sprite.Scale,
                new Vector2(46, 246) * renderer.Sprite.Scale,
                new Vector2(128, 188) * renderer.Sprite.Scale,
                new Vector2(210, 246) * renderer.Sprite.Scale,
                new Vector2(180, 159) * renderer.Sprite.Scale,
                new Vector2(255, 101) * renderer.Sprite.Scale,
                new Vector2(160, 102) * renderer.Sprite.Scale
            };

            starPoints = new Vector2[]
            {
                new Vector2(128, 10) * renderer.Sprite.Scale,
                new Vector2(1, 101) * renderer.Sprite.Scale,
                new Vector2(46, 246) * renderer.Sprite.Scale,
                new Vector2(210, 246) * renderer.Sprite.Scale,
                new Vector2(255, 101) * renderer.Sprite.Scale,
            };

            midbossState = MidbossState.Phase1;
        }

        public override void Update(GameTime gameTime)
        {
            switch(midbossState)
            {
                case MidbossState.Phase1:
                    ManagePhase1(gameTime); break;
                case MidbossState.Phase2:
                    ManagePhase2(gameTime); break;
            }
        }

        private void ManagePhase1(GameTime gameTime)
        {
            var distance = Vector2.Distance(transform.Position, phaseOneCoordinates[currentPhase1Index]);

            if (distance <= velocity.Speed * gameTime.ElapsedGameTime.TotalSeconds)
            {
                currentPhase1Index++;
                if (currentPhase1Index >= 5) currentPhase1Index = 0;
                StarAttack();
            }
            else
            {
                velocity.Direction = phaseOneCoordinates[currentPhase1Index] - transform.Position;
            }
        }

        private void ManagePhase2(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds >= phase2LastAttack + phase2AttackCooldown)
            {
                var projectileRenderer = (Renderer)projectile.GetComponent<Renderer>();
                foreach (var point in starPoints)
                {
                    var rotatedPoint = VectorMath.RotatePoint(point, renderer.Sprite.GetCenter, transform.Rotation);
                    var direction = rotatedPoint - renderer.Sprite.GetCenter;
                    if (direction.X != 0 || direction.Y != 0) direction.Normalize();
                    var instance = GameManager.Instantiate(projectile, rotatedPoint + transform.Position - projectileRenderer.Sprite.GetCenter);

                    var instanceVelocity = (Velocity)instance.GetComponent<Velocity>();
                    instanceVelocity.Direction = direction;
                }
                phase2LastAttack = gameTime.TotalGameTime.TotalMilliseconds;
            }

            transform.Rotation += (float)phase2RotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void StarAttack()
        {
            var projectileSprite = ((Renderer)projectile.GetComponent<Renderer>()).Sprite;
            var projectileDirection = player.Position - transform.Position;

            if(projectileDirection.X != 0 || projectileDirection.Y != 0) projectileDirection.Normalize();
            
            for(int i = 0; i < bossOutline.Length; i++)
            {
                var j = (i == bossOutline.Length - 1) ? 0 : i + 1;

                var direction = bossOutline[j] - bossOutline[i];
                if (direction.X != 0 || direction.Y != 0) direction.Normalize();

                var offset = Vector2.Distance(bossOutline[j], bossOutline[i]) / 8;
                var currentPoint = bossOutline[i];

                while(Vector2.Distance(currentPoint, bossOutline[j]) >= offset)
                {
                    var projectileInstance = GameManager.Instantiate(projectile, currentPoint + transform.Position - projectileSprite.GetCenter);
                    var instanceVelocity = (Velocity)projectileInstance.GetComponent<Velocity>();
                    instanceVelocity.Direction = projectileDirection;
                    currentPoint += offset * direction;
                }
            }
        }
    }
}
