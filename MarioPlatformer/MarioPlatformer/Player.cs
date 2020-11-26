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
            direction = Vector2.Zero;
            UpdateGravity(gameTime);
            
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                direction.X = -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                direction.X = 1;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Jump(gameTime);
            }
            else
            {
                jumping = false;
            }

               

            UpdateVelocity(gameTime);
            UpdateCollision(gameTime);
        }

    }
}
