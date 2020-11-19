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
        private GameState gameState;
        private InGameState inGameState;
        private MenuState menu;
        private Editor editor;

        private SpriteSheetLoader spritesheetLoader;


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

            Graphics.PreferredBackBufferWidth = 1280;
            Graphics.PreferredBackBufferHeight = 900;

        }
    

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spritesheetLoader = new SpriteSheetLoader(Content, GraphicsDevice);    
            
            SpriteFont font = Content.Load<SpriteFont>(@"font");
            menu = new MenuState(font);

            menu.Actions.Add(new MenuOption("1. Start Game", () => System.Diagnostics.Debug.WriteLine("StartGame")));
            menu.Actions.Add(new MenuOption("2. Switch Character", () => System.Diagnostics.Debug.WriteLine("Switching Character")));
            menu.Actions.Add(new MenuOption("3. Open Editor", () => System.Diagnostics.Debug.WriteLine("Opening Editor")));
            menu.Actions.Add(new MenuOption("4. Exit", () => Exit()));

            inGameState = new InGameState(spritesheetLoader, GraphicsDevice, Window);
            editor = new Editor(spritesheetLoader, Window);
            //gameState = inGameState;
            gameState = editor;

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
            gameState.Draw(spriteBatch);
        }
    }
}
