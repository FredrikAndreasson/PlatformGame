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

            return new Vector2(1, 0);
        }

        protected override void InternalUpdate(GameTime gameTime)
        {

            velocity = ChangeDirection(velocity);
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
        }
    }
}
