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
        private Renderer renderer;
        private Animator animator;

        private Rectangle buttonRectangle;

        private Dictionary<ButtonObjectState, Sprite> buttonSprites;

        protected ButtonObjectState state;
        protected ButtonObjectState previousState;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            transform = (Transform)GetComponent<Transform>();
            renderer = (Renderer)GetComponent<Renderer>();
            animator = (Animator)GetComponent<Animator>();

            buttonRectangle = new Rectangle((int)transform.Position.X, (int)transform.Position.Y,
                                            renderer.Sprite.SpriteRectangle.Width,
                                            renderer.Sprite.SpriteRectangle.Height);

            buttonSprites = new Dictionary<ButtonObjectState, Sprite>();
        }

        public void AddSprite(ButtonObjectState state, Sprite sprite)
        {
            buttonSprites[state] = sprite;
        }

        public override void Update(GameTime gameTime)
        {

            Vector2 mousePos = Input.GetMouseLocation();

            if (state == ButtonObjectState.Clicked && !Input.MouseDown())
            {
                OnClick();
                state = ButtonObjectState.Idle;
                animator.SeamlessAnimationSwitch("Idle");
            }
            else if (!Input.MouseDown() && MouseInBounds(mousePos))
            {
                state = ButtonObjectState.Hovered;
                animator.SeamlessAnimationSwitch("Hovered");
            }
            else if((state == ButtonObjectState.Hovered || state == ButtonObjectState.Clicked) && Input.MouseDown())
            {
                state = ButtonObjectState.Clicked;
                animator.SeamlessAnimationSwitch("Clicked");
            }
            else
            {
                state = ButtonObjectState.Idle;
                animator.SeamlessAnimationSwitch("Idle");
            }


        }

        private bool MouseInBounds(Vector2 point)
        {
            return (point.X >= buttonRectangle.Left && point.X <= buttonRectangle.Right && 
                    point.Y >= buttonRectangle.Top && point.Y <= buttonRectangle.Bottom);
        }
    }
}
