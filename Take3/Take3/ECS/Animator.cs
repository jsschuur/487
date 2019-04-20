using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Take3.ECS
{
    class Animator : Updatable
    {
        private Animation currentAnimation;
        public double lastSwitch;

        private int currentIndex;

        private Renderer renderer;
        private Transform transform;

        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

        public Dictionary<string, Animation> Animations { get { return animations; } set { animations = value; } }
        public Animation CurrentAnimation { get { return currentAnimation; } set { currentAnimation = value; } }

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            renderer = (Renderer)GetComponent<Renderer>();
            transform = (Transform)GetComponent<Transform>();
        }

        public void AddAnimation(string name, Animation animation)
        {
            animations.Add(name, animation);
            if(currentAnimation == null)
            {
                currentAnimation = animation;
            }
        }

        public void SwitchAnimation(string animationName)
        {
            currentAnimation = animations[animationName];
            lastSwitch = 0;
            currentIndex = 0;
        }

        public void SeamlessAnimationSwitch(string animationName)
        {
            currentAnimation = animations[animationName];
        }

        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalMilliseconds > currentAnimation.Delay + lastSwitch)
            {
                Rectangle sr = renderer.Sprite.SpriteRectangle;
                sr.X = (int)(currentIndex * sr.Width);
                sr.Y = (int)(currentAnimation.YOffset * sr.Height);
                renderer.Sprite.SpriteRectangle = sr;

                currentIndex++;

                if (currentIndex >= currentAnimation.NumFrames)
                {
                    currentIndex = 0;
                }

                lastSwitch = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }
    }
}
