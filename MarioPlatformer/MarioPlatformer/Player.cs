using Microsoft.Xna.Framework;
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

        private float walkSpeed;
        private float runSpeed;

        
        public Player(SpriteSheet texture, Level level, Vector2 position,Vector2 size, int health, float speed) : base(texture, level, position, size, health, speed)
        {
            idleSpriteSheet = texture.GetSubAt(0, 0, 1, 1, size);
            runningSpriteSheet = texture.GetSubAt(2, 0, 3, 1, size);
            jumpingSpriteSheet = texture.GetSubAt(6, 0, 1, 1, size);

            this.walkSpeed = speed;
            this.runSpeed = speed * 2;

            this.msPerFrame = 50;

            this.currentSpriteSheet = idleSpriteSheet;
        }


        public void Death(Vector2 spawnPoint)
        {
            position = spawnPoint;
        }


        protected override void InternalUpdateAnimation(GameTime gameTime)
        {

            if(!doneJumping)
            {
                this.currentSpriteSheet = jumpingSpriteSheet;
            }
            else if(running)
            {
                this.currentSpriteSheet = runningSpriteSheet;
                this.msPerFrame = 25;

                this.currentSpriteSheet.XIndex++;
            }
            else if(walking)
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
        }

    }
}
