using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;
using static Take3.Utility.UtilityMath;

namespace Take3.ECS.Scripts
{ 
    class FinalBoss : Enemy
    {

        enum FinalBossState
        {
            TransitionPhase1, Phase1, TransitionPhase2, Phase2
        }

        private Transform transform;
        private SpriteRenderer renderer;

        private float triangleAttackCooldown = 3500;
        private double lastTriangleAttackTime;

        private float flipGameScreenAttackCooldown = 5000;
        private double flipGameScreenLastAttackTime;

        bool isFlipped;

        private FinalBossState finalBossState;

        private Vector2[] bossOutline;

        private float bulletRainCooldown = 1500f;
        private double bulletRainLastAttackTime;
        private float bulletRainSpacing = 700 / 10f;
        private float bulletRainOffset;

        private Prefabrication bulletFanProjectile;
        private float bulletFanCooldown = 2500f;
        private double bulletFanLastAttackTime;
        private float bulletFanOffset = .3f;

        private int totalHealth;

        

        private Prefabrication bulletRainProjectile;

        private Prefabrication phaseOneProjectile;
        private Prefabrication flipScreenAttack;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            transform = (Transform)GetComponent<Transform>();
            renderer = (SpriteRenderer)GetComponent<SpriteRenderer>();

            bossOutline = new Vector2[]
            {
                new Vector2(3, 43) * renderer.Sprite.Scale,
                new Vector2(128, 255) * renderer.Sprite.Scale,
                new Vector2(253 , 43) * renderer.Sprite.Scale
            };

            phaseOneProjectile = GameManager.GetPrefab("BlueProjectile");
            flipScreenAttack = GameManager.GetPrefab("FlipGameScreen");
            bulletRainProjectile = GameManager.GetPrefab("PurpleDiamondProjectile");
            bulletFanProjectile = GameManager.GetPrefab("GreySquareProjectile");


            health = totalHealth = 10;

            finalBossState = FinalBossState.TransitionPhase1;
        }

        public void SwitchBulletRainOffset()
        {
            bulletRainOffset = (bulletRainOffset == 35) ? 0 : 35;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            if(gameTime.TotalGameTime.TotalMilliseconds >= triangleAttackCooldown + lastTriangleAttackTime)
            {
                TriangleAttack();
                lastTriangleAttackTime = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if (gameTime.TotalGameTime.TotalMilliseconds >= bulletRainCooldown + bulletRainLastAttackTime)
            {
                BulletRain(bulletRainOffset);
                SwitchBulletRainOffset();
                bulletRainLastAttackTime = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if ((float)health / totalHealth <= .33)
            {
                if (isFlipped)
                {
                    GameManager.Instantiate(GameManager.GetPrefab("FlipGameScreen"));
                    isFlipped = false;
                }

                else if (gameTime.TotalGameTime.TotalMilliseconds >= bulletFanLastAttackTime + bulletFanCooldown)
                {
                    BulletFan(new Vector2(0, transform.Position.Y));
                    BulletFan(new Vector2(720, transform.Position.Y));
                    bulletFanLastAttackTime = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }

            else if ((float)health / totalHealth <= .66)
            {
                if(gameTime.TotalGameTime.TotalMilliseconds >= flipGameScreenLastAttackTime + flipGameScreenAttackCooldown)
                {
                    GameManager.Instantiate(GameManager.GetPrefab("FlipGameScreen"));
                    flipGameScreenLastAttackTime = gameTime.TotalGameTime.TotalMilliseconds;
                    isFlipped ^= true;
                }
            }

           


        }

        public void ManageTransitionPhase1(GameTime gameTime)
        {
            finalBossState = FinalBossState.Phase1;
        }

        public void ManagePhase1(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds >= lastTriangleAttackTime + triangleAttackCooldown)
            {
                TriangleAttack();
                lastTriangleAttackTime = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if((float)health / totalHealth <= .6f)
            {
                if(gameTime.TotalGameTime.TotalMilliseconds >= flipGameScreenLastAttackTime + flipGameScreenAttackCooldown)
                {
                    GameManager.Instantiate(flipScreenAttack, new Vector2(0, 0));
                    flipGameScreenLastAttackTime = gameTime.TotalGameTime.TotalMilliseconds;
                    isFlipped ^= true;
                }

                if((float)health / totalHealth <= .4f)
                {
                    finalBossState = FinalBossState.TransitionPhase2;
                }
            }
        }

        public void ManageTransitionPhase2(GameTime gameTime)
        {
            if(isFlipped)
            {
                GameManager.Instantiate(flipScreenAttack, new Vector2(0, 0));
                isFlipped = false;
            }
            finalBossState = FinalBossState.Phase2;
        }

        public void ManagePhase2(GameTime gameTime)
        {

        }

        

        public void TriangleAttack()
        {
            var projectileSprite = ((SpriteRenderer)phaseOneProjectile.GetComponent<SpriteRenderer>()).Sprite;
            var projectileDirection = -VectorMath.Angle2Vector(transform.Rotation);

            for (int i = 0; i < bossOutline.Length - 1; i++)
            {
                var j = (i == bossOutline.Length - 1) ? 0 : i + 1;

                var direction = bossOutline[j] - bossOutline[i];
                if (direction.X != 0 || direction.Y != 0) direction.Normalize();

                var offset = Vector2.Distance(bossOutline[j], bossOutline[i]) / 16;
                var currentPoint = bossOutline[i];

                var projectileInstance = GameManager.Instantiate(phaseOneProjectile, currentPoint + transform.Position - projectileSprite.GetCenter());
                projectileInstance.Tag = "Enemy" + projectileInstance.Tag;
                var instanceVelocity = (Velocity)projectileInstance.GetComponent<Velocity>();
                instanceVelocity.Direction = projectileDirection;

                while (Vector2.Distance(currentPoint, bossOutline[j]) >= offset)
                {
                    currentPoint += offset * direction;
                    projectileInstance = GameManager.Instantiate(phaseOneProjectile, currentPoint + transform.Position - projectileSprite.GetCenter());
                    projectileInstance.Tag = "Enemy" + projectileInstance.Tag;
                    instanceVelocity = (Velocity)projectileInstance.GetComponent<Velocity>();
                    instanceVelocity.Direction = projectileDirection;
                }
            }
        }

        public void BulletRain(float offset)
        {
            for(var currentX = offset; currentX <= 710; currentX += bulletRainSpacing)
            {
                var projectileInstance = GameManager.Instantiate(bulletRainProjectile, new Vector2(currentX, transform.Y + ((isFlipped) ? renderer.Sprite.GetDimensions().Y + 10 : -10)));
                projectileInstance.Tag = "Enemy" + projectileInstance.Tag;
                var projectileVelocity = (Velocity)projectileInstance.GetComponent<Velocity>();
                projectileVelocity.Direction = -VectorMath.Angle2Vector(transform.Rotation);
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
