using _487Game.Collision;
using _487Game.Components;
using _487Game.Components.EnemyBehaviorComponents;
using _487Game.EnemyBehavior;
using _487Game.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yeet1.Texture;

namespace _487Game.Entities
{
    class EntityBuilder
    {

        private Dictionary<string, dynamic> _componentArgs;

        public EntityBuilder()
        {
            _componentArgs = new Dictionary<string, dynamic>();
        }


        public Entity BuildEntity(string type, TextureManager textureManager, EntityManager entityManager)
        {
            Entity newEntity = new Entity(entityManager.GetId(), type);
            newEntity.AddComponent(new PositionComponent(newEntity));

            foreach(var c in _componentArgs)
            {
                AddComponent(newEntity, c.Key, c.Value, textureManager, entityManager);
            }

            return newEntity;
        }

        public void AddComponentArgs(string componentType, dynamic args)
        {
            _componentArgs.Add(componentType, args);
        }

        public void AddComponent(Entity owner, string componentType, dynamic args, TextureManager textureManager, EntityManager entityManager)
        {
            switch (componentType)
            {
                case "SpriteComponent":
                    owner.AddComponent(new SpriteComponent(owner, textureManager.LoadTexture(args.TexturePath.Value), (int)args.Width.Value, (int)args.Height.Value, args.Rotatable.Value));
                    break;
                case "CollisionComponent":
                    owner.AddComponent(NewCollisionComponent(owner, args));
                    break;
                case "HealthComponent":
                    owner.AddComponent(new HealthComponent(owner, (int)args.Health.Value));
                    break;
                case "MovementComponent":
                    owner.AddComponent(new MovementComponent(owner, (float)args.Speed.Value, (float)args.Acceleration.Value));
                    break;
                case "LinearMovementPattern":
                    owner.AddComponent(new LinearMovementComponent(owner, (float)args.Delay.Value, (float)args.XDir.Value, (float)args.YDir.Value));
                    break;
                case "EnemyAttack":
                    owner.AddComponent(new ProjectileComponent(owner, entityManager));
                    owner.AddComponent(new EnemyAttackComponent(owner, NewEnemyAttack(args, NewProjectileInformation(args.ProjectileInformation))));
                    break;
                case "AnimationComponent":
                    LoadAnimations(owner, args);
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }


        private void LoadAnimations(Entity owner, dynamic args)
        {
            AnimationComponent newAnimationComponent = new AnimationComponent(owner);
            foreach(var c in args)
            {
                newAnimationComponent.AddAnimation(c.Name.Value, new Animation((int)c.Row.Value, (int)c.NumFrames.Value, (float)c.Delay.Value, (bool)c.Repeatable.Value));
            }

            owner.AddComponent(newAnimationComponent);
        }

        private CollisionComponent NewCollisionComponent(Entity owner, dynamic args)
        {
            switch (args.HitboxShape.Value)
            {
                case "Circle":
                    return new CollisionComponent(owner, new CircleHitbox((float)args.Radius.Value), new EntityCollisionAction());
                default:
                    throw new IndexOutOfRangeException();
            }

        }

        private EnemyAttack NewEnemyAttack(dynamic args, ProjectileInformation projectileInformation)
        {
            return new EnemyAttack((float)args.Cooldown.Value, (float)args.Duration.Value, (float)args.Delay.Value,
                (float)args.MinAngle.Value, (float)args.MaxAngle.Value, (float)args.DeltaAngle.Value, (float)args.DeltaAcceleration.Value,
                projectileInformation, new Microsoft.Xna.Framework.Vector2((float)args.Direction.X.Value, (float)args.Direction.Y.Value));
        }

        private ProjectileInformation NewProjectileInformation(dynamic args)
        {
            return new ProjectileInformation(args.Texture.Value, (int)args.Damage.Value, (float)args.Speed.Value, (int)args.Width.Value, (int)args.Height.Value, (float)args.Acceleration.Value);
        }
    }
}
