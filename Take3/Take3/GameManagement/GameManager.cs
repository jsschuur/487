using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.ECS;
using Take3.ECS.Collision;
using Take3.Utility;

namespace Take3.GameManagement
{

    public enum State
    {
        Game, Menu, Pause, Loading, Exit
    }


    public sealed class GameManager
    {
        private static readonly GameManager gameManager = new GameManager();

        private static Dictionary<State, GameState> gameStates;
        private static Dictionary<string, GameObject> prefabrications;
        private static GameState currentState;

        private static State state;
        private static GameTime playingTime;
        private static Configs configs;

        private static CollisionManager collisionManager;

        private static Action exit;

        public static List<GameObject> GameObjects { get { return prefabrications.Values.ToList(); } }

        public static void Init(Game game)
        {
            foreach(State value in Enum.GetValues(typeof(State)))
            {
                gameStates[value] = new GameState();
            }
            prefabrications = new Dictionary<string, GameObject>();

            configs = new Configs(game);

            configs.SetMouseVisible();

            exit = new Action(game.Exit);

            state = State.Menu;
            currentState = gameStates[state];
            playingTime = new GameTime();

            collisionManager = new CollisionManager();
            collisionManager.AddLayer("PlayerProjectile", "Enemy");
            collisionManager.AddLayer("VerticalBoundary", "Player");
            collisionManager.AddLayer("HorizantalBoundary", "Player");
            collisionManager.AddLayer("PlayerProjectile", "ProjectileBoundary");
        }

        static GameManager() { }

        private GameManager()
        {
            gameStates = new Dictionary<State, GameState>();
        }

        public static void SwitchState(State newState)
        {
            currentState = gameStates[newState];
            state = newState;

            if(newState == State.Game)
            {
                configs.SetMouseInvisible();
            }

            if(newState == State.Exit)
            {
                exit();
            }
            else
            {
                configs.SetMouseVisible();
            }
        }

        public static void Update(GameTime gameTime)
        {
            if(state == State.Game)
            {
                playingTime.TotalGameTime += gameTime.ElapsedGameTime;
                playingTime.ElapsedGameTime = gameTime.ElapsedGameTime;
               
                currentState.Update(playingTime);
                collisionManager.CheckCollisions();
            }
            else
            {
                currentState.Update(gameTime);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            currentState.Draw(spriteBatch);
        }

        public static GameObject Instantiate(GameObject prefab)
        {
            GameObject newObject = CloneGameObject.Clone(prefab);
            currentState.AddGameObject(newObject);
            return newObject;
        }

        public static GameObject Instantiate(GameObject prefab, Vector2 origin)
        {
            GameObject newObject = CloneGameObject.Clone(prefab);
            ((Transform)newObject.GetComponent<Transform>()).Position = origin;
            currentState.AddGameObject(newObject);
            return newObject;
        }

        public static void AddGameObject(GameObject obj)
        {
            currentState.AddGameObject(obj);
        }

        public static void AddGameObject(GameObject obj, State state)
        {
            gameStates[state].AddGameObject(obj);
        }

        public static void AddPrefabrication(string name, GameObject obj)
        {
            try
            {
                prefabrications.Add(name, obj);
            }
            catch(ArgumentException)
            {
                throw;
            }
        }

        public static List<GameObject> GetObjectsByTag(string tag)
        {
            return currentState.GetObjectsByTag(tag);
        }

        public static GameObject GetObjectByTag(string tag)
        {
            return currentState.GetObjectByTag(tag);
        }

        public static GameObject GetPrefab(string name)
        { 
            try
            {
                return prefabrications[name];
            }
            catch(KeyNotFoundException)
            {
                throw;
            }
        }

        public static List<GameObject> GetPrefabsByTag(string tag)
        {
            return prefabrications.Values.Where(obj => (obj.Tag == tag)).ToList();
        }
    }
}
