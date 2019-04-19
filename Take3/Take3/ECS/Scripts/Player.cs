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


        public GameObject PlayerProjectile { get; set; }

        private float cooldown = 200;
        private double timeLastFired;
        private bool canFire = true;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            transform = (Transform)GetComponent<Transform>();
            velocity = (Velocity)GetComponent<Velocity>();
            sprite = ((Renderer)GetComponent<Renderer>()).Sprite;

            PlayerProjectile = Utility.CloneGameObject.Clone(GameManager.GetPrefab("PurpleDiamondProjectile"));
            PlayerProjectile.Tag = "Player" + PlayerProjectile.Tag;

            projectileSprite = ((Renderer)PlayerProjectile.GetComponent<Renderer>()).Sprite;
            
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
                if(canFire)
                {
                    FireProjectile();
                    timeLastFired = gameTime.TotalGameTime.TotalMilliseconds;
                    canFire = false;
                }
            }

            if(gameTime.TotalGameTime.TotalMilliseconds >= timeLastFired + cooldown)
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
        }

        private void FireProjectile()
        {
            var projectileInstance = GameManager.Instantiate(PlayerProjectile, 
                                                 new Vector2(sprite.GetCenter.X + transform.Position.X - projectileSprite.GetCenter.X,
                                                             transform.Position.Y));
            var instanceVelocity = (Velocity)projectileInstance.GetComponent<Velocity>();
            instanceVelocity.Direction = new Vector2(0, -1);
        }
    }
}
