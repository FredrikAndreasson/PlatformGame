using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    abstract class Character : GameObject
    {
        protected int health;
        protected float speed;
        
        protected Vector2 velocity;
        protected Vector2 direction;

        protected GameObject[] colliders;

        public Character(SpriteSheet texture, Level level, Vector2 position, Vector2 size, int health, float speed) : base(texture, level, position, size)
        {
            this.health = health;
            this.speed = speed;
        }
        
        public Rectangle GetBounds(GameTime gameTime)
        {
            Rectangle r = Bounds;
            Vector2 vel = GetTotalVelocity(gameTime);

            
            if(vel.X < 0)
            {
                r.X += (int)vel.X;
                r.Width -= (int)vel.X;
            }
            else
            {
                r.Width += (int)vel.X;
            }
            if(vel.Y < 0)
            {
                r.Y += (int)vel.Y;
                r.Height -= (int)vel.Y;
            }
            else
            {
                r.Height += (int)vel.Y;
            }
            return r;
        }

        public Vector2 GetTotalVelocity(GameTime gameTime)
        {
            return speed * (velocity + direction) * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        protected void UpdateGravity(GameTime gameTime)
        {
            velocity.Y += 9.82f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        protected void UpdateCollision(GameTime gameTime)
        {
            GameObject[] colliders = GetColliders(level.Tiles, GetBounds(gameTime));
            foreach (GameObject collider in colliders)
            {
                if (IsOnTopOf(collider))
                {
                    position.Y = collider.Bounds.Top - Bounds.Height + 1;
                    velocity.Y = 0;
                }
                else if(IsBelow(collider))
                {
                    position.Y = collider.Bounds.Bottom;
                    velocity.Y = 0;
                }
                else if(IsLeftOf(collider))
                {
                    position.X = collider.Bounds.Left - Bounds.Width;
                }
                else if (IsRightOf(collider))
                {
                    position.X = collider.Bounds.Right;
                }
            }
        }

        protected void UpdateVelocity(GameTime gameTime)
        {   
            position += speed * (velocity + direction) * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        
        protected void Jump()
        {
            GameObject[] colliders = GetColliders(level.Tiles);
            foreach(GameObject collider in colliders)
            {
                if(IsOnTopOf(collider))
                {
                    velocity.Y -= 4.0f;
                    break;
                }
            }
        }

        

        public abstract void Update(GameTime gameTime);
    }
}
