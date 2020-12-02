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
        private HUD hud;
        
        private Tile[] tiles;
        private Tile[] objects;

        private List<PowerupBlock> powerupBlocks;

        private bool isDay = true;

        private Player player;
        private List<Enemy> enemies;

        private Vector2 playerSpawn;

        private bool lvlWon;
        private bool clearedSpawns;

        public int scoreToAdd;

        public Level(SpriteSheetLoader loader, LevelData levelData, HUD hud)
        {
            this.levelData = levelData;
            this.hud = hud;

            this.lvlWon = false;

            this.enemies = new List<Enemy>();
            this.powerupBlocks = new List<PowerupBlock>();

            playerSpawn = new Vector2(0, 250);

            Create(loader);

            SpriteSheet playerAnimationSheet = loader.LoadSpriteSheet("player");
            this.player = new Player(playerAnimationSheet, this, playerSpawn, new Vector2(16, 16), 5, 200.0f, loader);
        }

        public bool LvlWon => lvlWon;

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
                if(objects[i].IDType == 10)
                {
                    playerSpawn = objects[i].Position;
                    continue;
                }

                Enemy enemy = Enemy.Get(loader, this, objects[i].IDType);
                enemy.Position = objects[i].Position;

                AddEnemy(enemy);
            }

            for (int i = 0; i < tiles.Length; i++)
            {
                if (tiles[i].IDType == 0)
                {
                    powerupBlocks.Add(new PowerupBlock(loader.LoadSpriteSheet("blocks", Vector2.Zero, new Vector2(16, 16), 0), this, tiles[i].Position, new Vector2(16,16), loader));
                }
            }
        }

        

        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        private void ScoreToAddCheck()
        {
            hud.Score += scoreToAdd;
            scoreToAdd = 0;
        }

        private void PlayerCollision(PowerupBlock powerupBlock, GameTime gameTime)
        {
            if (player.IsBelow(powerupBlock))
            {
                if (!powerupBlock.collided)
                {
                    powerupBlock.collided = true;
                }
            }
            if (powerupBlock.powerup != null)
            {
                if (player.Bounds.Intersects(powerupBlock.powerup.Bounds) && powerupBlock.powerup.Type == Powerup.PowerupType.FireFlower)
                {
                    player.powerupTimer = 10000;
                    powerupBlock.powerup.isDead = true;
                }
            }
            
        }
        private void PlayerCollision(Enemy enemy, GameTime gameTime)
        {
            if(player.Bounds.Intersects(enemy.Bounds))
            {
                if (player.IsOnTopOf(enemy))
                {
                    enemy.isDead = true;
                    player.CollisionJump(350.0f);
                    hud.Score += 100;
                }
                else
                {
                    enemy.isDead = true;
                    player.Death(playerSpawn);
                    player.Health--;
                }                
            }
        }
        private void PlayerCollision(Spike spike, GameTime gameTime)
        {
            if (player.Bounds.Intersects(spike.Bounds))
            {
                player.Death(playerSpawn);
                player.Health--;
            }
        }

        private void FireEnemyCollision(Enemy enemy, FireBall fireBall)
        {
            if (enemy.Bounds.Intersects(fireBall.Bounds))
            {
                enemy.isDead = true;
                fireBall.exploding = true;
                hud.Score += 10;
            }
        }

        private void WinCheck()
        {
            foreach (Tile tile in tiles)
            {
                if (player.IsOnTopOf(tile) && tile.IDType == 8)
                {
                    lvlWon = true;
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

            WinCheck();
            ScoreToAddCheck();

            DeadEnemyCheck();
            player.Update(gameTime);

            if(player.Position.Y >= 500)
            {
                player.Death(playerSpawn);
                player.Health--;
            }

            for (int i = enemies.Count-1; i > -1; i--)
            {
                if (enemies[i] is Cannon)
                {
                    enemies[i].Update(gameTime);
                    continue;
                }
                else if (enemies[i] is Spike)
                {
                    enemies[i].Update(gameTime);
                    PlayerCollision((Spike)enemies[i], gameTime);
                    continue;
                }
                else
                {
                    PlayerCollision(enemies[i], gameTime);
                    enemies[i].Update(gameTime);
                }
                for (int j = player.fireBalls.Count-1; j > -1; j--)
                {
                    FireEnemyCollision(enemies[i], player.fireBalls[j]);
                }
            }
            foreach (PowerupBlock powerupBlock in powerupBlocks)
            {
                powerupBlock.Update(gameTime);
                PlayerCollision(powerupBlock, gameTime);
            }
        }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
            foreach (Tile tile in tiles)
            {
                if (tile.IDType != 5)
                {
                    tile.Draw(spriteBatch);
                }
                
            }
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (PowerupBlock powerupBlock in powerupBlocks)
            {
                powerupBlock.Draw(spriteBatch);
            }
        }
        
    }
}
