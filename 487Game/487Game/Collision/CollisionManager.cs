using _487Game.Components;
using _487Game.Entities;
using _487Game.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Collision
{
    class CollisionManager
    {
        private Dictionary<string, List<Entity>> _collidables;

        private event EventHandler<EntityEventArgs> _collideEvent;

        public CollisionManager(EntityManager entityManager)
        {
            _collideEvent += entityManager.EntityEventHandler;

            _collidables = new Dictionary<string, List<Entity>>();
            _collidables["player"] = new List<Entity>();
            _collidables["enemy"] = new List<Entity>();
            _collidables["enemyprojectile"] = new List<Entity>();
            _collidables["playerprojectile"] = new List<Entity>();
        }

        public void AddCollidable(Entity entity)
        {
            _collidables[entity.Type].Add(entity);
        }

        public void RemoveCollidable(Entity entity)
        {
            _collidables[entity.Type].Remove(entity);
        }

        public void CheckCollisions()
        {
            Entity player = _collidables["player"].First();

            CollisionComponent playerCollisionComponent = ((CollisionComponent)player.GetComponent("CollisionComponent"));

            foreach (var e in _collidables["enemyprojectile"])
            {
                CollisionComponent c = ((CollisionComponent)e.GetComponent("CollisionComponent"));

                if(CollisionMath.CheckCollision(playerCollisionComponent.GetHitbox, c.GetHitbox))
                {
                    playerCollisionComponent.Collide(e);
                    c.Collide(player);
                    _collideEvent?.Invoke(this, new EntityEventArgs("playerhit"));
                }
            }

            foreach (var p in _collidables["playerprojectile"])
            {
                foreach (var e in _collidables["enemy"])
                {
                    CollisionComponent c1 = ((CollisionComponent)p.GetComponent("CollisionComponent"));
                    CollisionComponent c2 = ((CollisionComponent)e.GetComponent("CollisionComponent"));

                    if(CollisionMath.CheckCollision(c1.GetHitbox, c2.GetHitbox))
                    {
                        c1.Collide(e);
                        c2.Collide(p);
                    }
                }
            }
        }
    }
}
