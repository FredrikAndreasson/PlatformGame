using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class Powerup : GameObject
    {
        public enum PowerupType
        {
            Coin,
            FireFlower,
        }
        private PowerupType type;
        

        private float deltaY = 0;
        private Vector2 direction;
        private bool isDead;

        public Powerup(SpriteSheet texture, Level level, Vector2 position, Vector2 size, PowerupType type) : base(texture, level, position, size)
        {
            this.type = type;

            direction.X = Game1.random.Next(2) == 1 ? -1 : 1;
        }

        public bool IsDead => isDead;
        public PowerupType Type => type;

        public void Update(GameTime gameTime)
        {
            if (type == PowerupType.FireFlower)
            {
                UpdateFlower();
            }
            else
            {
                UpdateCoin();
            }
        }

        private void UpdateFlower()
        {
            if (deltaY <= currentSpriteSheet.Texture.Height)
            {
                position.Y--;
                deltaY++;
            }
            if (deltaY > currentSpriteSheet.Texture.Height)
            {
                position += direction;
            }
        }

        private void UpdateCoin()
        {
            if (deltaY <= currentSpriteSheet.Texture.Height)
            {
                position.Y--;
                deltaY++;
            }
            if (deltaY > currentSpriteSheet.Texture.Height)
            {
                isDead = true;
            }
        }
    }
}
