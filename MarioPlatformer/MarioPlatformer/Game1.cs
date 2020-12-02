using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarioPlatformer
{
    public class Game1 : Game
    {

        public static Random random = new Random();
        public static Vector2 Scale = new Vector2(2,2);

        private GraphicsDeviceManager Graphics;
        private SpriteBatch spriteBatch;
        
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
            
            Graphics.PreferredBackBufferWidth = 1280;
            Graphics.PreferredBackBufferHeight = 900;

        }
    

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spritesheetLoader = new SpriteSheetLoader(Content, GraphicsDevice);    
            
            SpriteFont font = Content.Load<SpriteFont>(@"font");

            HUD hud = new HUD(Window, 100, 100, spritesheetLoader.LoadTexture("score-numbers"), spritesheetLoader.LoadSpriteSheet("player", Vector2.Zero, new Vector2(16, 16)).GetAt(0, 0));

            menu = new MenuState(font, Window, hud);

            menu.Actions.Add(new MenuOption("1. Start Game", () => gameState = inGameState));
            menu.Actions.Add(new MenuOption("2. Open Editor", () => gameState = editor));
            menu.Actions.Add(new MenuOption("3. Exit", () => Exit()));

            inGameState = new InGameState(spritesheetLoader, GraphicsDevice, Window, hud);
            editor = new Editor(spritesheetLoader, Window);
            editor.LoadLevel("Content\\Level1.lvl");
            gameState = menu;
            //gameState = editor;           

        }

        

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            gameState.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);
            gameState.Draw(spriteBatch);

        }
    }
}
