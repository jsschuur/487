using _487Game.Collision;
using _487Game.Components;
using _487Game.Components.EnemyBehaviorComponents;
using _487Game.EnemyBehavior;
using _487Game.Entities;
using _487Game.Input;
using _487Game.Level;
using _487Game.Screen;
using _487Game.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using yeet1.Texture;

namespace _487Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        EntityManager entityManager;
        TextureManager textureManager;

        Screen.Screen screen;

        

        Matrix Scale;

        GameTime entityGameTime;

        KeyboardManager keyboardManager;

        Wave w;

        Static.GameState GameState;

        bool pause;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

   

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            Content.RootDirectory = "Content";
            
            textureManager = new TextureManager(Content);
            entityManager = new EntityManager(textureManager);
            keyboardManager = new KeyboardManager();

            keyboardManager.NewInput += InputEventHandler;


            screen = new Screen.Screen();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            entityGameTime = new GameTime();
            
            float scaleX = graphics.PreferredBackBufferWidth / Constants.TargetWidth;
            float scaleY = graphics.PreferredBackBufferHeight / Constants.TargetHeight;
            Scale = Matrix.CreateScale(new Vector3(scaleX, scaleY, 1));


            screen.AddComponent(new EmptyHealthBar(textureManager.LoadTexture("EmptyHealthBar")));

            Player player = new Player(entityManager.GetId(), "player", keyboardManager, entityManager);

            player.AddComponent(new SpriteComponent(player, textureManager.LoadTexture("Player"), 64, 64, false));

            player.AddComponent(new PositionComponent(player, new Vector2(200, 400)));
            player.AddComponent(new MovementComponent(player, 180));
            player.AddComponent(new ProjectileComponent(player, entityManager));
            player.AddComponent(new CollisionComponent(player, new CircleHitbox(28), new EntityCollisionAction()));
            player.AddComponent(new HealthComponent(player, 100));
            player.AddComponent(new PlayerAttackComponent(player, 100, new ProjectileInformation("BasicProjectile", 10, 300, 8, 16, 0f)));


            entityManager.AddEntity(player);

            entityManager.InitializePlayer(player);
            

            screen.AddComponent(new PlayerHealth(textureManager.LoadTexture("PlayerHealthBar"), (HealthComponent)player.GetComponent("HealthComponent")));
            w = WaveLoader.LoadWave("Content/wave1.json", entityManager, textureManager);
            base.Initialize();

            GameState = Static.GameState.Playing;
        }
            
        private void InputEventHandler(object sender, NewInputEventArgs e)
        {
            switch(e.Input)
            {
                case Static.Input.Pause:
                    if (pause == true)
                        pause = false;
                    else
                        pause = true;
                    break;
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            keyboardManager.Update(gameTime);

            
            switch(GameState)
            {
                case Static.GameState.Menu:
                    break;
                case Static.GameState.Paused:
                    break;
                case Static.GameState.Playing:
                    entityGameTime.TotalGameTime += gameTime.ElapsedGameTime;
                    entityGameTime.ElapsedGameTime = gameTime.ElapsedGameTime;
                    w.Update(entityGameTime);
                    entityManager.Update(entityGameTime);
                    screen.Update(entityGameTime);
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Scale);

           

            entityManager.Draw(spriteBatch);
            screen.Draw(spriteBatch);
            // TODO: Add your drawing code here

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
