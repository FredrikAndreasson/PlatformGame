using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{

    class Bullet
    {

        SpriteSheet texture;
        Level level;
        Vector2 position;
        Vector2 size;
        float speed;
        float lifeTime;
        Vector2 direction;
        

        public Bullet(SpriteSheet texture, Level level, Vector2 position, Vector2 size, float speed, float lifeTime, Vector2 direction)
        {
            this.texture = texture;
            this.level = level;
            this.position = position;
            this.size = size;
            this.speed = speed;
            this.lifeTime = lifeTime;
            this.direction = direction;
        }

        public Rectangle BoundingBox => new Rectangle((int) position.X, (int) position.Y, (int)(size.X* Game1.Scale.X), (int) (size.Y* Game1.Scale.Y));


        public void Update(GameTime gameTime)
        {
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects rotation = direction.X == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            texture.Sprite.Draw(spriteBatch, position, Game1.Scale, rotation);
        }
    }
}
