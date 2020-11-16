using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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


        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            gameState.Draw(spriteBatch);

            spriteBatch.End();

        }
    }
}
