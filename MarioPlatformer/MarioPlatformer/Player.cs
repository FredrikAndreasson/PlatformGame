using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class Player : Character
    {

        public Player(SpriteSheet texture, Vector2 position,Vector2 size, int health, float speed) : base(texture, position,size, health, speed)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space)&& verticalSpeed == 0)
            {
                verticalSpeed += speed/10;

            }
            if (verticalSpeed > 0)
            {
                verticalSpeed -= 9.82f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            position.Y -= verticalSpeed;
        }
    }
}
