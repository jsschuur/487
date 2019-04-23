using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Take3.ECS.Scripts
{

    public enum ButtonObjectState
    {
        Idle, Hovered, Clicked
    }

    public abstract class Button : Script
    {
        private Transform transform;
        private SpriteRenderer renderer;
        private Animator animator;

        protected ButtonObjectState state;


        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            transform = (Transform)GetComponent<Transform>();
            renderer = (SpriteRenderer)GetComponent<SpriteRenderer>();
            animator = (Animator)AddComponent<Animator>();

            Animation idle = new Animation(0, 0, 0);
            Animation hovered = new Animation(0, 0, 1);
            Animation clicked = new Animation(0, 0, 2);

            animator.AddAnimation("Idle", idle);
            animator.AddAnimation("Hovered", hovered);
            animator.AddAnimation("Clicked", clicked);
        }

        public override void Update(GameTime gameTime)
        {
            var mousePos = Input.GetMouseLocation();
            
            if(!Input.MouseDown())
            {
                if (MouseInBounds(mousePos))
                {
                    state = ButtonObjectState.Hovered;
                }
                else
                {
                    state = ButtonObjectState.Idle;
                }
            }
            else
            {
                if(state == ButtonObjectState.Hovered)
                {
                    state = ButtonObjectState.Clicked;
                }
                if(state != ButtonObjectState.Clicked)
                {
                    state = ButtonObjectState.Idle;
                }
            }

            switch(state)
            {
                case ButtonObjectState.Idle:
                    animator.SeamlessAnimationSwitch("Idle"); break;
                case ButtonObjectState.Hovered:
                    animator.SeamlessAnimationSwitch("Hovered"); break;
                case ButtonObjectState.Clicked:
                    animator.SeamlessAnimationSwitch("Clicked"); OnClick();  state = ButtonObjectState.Idle; break;
            }
        }

        private Rectangle GetButtonRectangle()
        {
            return new Rectangle((int)transform.Position.X, (int)transform.Position.Y,
                                 renderer.Sprite.SpriteRectangle.Width,
                                 renderer.Sprite.SpriteRectangle.Height);
        }

        private bool MouseInBounds(Vector2 point)
        {
            var buttonRectangle = GetButtonRectangle();

            return (point.X >= buttonRectangle.Left && point.X <= buttonRectangle.Right && 
                    point.Y >= buttonRectangle.Top && point.Y <= buttonRectangle.Bottom);
        }
    }
}
