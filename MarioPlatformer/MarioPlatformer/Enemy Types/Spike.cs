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
            throw new NotImplementedException();
        }

        protected override void InternalUpdate(GameTime gameTime)
        {
            base.InternalUpdate(gameTime);
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
            base.InternalUpdateAnimation(gameTime);
        }
    }
}
