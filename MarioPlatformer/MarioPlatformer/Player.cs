using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class Player : Character
    {

        public Player(SpriteSheet texture, Vector2 position, int health, float speed) : base(texture, position, health, speed)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                System.Diagnostics.Debug.WriteLine(position);
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
