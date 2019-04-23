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
        Game, Menu, Options, Loading, Exit, GameOver, Gallery
    }


    public sealed class GameManager
    {
        private static readonly GameManager gameManager = new GameManager();

        private static Dictionary<State, GameState> gameStates;
        private static Dictionary<string, Prefabrication> prefabrications;

        private static GameState currentState;

        private static State state;

        private static GameTime playingTime;
        private static float playingTimeScale;


        private static CollisionManager collisionManager;

        private static Action exit;

        public static List<Prefabrication> Prefabrications { get { return prefabrications.Values.ToList(); } }

        private static event EventHandler<GameEventArgs> GameEvent;

        private static bool gamePaused;

        static GameManager() { }

        private GameManager()
        {
            gameStates = new Dictionary<State, GameState>();
        }

        public static void Init(Game1 game)
        {
            foreach(State value in Enum.GetValues(typeof(State)))
            {
                gameStates[value] = new GameState();
            }
            prefabrications = new Dictionary<string, Prefabrication>();


            exit = new Action(game.Exit);
            GameEvent += game.GameEventHandler;

            state = State.Menu;
            currentState = gameStates[state];
            playingTime = new GameTime();

            collisionManager = new CollisionManager();
            collisionManager.AddLayer("PlayerProjectile", "Enemy");
            collisionManager.AddLayer("VerticalBoundary", "Player");
            collisionManager.AddLayer("HorizantalBoundary", "Player");
            collisionManager.AddLayer("PowerUp", "Player");
            collisionManager.AddLayer("PlayerProjectile", "ProjectileBoundary");
            collisionManager.AddLayer("Player", "EnemyProjectile");

            playingTimeScale = 1;
        }

        public static void SetPlayingTimeScale(float scale)
        {
            playingTimeScale = scale;
        }

        public static void SwitchState(State newState)
        {
            
            state = newState;
            currentState = gameStates[newState];

            if(newState == State.Game)
            {
                GameEvent?.Invoke(gameManager, new GameEventArgs("InitializeGame"));
                gamePaused = false;
                Configs.SetMouseInvisible();
            }
            else
            {
                Configs.SetMouseVisible();
            }

            if(newState == State.Exit)
            {
                GameEvent?.Invoke(gameManager, new GameEventArgs("Exit"));
            }
        }

        public static void Update(GameTime gameTime)
        {
            if(state == State.Game)
            {
                playingTime.TotalGameTime += gameTime.ElapsedGameTime;
                playingTime.ElapsedGameTime = gameTime.ElapsedGameTime;

                playingTime.TotalGameTime = TimeSpan.FromTicks((long)(playingTime.TotalGameTime.Ticks * playingTimeScale));
                playingTime.ElapsedGameTime = TimeSpan.FromTicks((long)(playingTime.ElapsedGameTime.Ticks * playingTimeScale));


                currentState.Update(playingTime);
                collisionManager.CheckCollisions();

                if (Input.KeyDown("pause") && !gamePaused)
                {
                    gamePaused = true;
                    Instantiate(GetPrefab("Pause"));
                }
            }
            else
            {
                currentState.Update(gameTime);
            }
        }

        public static void UnPause()
        {
            Configs.SetMouseVisible();
            gamePaused = false;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            currentState.Draw(spriteBatch);
        }

        public static GameObject Instantiate(Prefabrication prefab)
        {
            GameObject newObject = CloneGameObject.Clone(prefab);
            currentState.AddGameObject(newObject);
            return newObject;
        }

        public static GameObject Instantiate(Prefabrication prefab, Vector2 origin)
        {
            GameObject newObject = CloneGameObject.Clone(prefab);
            ((Transform)newObject.GetComponent<Transform>()).Position = origin;
            currentState.AddGameObject(newObject);
            return newObject;
        }

        public static GameObject Instantiate(Prefabrication prefab, State state)
        {
            GameObject newObject = CloneGameObject.Clone(prefab);
            gameStates[state].AddGameObjectWithPriority(newObject);
            return newObject;
        }

        public static GameObject InstantiateWithPriority(Prefabrication prefab, State state)
        {
            GameObject newObject = CloneGameObject.Clone(prefab);
            gameStates[state].AddGameObjectWithPriority(newObject);
            return newObject;
        }

        public static GameObject Instantiate(Prefabrication prefab, Vector2 origin, State state)
        {
            GameObject newObject = CloneGameObject.Clone(prefab);
            ((Transform)newObject.GetComponent<Transform>()).Position = origin;
            gameStates[state].AddGameObject(newObject);
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

        public static void AddPrefabrication(string name, Prefabrication prefab)
        {
            try
            {
                prefabrications.Add(name, prefab);
            }
            catch(ArgumentException)
            {
                throw;
            }
        }

        public static void ClearObjects()
        {
            currentState.ClearObjects();
        }

        public static void ClearObjects(State state)
        {
            gameStates[state].ClearObjects();
        }

        public static List<GameObject> GetObjectsByTag(string tag)
        {
            return currentState.GetObjectsByTag(tag);
        }

        public static List<GameObject> GetGameObjects()
        {
            return currentState.GameObjects;
        }

        public static GameObject GetObjectByTag(string tag)
        {
            return currentState.GetObjectByTag(tag);
        }

        public static List<Component> GetComponents<T>() where T : Component
        {
            return currentState.GetComponents<T>();
        }

        public static Prefabrication GetPrefab(string name)
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

        public static List<Prefabrication> GetPrefabsByTag(string tag)
        {
            return prefabrications.Values.Where(obj => (obj.Tag == tag)).ToList();
        }
    }
}
