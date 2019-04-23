using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;

namespace Take3.ECS.Scripts
{
    class PlayerHealthBar : Script
    {
        private Player player;

        private SpriteRenderer renderer;
        private Transform transform;

        private int totalHealth;
        private int initialHeight;

        private float initialY;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            player = (Player)GameManager.GetObjectByTag("Player").GetComponent<Player>();
            renderer = (SpriteRenderer)GetComponent<SpriteRenderer>();
            transform = (Transform)GetComponent<Transform>();

            initialHeight = renderer.Sprite.SpriteRectangle.Height;
            totalHealth = player.Health;

            initialY = transform.Position.Y;
        }

        public override void Update(GameTime gameTime)
        {
            var percent =  (float)player.Health / totalHealth;

            var spriteRect = renderer.Sprite.SpriteRectangle;
            spriteRect.Height = initialHeight - (int)(initialHeight * percent);
            renderer.Sprite.SpriteRectangle = spriteRect;
            
        }
    }
}
