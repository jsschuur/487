using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Take3.ECS;
using Take3.ECS.Scripts;

namespace Take3.Utility
{
    static class CloneGameObject
    {
        public static GameObject Clone(Prefabrication original)
        {
            GameObject copy = new GameObject();

            copy.Tag = original.Tag;

            foreach (Component c in original.Components)
            {
                switch (c)
                {
                    case Transform t:
                        CloneTransform(copy, t); break;
                    case Animator a:
                        CloneAnimator(copy, a); break;
                    case CircleCollider cc:
                        CloneCircleCollider(copy, cc); break;
                    case BoxCollider bc:
                        CloneBoxCollider(copy, bc); break;
                    case LinearProjectile lp:
                        CloneLinearProjectile(copy, lp); break;
                    case SpriteRenderer sr:
                        CloneSpriteRenderer(copy, sr); break;
                    case TextRenderer tr:
                        CloneTextRenderer(copy, tr); break;
                    case Velocity v:
                        CloneVelocity(copy, v); break;
                    case Script s:
                        CloneScript(copy, s); break;
                    default:
                        break;
                }
            }
            return copy;
        }

        private static void CloneVelocity(GameObject copy, Velocity velocity)
        {
            var newVelocity = (Velocity)copy.AddComponent<Velocity>();

            newVelocity.Speed = velocity.Speed;
            newVelocity.Direction = new Vector2(velocity.Direction.X, velocity.Direction.Y);
            newVelocity.Acceleration = velocity.Acceleration;
        }

        private static void CloneTextRenderer(GameObject copy, TextRenderer textRenderer)
        {
            var newTextRenderer = (TextRenderer)copy.AddComponent<TextRenderer>();
            newTextRenderer.Font = textRenderer.Font;
            newTextRenderer.Text = textRenderer.Text;
            newTextRenderer.TextColor = textRenderer.TextColor;
        }

        private static void CloneScript(GameObject copy, Script script)
        {
            var newScriptType = script.GetType();
            copy.AddComponent((Component)Activator.CreateInstance(newScriptType));
        }

        private static void CloneTransform(GameObject copy, Transform transform)
        {
            var newTransform = (Transform)copy.GetComponent<Transform>();
            newTransform.Initialize(copy);
            newTransform.Position = transform.Position;
            newTransform.Rotation = transform.Rotation;
        }

        private static void CloneAnimator(GameObject copy, Animator original)
        {
            var newAnimator = (Animator)copy.AddComponent<Animator>();
            newAnimator.Animations = original.Animations;
            newAnimator.CurrentAnimation = original.CurrentAnimation;
        }

        private static void CloneCircleCollider(GameObject copy, CircleCollider original)
        {
            var newCircleCollider = (CircleCollider)copy.AddComponent<CircleCollider>();
            newCircleCollider.Buffer = original.Buffer;
        }

        private static void CloneBoxCollider(GameObject copy, BoxCollider original)
        {
            var newBoxCollider = (BoxCollider)copy.AddComponent<BoxCollider>();
            newBoxCollider.Buffer = original.Buffer;
        }

        private static void CloneLinearProjectile(GameObject copy, LinearProjectile original)
        {
            var newLinearProjectile = (LinearProjectile)copy.AddComponent<LinearProjectile>();
            newLinearProjectile.Direction = new Vector2(original.Direction.X, original.Direction.Y);
            newLinearProjectile.Speed = original.Speed;
        }

        private static void CloneSpriteRenderer(GameObject copy, SpriteRenderer original)
        {
            var newRenderer = (SpriteRenderer)copy.AddComponent<SpriteRenderer>();
            Sprite newSprite = new Sprite(original.Sprite.Texture, 
                                          new Rectangle(original.Sprite.SpriteRectangle.X,
                                          original.Sprite.SpriteRectangle.Y,
                                          original.Sprite.SpriteRectangle.Width,
                                          original.Sprite.SpriteRectangle.Height), 
                                          original.Sprite.Scale, original.Sprite.Rotatable, original.Sprite.LayerDepth);


            newRenderer.Sprite = newSprite;
        }

       

        

    }
}
