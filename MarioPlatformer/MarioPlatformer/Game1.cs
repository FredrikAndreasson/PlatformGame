using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MarioPlatformer
{
    public class Game1 : Game
    {
        public static Vector2 Scale = Vector2.One;

        private GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;

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

        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            spritesheetLoader = new SpriteSheetLoader(Content);


            level = new Level(spritesheetLoader, LevelData.LoadLevelData("Content\\Level1.lvl"));

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            level.Draw(SpriteBatch);
            SpriteBatch.End();

        }
    }
}
