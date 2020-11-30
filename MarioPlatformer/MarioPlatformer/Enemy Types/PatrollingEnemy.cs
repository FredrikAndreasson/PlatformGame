using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class PatrollingEnemy : Enemy
    {
        public PatrollingEnemy(SpriteSheet texture, Level level, Vector2 position, Vector2 size, int health, float speed) : base(texture, level, position, size, health, speed)
        {
            velocity.X = 1;
            currentSpriteSheet = texture.GetSubAt(0,0,3,0);
        }

        protected override void ChangeDirection()
        {
            foreach (Tile tile in level.Tiles)
            {
                if (IsRightOf(tile))
                {
                    velocity.X = -1;
                }
                else if (IsLeftOf(tile))
                {
                    velocity.X = 1;
                }
            }
        }

        protected override void InternalUpdate(GameTime gameTime)
        {
            direction.X = velocity.X > 0 ? 1 : -1;
            facingLeft = direction.X == 1 ? true : false;

            ChangeDirection();
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
            if (msPerFrame < msSinceLastFrame)
            {
                currentSpriteSheet.XIndex++;
                msSinceLastFrame = 0;
            }
            else
            {
                msSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }
    }
}
