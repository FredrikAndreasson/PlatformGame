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
        private Texture2D collisionTexture;
        
        //Move Enemy logic to the level class
        List<ShootingObstacle> shootingObstacles = new List<ShootingObstacle>();
        List<Enemy> Enemies = new List<Enemy>();


        public InGameState(SpriteSheetLoader loader, GraphicsDevice graphicsDevice, GameWindow window)
        {
            SpriteSheet playerAnimationSheet = loader.LoadSpriteSheet("player");

            this.graphicsDevice = graphicsDevice;

            this.level = new Level(loader, LevelData.LoadLevelData("Content\\Level1.lvl"));
            this.player = new Player(playerAnimationSheet, level, new Vector2(0, 250),new Vector2(16,16), 5, 200.0f);

            this.debugTexture = loader.CreateRectangleTexture((int)(Tile.SIZE * Game1.Scale.X), (int)(Tile.SIZE * Game1.Scale.Y), new Color(255, 255, 255, 255));
            this.collisionTexture = loader.CreateFilledTexture((int)(Tile.SIZE * Game1.Scale.X), (int)(Tile.SIZE * Game1.Scale.Y), new Color(255, 255, 255, 255));

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
            //Enemies.Add(new Enemy(loader.LoadSpriteSheet("Enemies\\DKenemy", Vector2.Zero, new Vector2(15, 36)),level, new Vector2(100,0),new Vector2(15,36), 5, 30.0f));

            
        }
        private void PlayerCollisionHandling()
        {
            foreach (ShootingObstacle canon in shootingObstacles)
            {
                for (int i = canon.BulletList.Count-1; i > -1; i--)
                {
                    if (canon.BulletList[i].Bounds.Intersects(player.Bounds))
                    {
                        player.Death(new Vector2(100, 100)); // Die unless you jump ontop of the enemy 
                        canon.BulletList.RemoveAt(i);
                    }
                }
            }
        }
    public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            //Update after player so that the player stays centered
            camera.CenterOn(player.Position);
            camera.SetPosition(new Vector2(camera.Transform.Translation.X, camera.Bounds.Y > 0 ? camera.Transform.Translation.Y : 0));


            backgroundManager.Update(level.IsDay);

            foreach (ShootingObstacle canon in shootingObstacles)
            {
                canon.Update(gameTime);
            }
            foreach (Enemy enemy in Enemies)
            {
                enemy.Update(gameTime);
            }

            PlayerCollisionHandling();

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

                //spriteBatch.Draw(debugTexture, player.Bounds, new Color(0, 255, 0, 128));

                GameObject[] colliders = player.GetColliders(level.Tiles);
                foreach(GameObject collider in colliders)
                {
                    if (collider != null)
                    {

                        spriteBatch.Draw(collisionTexture, collider.Bounds, new Color(255, 0, 0, 255));
                    }
                }
            }
            
            spriteBatch.End();
        }
    }
}
