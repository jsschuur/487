﻿using System;
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
        public static GameObject Clone(GameObject original)
        {
            GameObject copy = new GameObject();

            copy.Tag = original.Tag;

            foreach (Component c in original.GetComponents<Component>())
            {
                switch (c)
                {
                    case Transform t:
                        CloneTransform(copy, t); break;
                    case Animator a:
                        CloneAnimator(copy, a); break;
                    case CircleCollider cc:
                        CloneCircleCollider(copy, cc); break;
                    case LinearProjectile lp:
                        CloneLinearProjectile(copy, lp); break;
                    case Renderer r:
                        CloneRenderer(copy, r); break;
                    case LinearMovement lm:
                        CloneLinearMovement(copy, lm); break;
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
        }

        private static void CloneLinearMovement(GameObject copy, LinearMovement linearMovement)
        {
            var newLinearMovement = (LinearMovement)copy.AddComponent<LinearMovement>();
            newLinearMovement.Direction = new Vector2(linearMovement.Direction.X, linearMovement.Direction.Y);
            newLinearMovement.Speed = linearMovement.Speed;
        }

        private static void CloneScript(GameObject copy, Script script)
        {
            var newScriptType = script.GetType();
            copy.AddComponent((Component)Activator.CreateInstance(newScriptType));
        }

        private static void CloneTransform(GameObject copy, Transform transform)
        {
            var newTransform = (Transform)copy.GetComponent<Transform>();
            newTransform.Scale = transform.Scale;
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

        private static void CloneLinearProjectile(GameObject copy, LinearProjectile original)
        {
            var newLinearProjectile = (LinearProjectile)copy.AddComponent<LinearProjectile>();
            newLinearProjectile.Direction = new Vector2(original.Direction.X, original.Direction.Y);
            newLinearProjectile.Speed = original.Speed;
        }

        private static void CloneRenderer(GameObject copy, Renderer original)
        {
            var newRenderer = (Renderer)copy.AddComponent<Renderer>();
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
