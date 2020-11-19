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

        protected float verticalSpeed;

        public Character(SpriteSheet texture, Vector2 position,Vector2 size, int health, float speed) : base(texture, position, size)
        {
            this.health = health;
            this.speed = speed;
        }

        protected void HandleGravity()
        {

        }

        public abstract void Update(GameTime gameTime);
    }
}
