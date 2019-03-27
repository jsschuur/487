using _487Game.Collision;
using _487Game.Components;
using _487Game.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yeet1.Texture;
using static _487Game.Utility.UtilityMath;

namespace _487Game.Entities
{
    class EntityManager
    {
        private Dictionary<uint, Entity> _entities;

        private Entity _player;

        private List<Entity> _toBeRemoved;
        private List<Entity> _toBeAdded;

        private uint _numberOfEntities;
        private uint _id;

        bool _paused;

        private TextureManager _textureManager;

        private CollisionManager _collisionManger;

        public uint GetId()
        {
            return ++_id;
        }

        public void InitializePlayer(Entity player)
        {
            _player = player;
        }

        public bool isPaused { get { return _paused; } }

        public void Pause()
        {
            if (_paused == false)
                _paused = true;
            else
                _paused = false;
        }

        public EntityManager(TextureManager textureManager)
        {
            _entities = new Dictionary<uint, Entity>();
            _toBeRemoved = new List<Entity>();
            _toBeAdded = new List<Entity>();

            _collisionManger = new CollisionManager(this);
            _numberOfEntities = 0;
            _id = 0;
            _textureManager = textureManager;
        }

        public void ProjectileEventHandler(object sender, ProjectileEventArgs e)
        {

            Entity owner = ((ProjectileComponent)sender).Owner;

            Texture2D tex = _textureManager.LoadTexture(e.NewProjectileInformation.TextureName);

            Projectile newProjectile = new Projectile(GetId(), owner.Type + "projectile", e.NewProjectileInformation.Damage);

            newProjectile.AddComponent(new ProjectileDamageComponent(newProjectile, e.NewProjectileInformation.Damage));
            newProjectile.AddComponent(new SpriteComponent(newProjectile, tex, e.NewProjectileInformation.Width, e.NewProjectileInformation.Height, true));
            newProjectile.AddComponent(new MovementComponent(newProjectile, e.Direction, e.NewProjectileInformation.Speed, e.NewProjectileInformation.Acceleration));

            Vector2 ownerPosition = ((PositionComponent)owner.GetComponent("PositionComponent")).GetPosition;
            Rectangle ownerRectangle = ((SpriteComponent)owner.GetComponent("SpriteComponent")).GetDrawRectangle;
            Rectangle newProjectileRectangle = ((SpriteComponent)newProjectile.GetComponent("SpriteComponent")).GetDrawRectangle;

            Vector2 projectileOrigin = new Vector2((ownerPosition.X + (ownerRectangle.Width / 2 - newProjectileRectangle.Width / 2)), (ownerPosition.Y + (ownerRectangle.Height / 2 - newProjectileRectangle.Height / 2)));

            newProjectile.AddComponent(new PositionComponent(newProjectile, projectileOrigin));
            newProjectile.AddComponent(new CollisionComponent(newProjectile, new CircleHitbox(newProjectileRectangle.Width / 2), new ProjectileCollisionAction(newProjectile)));

            newProjectileRectangle.X = (int)projectileOrigin.X;
            newProjectileRectangle.Y = (int)projectileOrigin.Y;

            AddEntity(newProjectile);
        }

        public void AddEntity(Entity entity)
        {
            _toBeAdded.Add(entity);
            _numberOfEntities++;
        }

        public void ClearEnemyProjectiles()
        {
            foreach (Entity e in _entities.Values)
            {
                if(e.Type == "enemyprojectile")
                {
                    _toBeRemoved.Add(e);
                }
            }
        }

        public void EntityEventHandler(object sender, EntityEventArgs e)
        {
            switch(e.EventType)
            {
                case "playerhit":
                    ((PositionComponent)_player.GetComponent("PositionComponent")).SetPosition(200, 200);
                    ClearEnemyProjectiles();
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var e in _toBeAdded)
            {
                if(e.Id ==  3)
                {
                    Console.Write("code");
                }
                _entities.Add(e.Id, e);
                _collisionManger.AddCollidable(e);
            }

            _toBeAdded.Clear();

            foreach (var e in _entities.Values)
            {
                if(!e.IsAlive)
                {
                    _toBeRemoved.Add(e);
                }
                e.Update(gameTime);
            }


            foreach (var e in _toBeRemoved)
            {
                _entities.Remove(e.Id);
                _collisionManger.RemoveCollidable(e);
            }
            
            _toBeRemoved.Clear();

            _collisionManger.CheckCollisions();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var e in _entities.Values)
            {
                ((SpriteComponent)e.GetComponent("SpriteComponent"))?.Draw(spriteBatch);
            }
        }
    }
}
