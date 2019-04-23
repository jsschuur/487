using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Take3.ECS.Scripts;
using Take3.GameManagement;
using static Take3.Utility.UtilityMath;

namespace Take3.ECS.Scripts
{
    class Player : Script
    {
        private Transform transform;
        private Velocity velocity;

        private Sprite sprite;

        private Sprite projectileSprite;

        private int health;

        public int Health { get { return health; } }

        public Prefabrication PlayerProjectile { get; set; }

        private float cooldown = 200;
        private double timeLastFired;
        private bool canFire = true;

        private bool fireDelay;
        private double fireDelayTime;

        private bool isFlipped;

        public bool IsFlipped { get { return isFlipped; } set { isFlipped = value; } }

        private int damage;

        private Vector2 playerOrigin;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            transform = (Transform)GetComponent<Transform>();
            velocity = (Velocity)GetComponent<Velocity>();
            sprite = ((SpriteRenderer)GetComponent<SpriteRenderer>()).Sprite;

            PlayerProjectile = GameManager.GetPrefab("RedProjectile");


            projectileSprite = ((SpriteRenderer)PlayerProjectile.GetComponent<SpriteRenderer>()).Sprite;

            damage = 10;
            health = 100;

            playerOrigin = new Vector2(360, 580) - sprite.GetCenter();

            transform.Position = playerOrigin;
        }

        private void PushBackVertical()
        {
            transform.Y = velocity.LastPosition.Y;
        }

        private void PushBackHorizantal()
        {
            transform.X = velocity.LastPosition.X;
        }
       
        public override void Update(GameTime gameTime)
        {
            velocity.Direction = Vector2.Zero;

            if (Input.KeyDown("left"))
            {
                velocity.XDir += -1;
            }
            if (Input.KeyDown("right"))
            {
                velocity.XDir += 1;
            }
            if (Input.KeyDown("down"))
            {
                velocity.YDir += 1;
            }
            if (Input.KeyDown("up"))
            {
                velocity.YDir += -1;
            }
            if (Input.KeyDown("fire"))
            {
                AttemptToFire(gameTime);
            }

            
            if(isFlipped)
            {
                velocity.Direction *= -1;
            }

            if(fireDelay)
            {
                fireDelayTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
                if(fireDelayTime < 0)
                {
                    fireDelay = false;
                    canFire = true;
                    fireDelayTime = 0;
                }
            }

            else if(gameTime.TotalGameTime.TotalMilliseconds >= timeLastFired + cooldown)
            {
                canFire = true;
            }
        }

        public override void OnCollision(GameObject collider)
        {
            if(collider.Tag == "HorizantalBoundary")
            {
                PushBackHorizantal();
            }
            else if(collider.Tag == "VerticalBoundary")
            {
                PushBackVertical();
            }
            else if(collider.Tag == "EnemyProjectile")
            {
                var projectile = (Projectile)collider.GetComponent<Projectile>();
                health -= projectile.Damage;
                PlayerHit();
            }
        }


        private void PlayerHit()
        {
            foreach(var obj in GameManager.GetObjectsByTag("EnemyProjectile"))
            {
                obj.Die();
            }

            foreach(var obj in GameManager.GetObjectsByTag("PlayerProjectile"))
            {
                obj.Die();
            }

            foreach(var obj in GameManager.GetObjectsByTag("Enemy"))
            {
                obj.SetDisableTimer(1500);
            }

            transform.Position = VectorMath.RotatePoint(playerOrigin, new Vector2(360, 360), transform.Rotation) - sprite.GetDimensions();
            fireDelay = true;
            canFire = false;
            fireDelayTime = 1500;
        }

        public void EquipPowerUp(PowerUp powerUp)
        {
            damage = powerUp.Damage;
            PlayerProjectile = powerUp.Projectile;
        }

        public void AttemptToFire(GameTime gameTime)
        {
            if (canFire)
            {
                FireProjectile();
                timeLastFired = gameTime.TotalGameTime.TotalMilliseconds;
                canFire = false;
            }
        }

        private void FireProjectile()
        {
            var projectileInstance = GameManager.Instantiate(PlayerProjectile,
                                                             sprite.GetCenter() + transform.Position - projectileSprite.GetCenter());

            projectileInstance.Tag = "Player" + PlayerProjectile.Tag;
            var instanceVelocity = (Velocity)projectileInstance.GetComponent<Velocity>();
            instanceVelocity.Direction = VectorMath.Angle2Vector(transform.Rotation);
            var instanceProjectile = (Projectile)projectileInstance.GetComponent<Projectile>();
            instanceProjectile.Damage = damage;
            
        }
    }
}
