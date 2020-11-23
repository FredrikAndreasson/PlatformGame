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

        List<ShootingObstacle> shootingObstacles = new List<ShootingObstacle>();
        List<Enemy> Enemies = new List<Enemy>();


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

            for (int i = 0; i < level.Tiles.Length; i++)
            {
                if (level.Tiles[i].IDType == 90)
                {
                    int direction = Game1.random.Next(2);
                    direction = direction == 1 ? -1 : 1;
                    shootingObstacles.Add(new ShootingObstacle(loader.LoadSpriteSheet("Obstacles\\canon", Vector2.Zero, new Vector2(16, 16), 0),level,level.Tiles[i].Position,new Vector2(16*Game1.Scale.X,16*Game1.Scale.Y),new Vector2(direction, 0),loader.LoadSpriteSheet("Obstacles\\bullet",Vector2.Zero,new Vector2(16,13),0)));
                }
            }
            Enemies.Add(new Enemy(loader.LoadSpriteSheet("Enemies\\DKenemy", Vector2.Zero, new Vector2(15, 36)),level, new Vector2(100,0),new Vector2(15,36), 5, 30.0f));

        }
        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            playerVelocity = player.GetTotalVelocity(gameTime);// new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
            playerBounds = player.GetBounds(gameTime);
            //Update after player so that the player stays centered
            camera.SetPosition(player.Position);

            backgroundManager.Update(level.IsDay);

            foreach (ShootingObstacle canon in shootingObstacles)
            {
                canon.Update(gameTime);
            }
            foreach (Enemy enemy in Enemies)
            {
                enemy.Update(gameTime);
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color day = new Color(192, 2248, 248);
            Color night = new Color(0, 0, 24);
            graphicsDevice.Clear(level.IsDay ? day : night);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null);
            backgroundManager.Draw(spriteBatch);
            spriteBatch.End();


            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.Transform);
            player.Draw(spriteBatch);
            level.Draw(spriteBatch);
            foreach (ShootingObstacle canon in shootingObstacles)
            {
                canon.Draw(spriteBatch);
            }
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }


            if (debug)
            {
                foreach (Tile tile in level.Tiles)
                {
                    spriteBatch.Draw(debugTexture, tile.Bounds, new Color(0, 255, 0, 128));
                }
                spriteBatch.Draw(debugTexture, playerBounds, new Color(0,255,0,255));

                Vector2 playerMiddlePos = player.Position + (player.Size * 0.5f * Game1.Scale);
                Vector2 directionPoint = playerMiddlePos - playerVelocity;
                float angle = (float)Math.Atan2(playerMiddlePos.Y - directionPoint.Y, playerMiddlePos.X- directionPoint.X);

                spriteBatch.Draw(lineTexture, new Rectangle((int)(playerMiddlePos.X)-1, (int)(playerMiddlePos.Y), 2, 40), null, Color.White, MathHelper.ToRadians(270) + angle, Vector2.Zero, SpriteEffects.None, 1.0f);


                GameObject[] colliders = player.GetColliders(level.Tiles);
                foreach(GameObject collider in colliders)
                {
                    if (collider != null)
                    {
                        if (player.IsOnTopOf(collider))
                        {
                            spriteBatch.Draw(lineTexture, new Rectangle(collider.Bounds.X, collider.Bounds.Y, collider.Bounds.Width, 5), new Color(255, 0, 0, 255));
                        }

                        //spriteBatch.Draw(debugTexture, collider.Bounds, new Color(255, 0, 0, 128));
                    }
                }
            }
            
            spriteBatch.End();
        }
    }
}
