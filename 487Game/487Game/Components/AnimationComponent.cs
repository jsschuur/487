using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _487Game.Components
{
    class Animation
    {
        private int _row;
        private int _numFrames;

        private float _delay;
        private bool _repeatable;

        public int Row { get { return _row; } }
        public int NumFrames { get { return _numFrames; } }
        public bool Repeatable { get { return _repeatable; } }

        public float Delay { get { return _delay; } }


        public Animation(int row, int numFrames, float delay, bool repeatable)
        {
            _row = row;
            _numFrames = numFrames;
            _delay = delay;
            _repeatable = repeatable;
        }
    }
    class AnimationComponent : Component
    {

        private Dictionary<string, Animation> _animations;
        private Animation _activeAnimation;

        private int _index = 0;
        private double _lastSwapTime;

        public Animation ActiveAnimation { set { _activeAnimation = value; } }

        public AnimationComponent(Entity owner) : base(owner)
        {
            _componentType = "AnimationComponent";
            _animations = new Dictionary<string, Animation>();
        }

        public void AddAnimation(string key, Animation animation)
        {
            _animations.Add(key, animation);
            if(key == "Default")
            {
                _activeAnimation = animation;
            }
        }

        public void SwitchAnimation(string key)
        {
            _activeAnimation = _animations[key];
        }

        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalMilliseconds >= _lastSwapTime + _activeAnimation.Delay)
            {
                ((SpriteComponent)_owner.GetComponent("SpriteComponent")).SwapFrames(_index, _activeAnimation.Row);

                _index++;
                _lastSwapTime = gameTime.TotalGameTime.TotalMilliseconds;

                if(_index >= _activeAnimation.NumFrames)
                {
                    if(!_activeAnimation.Repeatable)
                    {
                        _activeAnimation = _animations["Default"];
                    }
                    else
                    {
                        _index = 0;
                    }
                }
            }
        }
    }
}
