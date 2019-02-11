using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yeet1.Entities.Enemies;
using yeet1.Entities.Projectiles;
using yeet1.Input;
using yeet1.Texture;
using yeet1.Utility;

namespace yeet1.Entities
{
    class EntityManager
    {
        private List<Entity> _entities;
        private List<Entity> _toBeAdded;

        private TextureManager _textureManager;

        public EntityManager(ContentManager content)
        {
            this._entities = new List<Entity>();
            this._toBeAdded = new List<Entity>();
            this._textureManager = new TextureManager(content);
        }

       

        public Entity CreateEntity(string type, Vector2 origin)
        {
            switch(type)
            {
                case "BasicEnemy":
                    return new BasicEnemy(_textureManager.LoadTexture("BasicEnemy"), origin, this);
                case "TurretEnemy":
                    return new TurretEnemy(_textureManager.LoadTexture("TurretEnemy"), origin, this);
                case "Midboss":
                    return new Midboss(_textureManager.LoadTexture("Midboss"), origin, this);
                default:
                    return null;
            }
        }

        public Entity CreateEntity(string type, Entity creator)
        {
            return null;
        }


        public void AddEntity(Entity entity)
        {
            _toBeAdded.Add(entity);
        }

        public void InitializePlayer(Vector2 origin, KeyboardManager keyboardManager)
        {
            AddEntity(new Player(_textureManager.LoadTexture("player"), origin, this, keyboardManager)); 
        }

        public void EntityEventHandler(object sender, EntityEventArgs e)
        {
            Entity creator = (Entity)sender;           
           
            if(e.Type == "projectile")
            {
                switch(e.Origin)
                {
                    case Static.Origin.TopCenter:
                        AddEntity(new Projectile(_textureManager.LoadTexture(e.projectileInformation.TextureName),
                        Utility.OriginMath.TopCenter(new Rectangle((int)creator.Position.X, (int)creator.Position.Y, creator.Width, creator.Height), new Rectangle(0, 0, e.projectileInformation.Width, e.projectileInformation.Height)),
                        e.projectileInformation));
                        break;
                    case Static.Origin.BottomCenter:
                        AddEntity(new Projectile(_textureManager.LoadTexture(e.projectileInformation.TextureName),
                        Utility.OriginMath.BottomCenter(new Rectangle((int)creator.Position.X, (int)creator.Position.Y, creator.Width, creator.Height), new Rectangle(0, 0, e.projectileInformation.Width, e.projectileInformation.Height)),
                        e.projectileInformation));
                        break;
                    case Static.Origin.RightCenter:
                        AddEntity(new Projectile(_textureManager.LoadTexture(e.projectileInformation.TextureName),
                        Utility.OriginMath.RightCenter(new Rectangle((int)creator.Position.X, (int)creator.Position.Y, creator.Width, creator.Height), new Rectangle(0, 0, e.projectileInformation.Width, e.projectileInformation.Height)),
                        e.projectileInformation));
                        break;
                    case Static.Origin.LeftCenter:
                        AddEntity(new Projectile(_textureManager.LoadTexture(e.projectileInformation.TextureName),
                        Utility.OriginMath.LeftCenter(new Rectangle((int)creator.Position.X, (int)creator.Position.Y, creator.Width, creator.Height), new Rectangle(0, 0, e.projectileInformation.Width, e.projectileInformation.Height)),
                        e.projectileInformation));
                        break;
                }
            }
            else
            {
                Entity newEntity = CreateEntity(e.Type, creator);
                switch (e.Origin)
                {
                    case Static.Origin.TopCenter:
                        AddEntity(newEntity);
                        break;
                    case Static.Origin.BottomCenter:
                        AddEntity(newEntity);
                        break;
                }
            }

        }

        public void Update(GameTime gameTime)
        {
            foreach (var e in _entities)
            {
                if(!e.IsActive)
                {
                    _entities.Remove(e); 
                }
                e.Update(gameTime);
            }

            foreach (var e in _toBeAdded)
            {
                _entities.Add(e);
            }
            _toBeAdded.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var e in _entities)
            {
                e.Draw(spriteBatch);
            }
        }
    }
}
