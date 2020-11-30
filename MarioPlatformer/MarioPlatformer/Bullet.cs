using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{

    class Bullet : Enemy
    {
        float lifeTime;

        public Bullet(SpriteSheet texture, Level level, Vector2 position, Vector2 size, float speed, float lifeTime, Vector2 direction) : base(texture, level, position, size, 1, speed)
        {
            this.lifeTime = lifeTime;
            this.direction = direction;
        }

        public override void Update(GameTime gameTime)
        {
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            TerrainCollision();
        }

        private void TerrainCollision()
        {
            foreach (Tile tile in level.Tiles)
            {
                if (Bounds.Intersects(tile.Bounds)&&tile.IDType != 90)
                {
                    isDead = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects rotation = direction.X == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            currentSpriteSheet.Sprite.Draw(spriteBatch, position, Game1.Scale, rotation);
        }

        protected override void ChangeDirection()
        {
        }
    }
}
