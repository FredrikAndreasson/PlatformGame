using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class FireBall : GameObject
    {
        int bouncesLeft;
        float speed;

        Vector2 velocity;
        bool colliding = false;

        SpriteSheet explosionSheet;

        public FireBall(SpriteSheet texture, Level level, Vector2 position, Vector2 size, Vector2 velocity, int bouncesLeft = 8, float speed = 150.0f) : base(texture, level, position, size)
        {
            this.bouncesLeft = bouncesLeft;
            this.speed = speed;

            currentSpriteSheet = texture.GetSubAt(0, 0, 7, 1);

            explosionSheet = texture.GetSubAt(7, 0, 1, 1);

            this.velocity = velocity;
        }

        public bool FireIsOnTopOf(GameObject collider)
        {
            if (!collider.collidable)
            {
                return false;
            }
            int yDistance = collider.Bounds.Top - Bounds.Bottom;
            int leftDistance = collider.Bounds.Left - Bounds.Right;
            int rightDistance = Bounds.Left - collider.Bounds.Right;
            if (yDistance >= -5 && yDistance <= 6 && leftDistance <= -5 && rightDistance <= -5)
            {
                return true;
            }
            return false;
        }
        public bool FireIsBelow(Tile collider)
        {
            if (!collider.collidable)
            {
                return false;
            }
            int yDistance = collider.Bounds.Bottom - Bounds.Top;
            int yDistfeet = collider.Bounds.Top - Bounds.Bottom;
            int leftDistance = Bounds.Right - collider.Bounds.Left;
            int rightDistance = collider.Bounds.Right - Bounds.Left;
            if (yDistance >= -10 && yDistfeet <= -7 && leftDistance >= 5 && rightDistance >= 5)
            {
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            colliding = false;
            foreach (Tile tile in level.Tiles)
            {
                if (Bounds.Intersects(tile.Bounds))
                {
                    velocity = Vector2.Zero;
                    colliding = true;
                    break;
                }
                if (FireIsOnTopOf(tile))
                {
                    velocity.Y = -2;
                    colliding = true;
                    break;
                }
                if (FireIsBelow(tile))
                {
                    velocity.Y = 2;
                    colliding = true;
                    break;
                }
                
            }
            if(!colliding)
            {
                velocity.Y += 9.82f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            position.X += velocity.X * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y += velocity.Y;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            currentSpriteSheet.Sprite.Draw(spriteBatch, position, Game1.Scale/2.0f);
        }
    }
    
}
