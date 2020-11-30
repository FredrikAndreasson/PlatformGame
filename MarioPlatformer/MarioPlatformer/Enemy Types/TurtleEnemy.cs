using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class TurtleEnemy : Enemy
    {
        private float lifeTime; //After getting turned into a shell there is a certain amount of time until it despawns

        private bool isShell;

        private SpriteSheet walkingSpriteSheet;
        private SpriteSheet shellSpriteSheet;

        public TurtleEnemy(SpriteSheet texture, Level level, Vector2 position, Vector2 size, int health, float speed) : base(texture, level, position, size, health, speed)
        {
            velocity.X = 1;
            walkingSpriteSheet = texture.GetSubAt(0, 0, 2, 0,size);
            shellSpriteSheet = texture.GetSubAt(2, 0, 2, 0,size);

            currentSpriteSheet = walkingSpriteSheet;
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
            facingLeft = direction.X == 1 ? false : true;

            if (isDead && !isShell)
            {
                isShell = true;
                speed = 0;
                velocity.X = 0;
                isDead = false;
                currentSpriteSheet = shellSpriteSheet;
                size.Y = 17;
            }
            if (isDead && isShell && speed == 0)
            {
                velocity.X = 1;
                speed = 300.0f;
                isDead = false;
                
            }
            ChangeDirection();
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
            if (speed != 0.0f)
            {
                currentSpriteSheet.XIndex++;
            }
                
        }
    }
}
