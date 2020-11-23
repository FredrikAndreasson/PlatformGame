using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class InGameState : GameState
    {
        private Player player;
        private Camera camera;

        private Level level;


        GraphicsDevice graphicsDevice;

        ParalaxBackgroundManager backgroundManager;

        private bool debug = true;
        private Texture2D debugTexture;
        private Texture2D lineTexture;

        private Vector2 playerVelocity = Vector2.Zero;
        private Rectangle playerBounds = new Rectangle(0,0,0,0);

        public InGameState(SpriteSheetLoader loader, GraphicsDevice graphicsDevice, GameWindow window)
        {
            SpriteSheet playerAnimationSheet = loader.LoadSpriteSheet("player", new Vector2(0, 0), new Vector2(12, 16), new Vector2(12, 16));


            this.graphicsDevice = graphicsDevice;

            this.level = new Level(loader, LevelData.LoadLevelData("Content\\Level1.lvl"));
            this.player = new Player(playerAnimationSheet, level, new Vector2(0, 250),new Vector2(12,16), 5, 200.0f);

            this.debugTexture = loader.CreateRectangleTexture((int)(Tile.SIZE * Game1.Scale.X), (int)(Tile.SIZE * Game1.Scale.Y), new Color(255, 255, 255, 255));
            this.lineTexture = loader.CreateFilledTexture(1, 1, Color.White);

            this.camera = new Camera(graphicsDevice.Viewport);

            backgroundManager = new ParalaxBackgroundManager(player, loader, graphicsDevice, window);

        }
        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            playerVelocity = player.GetTotalVelocity(gameTime);// new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
            playerBounds = player.GetBounds(gameTime);
            //Update after player so that the player stays centered
            camera.SetPosition(player.Position);

            backgroundManager.Update(level.IsDay);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color day = new Color(192, 2248, 248);
            Color night = new Color(0, 0, 24);
            graphicsDevice.Clear(level.IsDay ? day : night);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, null, null);
            backgroundManager.Draw(spriteBatch);
            spriteBatch.End();


            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.Transform);
            player.Draw(spriteBatch);
            level.Draw(spriteBatch);

            if(debug)
            {
                foreach (Tile tile in level.Tiles)
                {
                    spriteBatch.Draw(debugTexture, tile.Bounds, new Color(0, 255, 0, 255));
                }
                spriteBatch.Draw(debugTexture, playerBounds, new Color(0,255,0,255));

                Vector2 playerMiddlePos = player.Position + (player.Size * 0.5f * Game1.Scale);
                Vector2 directionPoint = playerMiddlePos - playerVelocity;
                float angle = (float)Math.Atan2(playerMiddlePos.Y - directionPoint.Y, playerMiddlePos.X- directionPoint.X);

                spriteBatch.Draw(lineTexture, new Rectangle((int)(playerMiddlePos.X)-1, (int)(playerMiddlePos.Y), 2, 40), null, Color.White, MathHelper.ToRadians(270) + angle, Vector2.Zero, SpriteEffects.None, 1.0f);


                GameObject[] colliders = player.GetColliders(level.Tiles);
                foreach(GameObject collider in colliders)
                {
                    if (collider != null && player.IsOnTopOf(collider))
                    {
                        spriteBatch.Draw(debugTexture, collider.Bounds, new Color(255, 0, 0, 255));
                    }
                }
            }
            
            spriteBatch.End();
        }
    }
}
