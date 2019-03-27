using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _487Game.Collision;
using Microsoft.Xna.Framework;

namespace _487Game.Components
{
    class CollisionComponent : Component
    {

        private Hitbox _hitbox;

        private CollisionAction _collisionAction;

        public Hitbox GetHitbox { get { return _hitbox; } }

        public CollisionComponent(Entity owner, Hitbox hitbox, CollisionAction collisionAction) : base(owner)
        {
            _componentType = "CollisionComponent";
            _hitbox = hitbox;
            _collisionAction = collisionAction;
        }

        public override void Update(GameTime gameTime)
        {
            _hitbox.Update(_owner);
        }

        public void Collide(Entity collider)
        {
            _collisionAction.Action(collider);
        }
    }
}
