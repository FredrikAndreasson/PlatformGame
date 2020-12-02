using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarioPlatformer
{
    class InGameState : GameState
    {
        private Camera camera;

        private Level level;
        private HUD hud;

        private SpriteFont font;

        int currentLvlIndex = 1;
        private SpriteSheetLoader loader;

        private bool gameOver;

        public bool restart;

        private GraphicsDevice graphicsDevice;
        private ParalaxBackgroundManager backgroundManager;
                
        private bool highscoresSaved;

        private bool debug = false;
        private Texture2D debugTexture;
        private Texture2D collisionTexture;

        public InGameState(SpriteSheetLoader loader, GraphicsDevice graphicsDevice, GameWindow window, HUD hud, SpriteFont font)
        {
            this.loader = loader;

            this.graphicsDevice = graphicsDevice;

            this.hud = hud; 
            this.level = new Level(loader, LevelData.LoadLevelData("Content\\Level1.lvl"), hud);

            this.hud.Player = level.MyPlayer;

            this.font = font;

            this.debugTexture = loader.CreateRectangleTexture((int)(Tile.SIZE * Game1.Scale.X), (int)(Tile.SIZE * Game1.Scale.Y), new Color(255, 255, 255, 255));
            this.collisionTexture = loader.CreateFilledTexture((int)(Tile.SIZE * Game1.Scale.X), (int)(Tile.SIZE * Game1.Scale.Y), new Color(255, 255, 255, 255));

            this.camera = new Camera(graphicsDevice.Viewport);

            backgroundManager = new ParalaxBackgroundManager(level.MyPlayer, loader, graphicsDevice, window);
        }

        private void CheckNextLevel()
        {
            if (!level.LvlWon && !restart)
            {
                return;
            }
            currentLvlIndex++;
            if (currentLvlIndex>5)
            {
                gameOver = true;
                currentLvlIndex = 5;
                return;
            }
            int health = level.MyPlayer.Health;
            this.level = new Level(loader, LevelData.LoadLevelData("Content\\Level" + currentLvlIndex + ".lvl"), hud);
            hud.CurrentLevel = this.level;
            level.MyPlayer.Health = health;
            hud.Player = level.MyPlayer;

        }

        public override void Update(GameTime gameTime)
        {
            if (!gameOver)
            {
                level.Update(gameTime, camera);
                camera.CenterOn(level.MyPlayer.Position);
                camera.SetPosition(new Vector2(camera.Transform.Translation.X, camera.Bounds.Y > 0 ? camera.Transform.Translation.Y : 0));

                if (level.MyPlayer.Health <= 0)
                {
                    gameOver = true;
                }

                backgroundManager.Update(level.IsDay);

                CheckNextLevel();

                if (gameOver && !highscoresSaved)
                {
                    hud.Highscores.Add(hud.Score);
                    hud.SaveHighscores();
                    highscoresSaved = true;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                currentLvlIndex = 0;
                gameOver = false;
                restart = true;
                CheckNextLevel();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!gameOver)
            {
                Color day = new Color(192, 2248, 248);
                Color night = new Color(0, 0, 24);
                graphicsDevice.Clear(level.IsDay ? day : night);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null);
                backgroundManager.Draw(spriteBatch);
                spriteBatch.End();


                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.Transform);
                level.Draw(spriteBatch);


                if (debug)
                {
                    foreach (Tile tile in level.Tiles)
                    {
                        spriteBatch.Draw(debugTexture, tile.Bounds, new Color(0, 255, 0, 128));
                    }

                    foreach (Enemy enemy in level.Enemies)
                    {
                        spriteBatch.Draw(debugTexture, enemy.Bounds, new Color(0, 255, 0, 128));
                    }
                    //spriteBatch.Draw(debugTexture, player.Bounds, new Color(0, 255, 0, 128));

                    GameObject[] colliders = level.MyPlayer.GetColliders(level.Tiles);
                    foreach (GameObject collider in colliders)
                    {
                        if (collider != null)
                        {

                            spriteBatch.Draw(collisionTexture, collider.Bounds, new Color(255, 0, 0, 255));
                        }
                    }
                }

                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null);
                hud.Draw(spriteBatch);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Press 'P' to continue", new Vector2(350, 250), Color.AliceBlue);
                spriteBatch.End();
            }
        }

       
    }
}
