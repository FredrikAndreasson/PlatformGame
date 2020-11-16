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

        //Different spritesheets
        Texture2D blockSheet;
        Texture2D enemySheet;
        Texture2D playerSheet;

        private Camera camera;
        private Player player;

        //GameStates
        GameState gameState;
        InGameState inGameState;


        private SpriteSheetLoader spritesheetLoader;

        private Level level;
        private MenuState menu;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Tile[] tiles = new Tile[4];
            tiles[0] = new Tile(null, new Vector2(0, 0), TileType.Empty);
            tiles[1] = new Tile(null, new Vector2(16 * 3, 0), TileType.Block1);
            tiles[2] = new Tile(null, new Vector2(32 * 3, 0), TileType.Block2);
            tiles[3] = new Tile(null, new Vector2(0, 16 * 3), TileType.Block2);
            LevelData data = new LevelData(@"blocks", tiles);

            LevelData.SaveData(data, "Content\\Level1.lvl");

            
        }
    

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            blockSheet = Content.Load<Texture2D>("blocks");
            enemySheet = Content.Load<Texture2D>("enemies");
            playerSheet = Content.Load<Texture2D>("player");

            SpriteSheet playerAnimationSheet = new SpriteSheet(playerSheet, new Vector2(0, 0), new Vector2(12, 16), new Vector2(12, 16));

            camera = new Camera(GraphicsDevice.Viewport);
            player = new Player(playerAnimationSheet, new Vector2(0, 0), 5, 100.0f);

            inGameState = new InGameState(player, camera);
            gameState = inGameState;

            spritesheetLoader = new SpriteSheetLoader(Content);


            level = new Level(spritesheetLoader, LevelData.LoadLevelData("Content\\Level1.lvl"));

            SpriteFont font = Content.Load<SpriteFont>(@"font");

            menu = new MenuState(font);

            menu.Actions.Add(new MenuOption("1. Start Game", () => System.Diagnostics.Debug.WriteLine("StartGame")));
            menu.Actions.Add(new MenuOption("2. Switch Character", () => System.Diagnostics.Debug.WriteLine("Switching Character")));
            menu.Actions.Add(new MenuOption("3. Open Editor", () => System.Diagnostics.Debug.WriteLine("Opening Editor")));
            menu.Actions.Add(new MenuOption("4. Exit", () => Exit()));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            menu.Update(gameTime);

            gameState.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            gameState.Draw(spriteBatch);
            level.Draw(spriteBatch);
            menu.Draw(spriteBatch);

            spriteBatch.End();


        }
    }
}
