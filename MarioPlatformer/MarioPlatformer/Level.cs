using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    public class Level
    {
        private LevelData levelData;
        private SpriteSheet spritesheet;
        
        private Tile[] tiles;
        private Tile[] objects;

        private bool isDay = false;

        private Player player;
        private List<Enemy> enemies;

        private bool clearedSpawns;

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

        public Tile[] Objects => objects;
        public Player MyPlayer => player;

        public List<Enemy> Enemies => enemies;

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

            this.objects = new Tile[levelData.Objects.Length];
            for (int i = 0; i < levelData.Objects.Length; i++)
            {
                Tile tile = levelData.Objects[i];

                int sx = (int)tile.IDType % spritesheet.Columns;
                int sy = (int)tile.IDType / spritesheet.Columns;

                SpriteSheet sheet = loader.LoadSpriteSheet(levelData.SpriteSheetFilePath, Vector2.Zero, new Vector2(16, 16), 0);
                sheet.XIndex = sx;
                sheet.YIndex = sy;

                objects[i] = new Tile(sheet, this, tile.Position, tile.IDType);
            }
                        

            for (int i = objects.Length - 1; i > -1; i--)
            {
                Enemy enemy = Enemy.Get(loader, this, objects[i].IDType);
                enemy.Position = objects[i].Position;

                AddEnemy(enemy);
            }
        }

        

        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        private void PlayerCollision(Enemy enemy, GameTime gameTime)
        {
            
            if(player.Bounds.Intersects(enemy.Bounds))
            {
                if (player.IsOnTopOf(enemy))
                {
                    enemy.isDead = true;
                    player.CollisionJump(300.0f);
                }
                else
                {
                    enemy.isDead = true;
                    player.Death(new Vector2(100, 100));
                }                
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
            if (!clearedSpawns)
            {
                for (int i = tiles.Length-1; i > -1; i--)
                {
                    if (tiles[i].IDType == 91)
                    {
                        tiles[i].collidable = false;
                    }
                }
                clearedSpawns = true;
            }
            DeadEnemyCheck();
            player.Update(gameTime);
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] is Cannon)
                {
                    continue;
                }
                PlayerCollision(enemies[i], gameTime);
                enemies[i].Update(gameTime);
            }
            

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
