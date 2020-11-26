using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class Enemy : Character
    {      

        public Enemy(SpriteSheet texture, Level level, Vector2 position, Vector2 size, int health, float speed) : base(texture, level, position, size, health, speed)
        {
            velocity.X = Game1.random.Next(2);
            velocity.X = velocity.X == 1 ? -1 : 1;
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
            currentSpriteSheet.XIndex++;
        }

        public Vector2 ChangeDirection(Vector2 velocity)
        {

            return Vector2.Zero;
        }

        protected override void InternalUpdate(GameTime gameTime)
        {
<<<<<<< HEAD
            UpdateGravity(gameTime);
            UpdateCollision(gameTime);
            UpdateVelocity(gameTime);
=======

>>>>>>> e35527c7d9795a093fb54e3fbf4505f73bfbe7a9
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

        }
    }
}
