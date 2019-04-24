using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Take3.ECS;
using Take3.ECS.Scripts;
using Take3.ECS.Scripts.Collision;
using Take3.GameManagement;
using Take3.LevelManagement;
using Take3.Screen;
using Take3.Utility;

namespace Take3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            base.Initialize();
            
            GameManager.Init(this);
            TextureManager.Init(Content);
            Configs.Initialize(this, graphics);
            
            JSONLoader.LoadJSONObjects("Content/GameObjects/MainMenu.json");
            JSONLoader.LoadJSONObjects("Content/GameObjects/Projectiles.json");
            JSONLoader.LoadJSONObjects("Content/GameObjects/wave1.json");
            JSONLoader.LoadJSONObjects("Content/GameObjects/GameBoundaries.json");
            JSONLoader.LoadJSONObjects("Content/GameObjects/Player.json");
            JSONLoader.LoadJSONObjects("Content/GameObjects/Enemies.json");
            JSONLoader.LoadJSONObjects("Content/GameObjects/GameScreen.json");
            JSONLoader.LoadJSONObjects("Content/GameObjects/Gallery.json");
            JSONLoader.LoadJSONObjects("Content/GameObjects/Pause.json");
            JSONLoader.LoadJSONObjects("Content/GameObjects/OptionsMenu.json");


            InitializeOptions();
            InitializeGallery();
            InitializeMainMenu();

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Resolution.Update(graphics);

            GameObject playButton = new GameObject();
        }
        

        public void GameEventHandler(object sender, GameEventArgs e)
        {
            switch(e.Event)
            {
                case "InitializeGame":
                    InitializeGame(); break;
                case "Exit":
                    Exit(); break;
            }
        }

        private void InitializeGallery()
        {
            foreach(var obj in GameManager.GetPrefabsByTag("Gallery"))
            {
                GameManager.Instantiate(obj, State.Gallery);
            }
        }

        private void InitializeGame()
        {
            GameManager.ClearObjects(State.Game);

            GameManager.Instantiate(GameManager.GetPrefab("LeftPlayerBoundary"), State.Game);
            GameManager.Instantiate(GameManager.GetPrefab("TopPlayerBoundary"), State.Game);
            GameManager.Instantiate(GameManager.GetPrefab("RightPlayerBoundary"), State.Game);
            GameManager.Instantiate(GameManager.GetPrefab("BottomPlayerBoundary"), State.Game);

            GameManager.Instantiate(GameManager.GetPrefab("Player"), State.Game);

            foreach (var obj in GameManager.GetPrefabsByTag("GameWindow"))
            {
                GameManager.Instantiate(obj, State.Game);
            }

            foreach (var obj in GameManager.GetPrefabsByTag("ProjectileBoundary"))
            {
                GameManager.Instantiate(obj, State.Game);
            }
            foreach (var obj in GameManager.GetPrefabsByTag("ScreenComponent"))
            {
                GameManager.Instantiate(obj, State.Game);
            }



            GameManager.Instantiate(GameManager.GetPrefab("Wave1"), State.Game);
        }

        private void InitializeOptions()
        {
            GameManager.SwitchState(State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("OptionsBackground"), State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("LeftResolutionButton"), State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("RightResolutionButton"), State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("LeftSpeedButton"), State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("RightSpeedButton"), State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("OptionsBackButton"), State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("EditLeftButton"), State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("EditRightButton"), State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("EditUpButton"), State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("EditDownButton"), State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("EditSlowButton"), State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("EditShootButton"), State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("EditPauseButton"), State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("ResolutionScript"), State.Options);
            GameManager.Instantiate(GameManager.GetPrefab("GamespeedScript"), State.Options);
            GameManager.SwitchState(State.Menu);
        }

        private void InitializeMainMenu()
        {
            foreach(var prefab in GameManager.GetPrefabsByTag("MainMenu"))
            {
                GameManager.Instantiate(prefab, State.Menu);
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
            GameManager.Update(gameTime);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.IndianRed);

            spriteBatch.Begin(transformMatrix: Resolution.ScaleMatrix, sortMode: SpriteSortMode.FrontToBack);

            GameManager.Draw(spriteBatch);

            spriteBatch.End();

            

            base.Draw(gameTime);
        }
    }
}
