using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        private float jumpPower;
        protected bool jumping;
        protected bool doneJumping;
        protected bool walking;
        protected bool running;
        protected double jumpTimer;

        private float msSinceLastFrame;
        protected float msPerFrame = 200;
        protected bool facingLeft;

        public Character(SpriteSheet texture, Level level, Vector2 position, Vector2 size, int health, float speed) : base(texture, level, position, size)
        {
            this.health = health;
            this.speed = speed;
        }
        

        public Vector2 GetTotalVelocity(GameTime gameTime)
        {
            return speed * (velocity + direction) * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        protected abstract void InternalUpdateAnimation(GameTime gameTime);

        protected void UpdateAnimation(GameTime gameTime)
        {
            if (msSinceLastFrame >= msPerFrame)
            {
                InternalUpdateAnimation(gameTime);
                msSinceLastFrame = 0;
            }
            else
            {
                msSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        protected void UpdateGravity(GameTime gameTime)
        {
            if(!jumping)
            {
                velocity.Y += 9.82f * 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }


        protected void UpdateVelocity(GameTime gameTime)
        {
            if(jumping)
            {
                velocity.Y -= jumpPower;
                jumpPower *= (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            jumpTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            
            
            if (jumpTimer <= 0)
            {
                jumpTimer = 0.0f;
                jumping = false;
            }

            Vector2 totalVelocity = velocity + (direction * speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 newPosition = position + totalVelocity;

            Rectangle hitbox = new Rectangle((int)newPosition.X, (int)newPosition.Y, Bounds.Width, Bounds.Height);
            if (totalVelocity.X < 0)
            {
                hitbox.X += (int)totalVelocity.X;
                hitbox.Width -= (int)totalVelocity.X;
            }
            else
            {
                hitbox.Width += (int)totalVelocity.X;
            }
            if (totalVelocity.Y < 0)
            {
                hitbox.Y += (int)totalVelocity.Y;
                hitbox.Height -= (int)totalVelocity.Y;
            }
            else
            {
                hitbox.Height += (int)totalVelocity.Y;
            }

            GameObject[] colliders = GetColliders(level.Tiles, hitbox);
            foreach (GameObject collider in colliders)
            {
                if (IsOnTopOf(collider))
                {
                    newPosition.Y = collider.Bounds.Top - Bounds.Height + 1;
                    velocity.Y = 0;
                    doneJumping = true;
                    //System.Diagnostics.Debug.WriteLine("TopOf");
                }
                else if (IsBelow(collider))
                {
                    newPosition.Y = collider.Bounds.Bottom;
                    velocity.Y = 0;
                    System.Diagnostics.Debug.WriteLine("Below");
                }
                else if (IsLeftOf(collider))
                {
                    newPosition.X = collider.Bounds.Right;
                    System.Diagnostics.Debug.WriteLine("Left");
                }
                else if (IsRightOf(collider))
                {
                    newPosition.X = collider.Bounds.Left - Bounds.Width;
                    System.Diagnostics.Debug.WriteLine("Right");
                }
            }

            position = newPosition;
        }
        
        protected void Jump(GameTime gameTime)
        {
            GameObject[] colliders = GetColliders(level.Tiles);
            foreach(GameObject collider in colliders)
            {
                if(IsOnTopOf(collider) && !jumping)
                {
                    jumpPower = 7.0f;
                    jumping = true;
                    jumpTimer = 250.0f;
                    doneJumping = false;
                    break;
                }
            }
        }

        protected abstract void InternalUpdate(GameTime gameTime);
        

        public virtual void Update(GameTime gameTime)
        {
            direction = Vector2.Zero;
            UpdateGravity(gameTime);

            InternalUpdate(gameTime);
            UpdateAnimation(gameTime);

            UpdateVelocity(gameTime);
            //UpdateCollision(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effect = facingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            currentSpriteSheet.Sprite.Draw(spriteBatch, position, Game1.Scale, effect);
        }
    }
}
