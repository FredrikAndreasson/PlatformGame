using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class Enemy : Character
    {
        private float msSinceLastFrame;
        private float msPerFrame = 200;

        public Enemy(SpriteSheet texture, Level level, Vector2 position, Vector2 size, int health, float speed) : base(texture, level, position, size, health, speed)
        {
            velocity.X = Game1.random.Next(2);
            velocity.X = velocity.X == 1 ? -1 : 1;
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

        public Vector2 ChangeDirection(Vector2 velocity)
        {

            return Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateGravity(gameTime);
            UpdateCollision(gameTime);
            UpdateVelocity(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

        }
    }
}
