using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class Spike : Enemy
    {
        public Spike(SpriteSheet texture, Level level, Vector2 position, Vector2 size, int health, float speed) : base(texture, level, position, size, health, speed)
        {
        }

        protected override void ChangeDirection()
        {
        }

        protected override void InternalUpdate(GameTime gameTime)
        {
            
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
            currentSpriteSheet.XIndex++;
        }
    }
}
