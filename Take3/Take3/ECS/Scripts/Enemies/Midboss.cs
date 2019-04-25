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
    class Midboss : Enemy
    {
        enum MidbossState
        {
            TransitionPhase1, Phase1, TransitionPhase2, Phase2
        }


        private Velocity velocity;
        private Transform transform;

        private SpriteRenderer renderer;

        private Prefabrication phaseOneProjectile;
        private Prefabrication phaseTwoProjectile;

        private Transform player;
        private Vector2 playerCenter;

        private Vector2[] phaseOneCoordinates;
        private Vector2[] bossOutline;
        private Vector2[] starPoints;

        private Vector2 centerPosition;

        private float phase2AttackCooldown = 70;

        private double phase2RotationSpeed = VectorMath.Degrees2Radians(40);
        private double phase2LastAttack;

        private int currentPhase1Index;

        private float bulletFanCooldown = 2500;
        private double bulletFanLastAttackTime;

        private float bulletFanOffset = .3f;
        private Prefabrication bulletFanProjectile;
        private SpriteRenderer bulletFanProjectileRenderer;

        private MidbossState midbossState;

        private int totalHealth;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            transform = (Transform)GetComponent<Transform>();
            transform.Position = new Vector2(360, 300);

            renderer = (SpriteRenderer)GetComponent<SpriteRenderer>();

            velocity = (Velocity)GetComponent<Velocity>();

            var playerObj = GameManager.GetObjectByTag("Player");

            player = (Transform)playerObj.GetComponent<Transform>();
            playerCenter = ((SpriteRenderer)playerObj.GetComponent<SpriteRenderer>()).Sprite.GetCenter();
            phaseOneProjectile = GameManager.GetPrefab("GreenProjectile");
            phaseTwoProjectile = GameManager.GetPrefab("PinkSquareProjectile");

            phaseOneProjectile.Tag = "Enemy" + phaseOneProjectile.Tag;

            centerPosition = new Vector2(360, 360) - renderer.Sprite.GetCenter();

            bulletFanProjectile = GameManager.GetPrefab("WhiteDiamondProjectile");
            bulletFanProjectileRenderer = (SpriteRenderer)bulletFanProjectile.GetComponent<SpriteRenderer>();

            phaseOneCoordinates = new Vector2[]
            {
                new Vector2(360, 84) - renderer.Sprite.GetCenter(),
                new Vector2(248, 432) - renderer.Sprite.GetCenter(),
                new Vector2(541, 215) - renderer.Sprite.GetCenter(),
                new Vector2(178, 215) - renderer.Sprite.GetCenter(),
                new Vector2(471, 432) - renderer.Sprite.GetCenter()
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

            health = totalHealth = 100;
            midbossState = MidbossState.TransitionPhase1;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch(midbossState)
            {
                case MidbossState.TransitionPhase1:
                    ManageTransitionPhase1(gameTime); break;
                case MidbossState.Phase1:
                    ManagePhase1(gameTime); break;
                case MidbossState.TransitionPhase2:
                    ManageTransitionPhase2(gameTime); break;
                case MidbossState.Phase2:
                    ManagePhase2(gameTime); break;
            }
        }



        private void ManageTransitionPhase1(GameTime gameTime)
        {
            velocity.Direction = phaseOneCoordinates[0] - transform.Position;
            var distance = Vector2.Distance(transform.Position, phaseOneCoordinates[0]);

            if (distance <= velocity.Speed * gameTime.ElapsedGameTime.TotalSeconds)
            {
                currentPhase1Index = 1;
                midbossState = MidbossState.Phase1;
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

            if((float)health / (float)totalHealth <= .5)
            {
                midbossState = MidbossState.TransitionPhase2;
            }

            if(gameTime.TotalGameTime.TotalMilliseconds >= bulletFanCooldown + bulletFanLastAttackTime)
            {
                BulletFan(new Vector2(0, 0));
                BulletFan(new Vector2(730, 0));
                bulletFanLastAttackTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        private void ManageTransitionPhase2(GameTime gameTime)
        {
            velocity.Direction = centerPosition - transform.Position;

            var distance = Vector2.Distance(transform.Position, centerPosition);

            if (distance <= velocity.Speed * gameTime.ElapsedGameTime.TotalSeconds)
            {
                velocity.IsActive = false;
                transform.Position = centerPosition; 
                midbossState = MidbossState.Phase2;
            }
        }

        private void ManagePhase2(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds >= phase2LastAttack + phase2AttackCooldown)
            {
                var projectileRenderer = (SpriteRenderer)phaseTwoProjectile.GetComponent<SpriteRenderer>();
                foreach (var point in starPoints)
                {
                    var rotatedPoint = VectorMath.RotatePoint(point, renderer.Sprite.GetCenter(), transform.Rotation);
                    var direction = rotatedPoint - renderer.Sprite.GetCenter();
 
                    var instance = GameManager.Instantiate(phaseTwoProjectile, rotatedPoint + transform.Position - projectileRenderer.Sprite.GetCenter());
                    instance.Tag = "Enemy" + instance.Tag;
                    var instanceVelocity = (Velocity)instance.GetComponent<Velocity>();
                    instanceVelocity.Direction = direction;
                }
                phase2LastAttack = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if (gameTime.TotalGameTime.TotalMilliseconds >= bulletFanCooldown + bulletFanLastAttackTime)
            {
                BulletFan(new Vector2(0, 0));
                BulletFan(new Vector2(730, 0));
                bulletFanLastAttackTime = gameTime.TotalGameTime.TotalMilliseconds;
            }

            transform.Rotation += (float)phase2RotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void StarAttack()
        {
            var projectileSprite = ((SpriteRenderer)phaseOneProjectile.GetComponent<SpriteRenderer>()).Sprite;
            var projectileDirection = player.Position + playerCenter - transform.Position;

            for(int i = 0; i < bossOutline.Length; i++)
            {
                var j = (i == bossOutline.Length - 1) ? 0 : i + 1;

                var direction = bossOutline[j] - bossOutline[i];
                if (direction.X != 0 || direction.Y != 0) direction.Normalize();

                var offset = Vector2.Distance(bossOutline[j], bossOutline[i]) / 8;
                var currentPoint = bossOutline[i];

                while(Vector2.Distance(currentPoint, bossOutline[j]) >= offset)
                {
                    var projectileInstance = GameManager.Instantiate(phaseOneProjectile, currentPoint + transform.Position - projectileSprite.GetCenter());
                    var instanceVelocity = (Velocity)projectileInstance.GetComponent<Velocity>();
                    instanceVelocity.Direction = projectileDirection;
                    currentPoint += offset * direction;
                }
            }
        }

        private void BulletFan(Vector2 origin)
        {
            for (var currentAngle = 0f; currentAngle < Math.PI * 2; currentAngle += bulletFanOffset)
            {
                var projectileInstance = GameManager.Instantiate(bulletFanProjectile, origin);
                projectileInstance.Tag = "Enemy" + projectileInstance.Tag;
                var instanceVelocity = (Velocity)projectileInstance.GetComponent<Velocity>();

                instanceVelocity.Direction = VectorMath.Angle2Vector(currentAngle);
                ((Transform)projectileInstance.GetComponent<Transform>()).Rotation = currentAngle;
            }
        }
    }
}
