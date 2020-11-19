using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class Player : Character
    {

        public Player(SpriteSheet texture, Level level, Vector2 position,Vector2 size, int health, float speed) : base(texture, level, position, size, health, speed)
        {

        }

        public override void Update(GameTime gameTime)
        {

            UpdateGravity(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X = -1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                velocity.X = 1;
            }
            else
            {
                velocity.X = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                velocity.Y = -1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                velocity.Y = 1;
            }

            
            UpdateVelocity(gameTime);
        }

    }
}
