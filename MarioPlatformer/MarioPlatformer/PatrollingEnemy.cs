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
            velocity.X = -1;
        }

        protected override Vector2 ChangeDirection(Vector2 velocity)
        {
            foreach (Tile tile in level.Tiles)
            {
                if (IsRightOf(tile))
                {
                    return new Vector2(1, 0);
                }
                else if (IsLeftOf(tile))
                {
                    return new Vector2(-1, 0);
                }
            }

            return direction;
        }

        protected override void InternalUpdate(GameTime gameTime)
        {
            //direction = ChangeDirection(direction);
            //velocity *= direction;
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
        }
    }
}
