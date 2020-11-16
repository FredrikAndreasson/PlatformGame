using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MarioPlatformer
{
    public class Game1 : Game
    {
        public static Vector2 Scale = new Vector2(3,3);

        private GraphicsDeviceManager Graphics;
        private SpriteBatch spriteBatch;

        private Camera camera;
        private Player player;
        private ScrollingBackground scrollingBackground;

        //GameStates
        GameState gameState;
        InGameState inGameState;


        private SpriteSheetLoader spritesheetLoader;

        private Level level;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            TileType[,] tiles = new TileType[2, 2];
            tiles[0, 0] = TileType.Empty;
            tiles[1, 0] = TileType.Empty;
            tiles[0, 1] = TileType.Empty;
            tiles[1, 1] = TileType.Block3;
            LevelData data = new LevelData(@"blocks", 2, 2, tiles);

            LevelData.SaveData(data, "Content\\Level1.lvl");

            Graphics.PreferredBackBufferWidth = 1280;
            Graphics.PreferredBackBufferHeight = 900;

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spritesheetLoader = new SpriteSheetLoader(Content);
            SpriteSheet playerAnimationSheet = spritesheetLoader.LoadSpriteSheet("player", new Vector2(0, 0), new Vector2(12, 16), new Vector2(12, 16));
            Texture2D backgroundTex = spritesheetLoader.LoadTexture("scrollingBackground");

            player = new Player(playerAnimationSheet, new Vector2(250, 250), 5, 100.0f);
            camera = new Camera(GraphicsDevice.Viewport);
            scrollingBackground = new ScrollingBackground(backgroundTex, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height));

            level = new Level(spritesheetLoader, LevelData.LoadLevelData("Content\\Level1.lvl"));

            inGameState = new InGameState(player, camera, scrollingBackground);
            gameState = inGameState;

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameState.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null, null, camera.Transform);

            gameState.Draw(spriteBatch);

            level.Draw(spriteBatch);
            spriteBatch.End();

        }
    }
}
