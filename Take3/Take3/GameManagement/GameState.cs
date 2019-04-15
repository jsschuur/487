using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.ECS;

namespace Take3.GameManagement
{
    public class GameState
    {
        private List<GameObject> gameObjects;

        private List<GameObject> added;
        private List<GameObject> removed;

        public GameState()
        {
            gameObjects = new List<GameObject>();
            added = new List<GameObject>();
            removed = new List<GameObject>();
        }

        public void Update(GameTime gameTime)
        {
            foreach(var obj in gameObjects)
            {
                foreach(var behavior in obj.GetComponents<Updatable>())
                {
                    ((Updatable)behavior).Update(gameTime);
                }

                if(!obj.IsAlive)
                {
                    removed.Add(obj);
                }
            }

            foreach (var obj in added) gameObjects.Add(obj);
            foreach (var obj in removed) gameObjects.Remove(obj);

            added.Clear();
            removed.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var obj in gameObjects.Where(obj => (obj.HasComponent<Renderer>())))
            {
                ((Renderer)obj.GetComponent<Renderer>()).Draw(spriteBatch);
            }
        }

        public void AddGameObject(GameObject obj)
        {
            added.Add(obj);
        }

        public void RemoveGameObject(GameObject obj)
        {
            removed.Add(obj);
        }

        public List<GameObject> GetObjectsByTag(string tag)
        {
            return gameObjects.Where(obj => (obj.Tag == tag)).ToList();
        }

        public GameObject GetObjectByTag(string tag)
        {
            return gameObjects.Where(obj => (obj.Tag == tag)).First();
        }

    }
}
