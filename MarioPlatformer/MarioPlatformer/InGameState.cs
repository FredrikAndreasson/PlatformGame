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
        private Camera camera;

        private Level level;


        GraphicsDevice graphicsDevice;

        ParalaxBackgroundManager backgroundManager;

        private bool debug = true;
        private Texture2D debugTexture;
        private Texture2D collisionTexture;


        public InGameState(SpriteSheetLoader loader, GraphicsDevice graphicsDevice, GameWindow window)
        {

            this.graphicsDevice = graphicsDevice;

            this.level = new Level(loader, LevelData.LoadLevelData("Content\\Level1.lvl"));
            

            this.debugTexture = loader.CreateRectangleTexture((int)(Tile.SIZE * Game1.Scale.X), (int)(Tile.SIZE * Game1.Scale.Y), new Color(255, 255, 255, 255));
            this.collisionTexture = loader.CreateFilledTexture((int)(Tile.SIZE * Game1.Scale.X), (int)(Tile.SIZE * Game1.Scale.Y), new Color(255, 255, 255, 255));

            this.camera = new Camera(graphicsDevice.Viewport);

            backgroundManager = new ParalaxBackgroundManager(level.MyPlayer, loader, graphicsDevice, window);

            for (int i = 0; i < level.Tiles.Length; i++)
            {
                if (level.Tiles[i].IDType == 90)
                {
                    int direction = Game1.random.Next(2);
                    direction = direction == 1 ? -1 : 1;
                    ShootingObstacle shooting = new ShootingObstacle(loader.LoadSpriteSheet("Obstacles\\canon", Vector2.Zero, new Vector2(16, 16), 0),level,level.Tiles[i].Position,new Vector2(16*Game1.Scale.X,16*Game1.Scale.Y),new Vector2(direction, 0),loader.LoadSpriteSheet("Obstacles\\bullet",Vector2.Zero,new Vector2(16,13),0));
                    level.AddEnemy(shooting);
                }
                if (level.Tiles[i].IDType == 91)
                {
                    Enemy patrolling = new PatrollingEnemy(loader.LoadSpriteSheet("Enemies\\DKenemy", Vector2.Zero, new Vector2(16, 22), 1),level, new Vector2(100,100), new Vector2(16*Game1.Scale.X, 16*Game1.Scale.Y),1, 70.0f);
                    level.AddEnemy(patrolling);
                }
            }
            

            
        }
        public override void Update(GameTime gameTime)
        {
            level.Update(gameTime, camera);
            camera.CenterOn(level.MyPlayer.Position);
            camera.SetPosition(new Vector2(camera.Transform.Translation.X, camera.Bounds.Y > 0 ? camera.Transform.Translation.Y : 0));


            backgroundManager.Update(level.IsDay);
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
            level.Draw(spriteBatch);


            if (debug)
            {
                foreach (Tile tile in level.Tiles)
                {
                    spriteBatch.Draw(debugTexture, tile.Bounds, new Color(0, 255, 0, 128));
                }

                //spriteBatch.Draw(debugTexture, player.Bounds, new Color(0, 255, 0, 128));

                GameObject[] colliders = level.MyPlayer.GetColliders(level.Tiles);
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
