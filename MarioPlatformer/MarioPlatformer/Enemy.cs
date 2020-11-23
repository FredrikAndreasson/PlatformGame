using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class Enemy : Character
    {
        Vector2 direction;

        private float msSinceLastFrame;
        private float msPerFrame;

        public Enemy(SpriteSheet texture, Level level, Vector2 position, Vector2 size, int health, float speed) : base(texture, level, position, size, health, speed)
        {
            direction.X = Game1.random.Next(2);
            direction.X = direction.X == 1 ? -1 : 1;
        }

        public void UpdateAnimation(GameTime gameTime)
        {
            if (msSinceLastFrame >= msPerFrame)
            {
                texture.XIndex++;
                msSinceLastFrame = 0;
            }
            else
            {
                msSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public override void Update(GameTime gameTime)
        {
            UpdateGravity(gameTime);
            velocity = direction;

            UpdateVelocity(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

        }
    }
}
