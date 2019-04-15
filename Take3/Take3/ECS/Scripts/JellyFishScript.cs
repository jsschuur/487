using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Take3.ECS.Scripts
{
    public class JellyFishScript : Script
    {

        private enum JellyFishState
        {
            Idle, Attacking, Spawning, Dying 
        }

        private Animator animator;

        private double timeLastSwitched;
        private float delay = 2000;

        private JellyFishState state;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            state = JellyFishState.Spawning;
            animator = (Animator)GetComponent<Animator>();
        }

        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalMilliseconds >= timeLastSwitched + delay)
            {
                state++;
                if((int)state > 3)
                {
                    state = 0;
                }

                switch (state)
                {
                    case JellyFishState.Idle:
                        animator.SwitchAnimation("Idle"); break;
                    case JellyFishState.Attacking:
                        animator.SwitchAnimation("Attack"); break;
                    case JellyFishState.Spawning:
                        animator.SwitchAnimation("Spawn"); break;
                    case JellyFishState.Dying:
                        animator.SwitchAnimation("Death"); break;
                }
                timeLastSwitched = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }
    }
}
