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

        
            JSONLoader.LoadJSONObjects("Content/mainmenu.json");
            JSONLoader.LoadJSONObjects("Content/Projectiles.json");
            JSONLoader.LoadJSONObjects("Content/wave1.json");
            JSONLoader.LoadJSONObjects("Content/GameObjects.json");
            JSONLoader.LoadJSONObjects("Content/GameBoundaries.json");

            JSONLoader.LoadJSONObjects("Content/Enemies.json");


            GameManager.AddGameObject(GameManager.GetPrefab("PlayButton"));
            GameManager.AddGameObject(GameManager.GetPrefab("ExitButton"));
            GameManager.AddGameObject(GameManager.GetPrefab("OptionsButton"));
            GameManager.AddGameObject(GameManager.GetPrefab("MainMenu"));

            GameManager.AddGameObject(GameManager.GetPrefab("Player"), State.Game);
            GameManager.AddGameObject(GameManager.GetPrefab("LeftPlayerBoundary"), State.Game);
            GameManager.AddGameObject(GameManager.GetPrefab("TopPlayerBoundary"), State.Game);
            GameManager.AddGameObject(GameManager.GetPrefab("RightPlayerBoundary"), State.Game);
            GameManager.AddGameObject(GameManager.GetPrefab("BottomPlayerBoundary"), State.Game);

            GameManager.AddGameObject(GameManager.GetPrefab("Midboss"), State.Game);

            foreach(var obj in GameManager.GetPrefabsByTag("GameWindow"))
            {
                GameManager.AddGameObject(obj, State.Game);
            }

            foreach (var obj in GameManager.GetPrefabsByTag("ProjectileBoundary"))
            {
                GameManager.AddGameObject(obj, State.Game);
            }


            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Resolution.Update(graphics);

            GameObject playButton = new GameObject();
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

            spriteBatch.Begin(samplerState: SamplerState.LinearWrap, transformMatrix: Resolution.ScaleMatrix, sortMode: SpriteSortMode.FrontToBack);

            GameManager.Draw(spriteBatch);

            spriteBatch.End();

            Console.WriteLine(1 / (float)gameTime.ElapsedGameTime.TotalSeconds + "\n");

            base.Draw(gameTime);
        }
    }
}
