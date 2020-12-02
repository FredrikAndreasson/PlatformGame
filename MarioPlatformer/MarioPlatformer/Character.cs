using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    public abstract class Character : GameObject
    {
        protected int health;
        protected float speed;
        
        protected Vector2 velocity;
        protected Vector2 direction;

        private float jumpPower;
        protected bool jumping;
        protected bool collisionJump;
        protected bool doneJumping;
        protected bool walking;
        protected bool running;
        protected double jumpTimer;

        protected float msSinceLastFrame;
        protected float msPerFrame = 200;
        protected bool facingLeft;

        public Character(SpriteSheet texture, Level level, Vector2 position, Vector2 size, int health, float speed) : base(texture, level, position, size)
        {
            this.health = health;
            this.speed = speed;
        }

        public int Health
        {
            get => health;
            set => health = value;
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
                velocity.Y += 9.82f * 2;
            }
        }


        protected void UpdateVelocity(GameTime gameTime)
        {
            if(jumping || collisionJump)
            {
                velocity.Y -= jumpPower;
                jumpPower = 0.0f;
            }

            jumpTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            
            
            if (jumpTimer <= 0)
            {
                jumpTimer = 0.0f;
                jumping = false;
                collisionJump = false;
            }

            Vector2 totalVelocity = (velocity + (direction * speed)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                    collisionJump = false;
                }
                else if (IsBelow(collider))
                {
                    newPosition.Y = collider.Bounds.Bottom;
                    velocity.Y = 0;
                }
                else if (IsLeftOf(collider))
                {
                    newPosition.X = collider.Bounds.Right;
                }
                else if (IsRightOf(collider))
                {
                    newPosition.X = collider.Bounds.Left - Bounds.Width;
                }
            }

            position = newPosition;
        }
        
        public void Jump(float power)
        {
            jumpPower = power;
            jumping = true;
            jumpTimer = 250.0f;
            velocity.Y = 0;
            doneJumping = false;
        }

        public void CollisionJump(float power = 400.0f)
        {
            Jump(power);
            collisionJump = true;
        }

        protected abstract void InternalUpdate(GameTime gameTime);
        

        public virtual void Update(GameTime gameTime)
        {
            direction = Vector2.Zero;
            UpdateGravity(gameTime);

            InternalUpdate(gameTime);
            UpdateAnimation(gameTime);

            UpdateVelocity(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effect = facingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            currentSpriteSheet.Sprite.Draw(spriteBatch, position, Game1.Scale, effect);
        }
    }
}
