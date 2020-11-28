using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class Level
    {

        private LevelData levelData;
        private SpriteSheet spritesheet;
        
        private Tile[] tiles;

        private bool isDay = false;

        private Player player;
        private List<Enemy> enemies;        

        public Level(SpriteSheetLoader loader, LevelData levelData)
        {
            this.levelData = levelData;

            SpriteSheet playerAnimationSheet = loader.LoadSpriteSheet("player");

            this.player = new Player(playerAnimationSheet, this, new Vector2(0, 250), new Vector2(16, 16), 5, 200.0f);

            this.enemies = new List<Enemy>();

            Create(loader);
        }


        public bool IsDay => isDay;

        public Tile[] Tiles => tiles;

        public Player MyPlayer => player;

        private void Create(SpriteSheetLoader loader)
        {
            this.spritesheet = loader.LoadSpriteSheet(levelData.SpriteSheetFilePath, Vector2.Zero, new Vector2(16, 16), 0);

            this.tiles = new Tile[levelData.Size];
            for(int i = 0; i < tiles.Length;i++)
            {
                Tile tile = levelData.Tiles[i];

                int sx = (int)tile.IDType % spritesheet.Columns;
                int sy = (int)tile.IDType / spritesheet.Columns;

                SpriteSheet sheet = loader.LoadSpriteSheet(levelData.SpriteSheetFilePath, Vector2.Zero, new Vector2(16, 16), 0);
                sheet.XIndex = sx;
                sheet.YIndex = sy;

                tiles[i] = new Tile(sheet, this, tile.Position, tile.IDType);
            }
        }

        

        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        private void PlayerCollision(Enemy enemy)
        {
            if (player.Bounds.Intersects(enemy.Bounds) && player.Position.Y < enemy.Position.Y)
            {
                 enemy.isDead = true;
            }
            else if(player.Bounds.Intersects(enemy.Bounds))
            {
                enemy.isDead = true;
                player.Death(new Vector2(100,100));
            }
        }

        private void DeadEnemyCheck()
        {
            for (int i = enemies.Count-1; i > -1; i--)
            {
                if (enemies[i].isDead)
                {
                    enemies.RemoveAt(i);
                }
            }
        }

        public void Update(GameTime gameTime, Camera camera)
        {
            player.Update(gameTime);
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(gameTime);

                if (enemies[i] is ShootingObstacle)
                {
                    continue;
                }
                PlayerCollision(enemies[i]);
            }
            DeadEnemyCheck();

        }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
            foreach (Tile tile in tiles)
            {
                if (tile.IDType != 91)
                {
                    tile.Draw(spriteBatch);
                }
                
            }
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
        
    }
}
