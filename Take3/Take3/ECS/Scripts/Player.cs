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
        private Sprite sprite;

        private Sprite projectileSprite;

        private Vector2 direction;
        private Vector2 speed;

        private Vector2 lastPosition;

        public GameObject PlayerProjectile { get; set; }

        private float cooldown = 200;
        private double timeLastFired;
        private bool canFire = true;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            direction = new Vector2();
            transform = (Transform)GetComponent<Transform>();
            sprite = ((Renderer)GetComponent<Renderer>()).Sprite;

            PlayerProjectile = GameManager.GetPrefab("PurpleDiamondProjectile");
            PlayerProjectile.Tag = "Player" + PlayerProjectile.Tag;

            projectileSprite = ((Renderer)PlayerProjectile.GetComponent<Renderer>()).Sprite;

            speed = new Vector2(250);
        }

        private void PushBackVertical()
        {
            transform.Y = lastPosition.Y;
        }

        private void PushBackHorizantal()
        {
            transform.X = lastPosition.X;
        }
       
        public override void Update(GameTime gameTime)
        {

            direction = Vector2.Zero;

            if (Input.KeyDown("left"))
            {
                direction.X += -1;
            }
            if (Input.KeyDown("right"))
            {
                direction.X += 1;
            }
            if (Input.KeyDown("down"))
            {
                direction.Y += 1;
            }
            if (Input.KeyDown("up"))
            {
                direction.Y += -1;
            }
            if (Input.KeyDown("fire"))
            {
                if(canFire)
                {
                    FireProjectile();
                    timeLastFired = gameTime.TotalGameTime.TotalMilliseconds;
                    canFire = false;
                }
            }
            if (direction.X != 0 || direction.Y != 0)
            {
                direction.Normalize();
            }

            if(gameTime.TotalGameTime.TotalMilliseconds >= timeLastFired + cooldown)
            {
                canFire = true;
            }

            lastPosition = transform.Position;
            transform.Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
        }

        private void FireProjectile()
        {
            var projectileInstance = GameManager.Instantiate(PlayerProjectile, 
                                                 new Vector2(sprite.GetCenter.X + transform.Position.X - projectileSprite.GetCenter.X,
                                                             transform.Position.Y));
            var projectileInstanceScript = (Projectile)projectileInstance.GetComponent<Projectile>();
            projectileInstanceScript.Direction = new Vector2(0, -1);
        }
    }
}
