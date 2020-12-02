using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    public class Player : Character
    {

        private SpriteSheet idleSpriteSheet;
        private SpriteSheet runningSpriteSheet;
        private SpriteSheet jumpingSpriteSheet;

        //Fire sheets
        private SpriteSheet idleFireSpriteSheet;
        private SpriteSheet runningFireSpriteSheet;
        private SpriteSheet jumpingFireSpriteSheet;

        private float walkSpeed;
        private float runSpeed;

        public float powerupTimer;

        List<FireBall> fireBalls;
        private SpriteSheetLoader loader;

        private ButtonState lastMouseState;

        public Player(SpriteSheet texture, Level level, Vector2 position,Vector2 size, int health, float speed, SpriteSheetLoader loader) : base(texture, level, position, size, health, speed)
        {
            idleSpriteSheet = texture.GetSubAt(0, 0, 1, 1, size);
            runningSpriteSheet = texture.GetSubAt(2, 0, 3, 1, size);
            jumpingSpriteSheet = texture.GetSubAt(6, 0, 1, 1, size);

            //Fire sheets
            idleFireSpriteSheet = texture.GetSubAt(0, 1, 1, 1, size);
            runningFireSpriteSheet = texture.GetSubAt(2, 1, 3, 1, size);
            jumpingFireSpriteSheet = texture.GetSubAt(6, 1, 1, 1, size);

            this.walkSpeed = speed;
            this.runSpeed = speed * 2;

            this.msPerFrame = 50;

            this.currentSpriteSheet = idleSpriteSheet;

            this.fireBalls = new List<FireBall>();
            this.loader = loader;
        }


        public void Death(Vector2 spawnPoint)
        {
            position = spawnPoint;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (FireBall fireBall in fireBalls)
            {
                fireBall.Draw(spriteBatch);
            }
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
            if (powerupTimer > 0)
            {
                if (!doneJumping)
                {
                    this.currentSpriteSheet = jumpingFireSpriteSheet;
                }
                else if (running)
                {
                    this.currentSpriteSheet = runningFireSpriteSheet;
                    this.msPerFrame = 25;

                    this.currentSpriteSheet.XIndex++;
                }
                else if (walking)
                {
                    this.currentSpriteSheet = runningFireSpriteSheet;
                    this.msPerFrame = 50;
                    this.currentSpriteSheet.XIndex++;
                }
                else
                {
                    this.currentSpriteSheet = idleFireSpriteSheet;
                }
                
            }
            else
            {
                if (!doneJumping)
                {
                    this.currentSpriteSheet = jumpingSpriteSheet;
                }
                else if (running)
                {
                    this.currentSpriteSheet = runningSpriteSheet;
                    this.msPerFrame = 25;

                    this.currentSpriteSheet.XIndex++;
                }
                else if (walking)
                {
                    this.currentSpriteSheet = runningSpriteSheet;
                    this.msPerFrame = 50;
                    this.currentSpriteSheet.XIndex++;
                }
                else
                {
                    this.currentSpriteSheet = idleSpriteSheet;
                }
            }

            
        }

        protected override void InternalUpdate(GameTime gameTime)
        {
            direction = Vector2.Zero;            
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                direction.X = -1;
                facingLeft = true;
                walking = true;
                
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                direction.X = 1;
                facingLeft = false;
                walking = true;
            }    

            if (!Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A))
            {
                walking = false;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.LeftShift) && walking)
            {
                running = true;
                speed = runSpeed;
            }
            else
            {
                running = false;
                speed = walkSpeed;
            }

            bool canJump = false;
            GameObject[] colliders = GetColliders(level.Tiles);
            foreach (GameObject collider in colliders)
            {
                if (IsOnTopOf(collider) && !jumping)
                {
                    canJump = true;
                    break;
                }
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && powerupTimer > 0 && lastMouseState != ButtonState.Pressed)
            {
                fireBalls.Add(new FireBall(loader.LoadSpriteSheet("Powerups\\fire", Vector2.Zero, new Vector2(16, 16), 0), level, Position, new Vector2(8, 8), facingLeft ? new Vector2(-1,0) : new Vector2(1,0)));
            }


            if ((Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.W)))
            {
                if(canJump)
                {
                    Jump(400.0f);
                }
            }
            else
            {
                jumping = false;
            }
            foreach (FireBall fireBall in fireBalls)
            {
                fireBall.Update(gameTime);
            }

            lastMouseState = Mouse.GetState().LeftButton;
            powerupTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

    }
}
