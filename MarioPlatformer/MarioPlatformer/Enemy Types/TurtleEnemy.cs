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

        private float height;

        public TurtleEnemy(SpriteSheet texture, Level level, Vector2 position, Vector2 size, int health, float speed) : base(texture, level, position, size, health, speed)
        {
            velocity.X = 1;
            walkingSpriteSheet = texture.GetSubAt(0, 0, 2, 0,size);
            shellSpriteSheet = texture.GetSubAt(2, 0, 2, 0, new Vector2(17,14));

            height = 24.0f;

            currentSpriteSheet = walkingSpriteSheet;
        }

        public override Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, (int)(size.X * Game1.Scale.X), (int)(height * Game1.Scale.Y));


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
            if (isDead && !isShell)
            {
                isShell = true;
                isDead = false;
            }

            if (isShell)
            {
                currentSpriteSheet = shellSpriteSheet;
                speed = 300.0f;
                height = 14.0f;
            }
            ChangeDirection();
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
            currentSpriteSheet.XIndex++;
        }
    }
}
