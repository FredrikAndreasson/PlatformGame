using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    public class PowerupBlock : GameObject
    {
        public Powerup powerup;
        private SpriteSheetLoader loader;

        public bool collided;
        private bool powerupSpawned = false;

        int collidedIndex = 2;


        public PowerupBlock(SpriteSheet texture, Level level, Vector2 position, Vector2 size, SpriteSheetLoader loader) : base(texture, level, position, size)
        {
            this.powerup = null;
            this.loader = loader;
        }

        public void Update(GameTime gameTime)
        {
            if (collided && !powerupSpawned)
            {
                int temp = Game1.random.Next(2);
                if (temp != 1)
                {
                    powerup = new Powerup(loader.LoadSpriteSheet("Powerups\\coin", Vector2.Zero, new Vector2(14, 14), 1), level, Position, new Vector2(14, 14), Powerup.PowerupType.Coin);
                }
                else
                {
                    powerup = new Powerup(loader.LoadSpriteSheet("Powerups\\flowerpowerup", Vector2.Zero, new Vector2(16, 16), 1), level, Position, new Vector2(16, 16), Powerup.PowerupType.FireFlower);

                }
                powerupSpawned = true;
                currentSpriteSheet.XIndex = collidedIndex;
            }

            if (powerup == null)
            {
                return;
            }
            powerup.Update(gameTime);
            if (powerup.isDead)
            {
                
                if (powerup.Type == Powerup.PowerupType.Coin)
                {
                    level.scoreToAdd = 100;
                }
                powerup = null;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (powerup == null)
            {
                return;
            }
            powerup.Draw(spriteBatch);
        }
    }
}
