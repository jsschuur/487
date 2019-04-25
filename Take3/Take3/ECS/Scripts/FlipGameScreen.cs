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
    class FlipGameScreen : Script
    {
        private List<GameObject> gameWindowObjects;

        private Vector2 gameWindowCenter;

        private float deltaPerSecond;
        private float currentRotation;
        private float totalRotation;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            gameWindowCenter = new Vector2(360, 360);
            deltaPerSecond = (float)VectorMath.Degrees2Radians(60);
            totalRotation = (float)VectorMath.Degrees2Radians(180);

            currentRotation = 0;

            gameWindowObjects = new List<GameObject>();

            var player = GameManager.GetObjectByTag("Player");
            player.IsActive = false;

            var playerScript = (Player)player.GetComponent<Player>();
            playerScript.IsFlipped ^= true;

            gameWindowObjects.Add(player);

            foreach(var obj in GameManager.GetObjectsByTag("Enemy"))
            {
                obj.IsActive = false;
                gameWindowObjects.Add(obj);
            }

            foreach (var obj in GameManager.GetObjectsByTag("EnemyProjectile"))
            {
                obj.IsActive = false;
                gameWindowObjects.Add(obj);
            }
            foreach (var obj in GameManager.GetObjectsByTag("PlayerProjectile"))
            {
                obj.IsActive = false;
                gameWindowObjects.Add(obj);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach(var obj in gameWindowObjects)
            {
                var transform = (Transform)obj.GetComponent<Transform>();

                var position = transform.Position;

                if(obj.HasComponent<Renderer>())
                {
                    var renderer = (SpriteRenderer)obj.GetComponent<SpriteRenderer>();
                    position += renderer.Sprite.GetCenter();
                    position = VectorMath.RotatePoint(position, gameWindowCenter, deltaPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    position -= renderer.Sprite.GetCenter();
                }
                else
                {
                    position = VectorMath.RotatePoint(position, gameWindowCenter, deltaPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                transform.Position = position;
                transform.Rotation += deltaPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            currentRotation += deltaPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(currentRotation >= totalRotation)
            {
                var difference = totalRotation - currentRotation;

                foreach (var obj in gameWindowObjects)
                {
                    var transform = (Transform)obj.GetComponent<Transform>();
                    var position = transform.Position;
                    if (obj.HasComponent<Renderer>())
                    {
                        var renderer = (SpriteRenderer)obj.GetComponent<SpriteRenderer>();
                        position += renderer.Sprite.GetCenter();
                        position = VectorMath.RotatePoint(position, gameWindowCenter, deltaPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        position -= renderer.Sprite.GetCenter();
                    }
                    else
                    {
                        position = VectorMath.RotatePoint(position, gameWindowCenter, deltaPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }

                    transform.Position = position;

                    if(obj.HasComponent<Velocity>())
                    {
                        var velocity = (Velocity)obj.GetComponent<Velocity>();
                        velocity.Direction *= -1;
                    }

                    transform.Position = position;
                    transform.Rotation += difference;

                    obj.IsActive = true; 
                }

                Die();
            }

        }
    }
}
