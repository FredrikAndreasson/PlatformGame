using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    public class Powerup : Character
    {
        public enum PowerupType
        {
            Coin,
            FireFlower,
        }
        private PowerupType type;
        

        private float deltaY = 0;
        public bool isDead;

        public Powerup(SpriteSheet texture, Level level, Vector2 position, Vector2 size, PowerupType type) : base(texture, level, position, size,0, 100.0f)
        {
            this.type = type;

            velocity.X = Game1.random.Next(2) == 1 ? -1 : 1;
        }

        public PowerupType Type => type;

        private void UpdateFlower()
        {
            if (deltaY <= currentSpriteSheet.Texture.Height * Game1.Scale.Y)
            {
                position.Y--;
                deltaY++;
            }
            if (deltaY > currentSpriteSheet.Texture.Height * Game1.Scale.Y)
            {
                position.X += velocity.X;
            }
        }

        private void UpdateCoin()
        {
            if (deltaY <= currentSpriteSheet.Texture.Height * Game1.Scale.Y)
            {
                position.Y--;
                deltaY++;
            }
            if (deltaY > currentSpriteSheet.Texture.Height * Game1.Scale.Y)
            {
                isDead = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (deltaY > currentSpriteSheet.Texture.Height * Game1.Scale.Y)
            {
                base.Update(gameTime);
                return;
            }
            InternalUpdate(gameTime);
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
        }

        protected override void InternalUpdate(GameTime gameTime)
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
    }
}
