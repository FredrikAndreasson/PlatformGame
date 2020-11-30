using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    abstract class Enemy : Character
    {
        public bool isDead = false;
        public Enemy(SpriteSheet texture, Level level, Vector2 position, Vector2 size, int health, float speed) : base(texture, level, position, size, health, speed)
        {
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
        }

        protected abstract void ChangeDirection();

        protected override void InternalUpdate(GameTime gameTime)
        {

        }
    }
}
