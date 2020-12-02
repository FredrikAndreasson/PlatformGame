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
            menu = new MenuState(font);

            menu.Actions.Add(new MenuOption("1. Start Game", () => System.Diagnostics.Debug.WriteLine("StartGame")));
            menu.Actions.Add(new MenuOption("2. Switch Character", () => System.Diagnostics.Debug.WriteLine("Switching Character")));
            menu.Actions.Add(new MenuOption("3. Open Editor", () => System.Diagnostics.Debug.WriteLine("Opening Editor")));
            menu.Actions.Add(new MenuOption("4. Exit", () => Exit()));

            inGameState = new InGameState(spritesheetLoader, GraphicsDevice, Window);
            editor = new Editor(spritesheetLoader, Window);
            editor.LoadLevel( "Content\\Level1.lvl");
            gameState = inGameState;
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
