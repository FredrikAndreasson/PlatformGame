using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class PowerupBlock : GameObject
    {
        private Powerup powerup;
        private SpriteSheetLoader loader;

        public bool collided;
        private bool powerupSpawned = false;

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
                if (temp == 1)
                {
                    powerup = new Powerup(loader.LoadSpriteSheet("Powerups\\coin", Vector2.Zero, new Vector2(14, 14), 1), level, Position, new Vector2(14, 14), Powerup.PowerupType.Coin);
                }
                else
                {
                    powerup = new Powerup(loader.LoadSpriteSheet("Powerups\\flowerpowerup", Vector2.Zero, new Vector2(16, 16), 1), level, Position, new Vector2(16, 16), Powerup.PowerupType.FireFlower);

                }
                powerupSpawned = true;
            }

            if (powerup.IsDead)
            {
                powerup = null;
                if (powerup.Type == Powerup.PowerupType.FireFlower)
                {
                    //Give the player the fireflower powerup...
                }
                else
                {
                    //Increase player score by 1...
                }
            }
        }
    }
}
