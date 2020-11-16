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

        public Character(SpriteSheet texture, Vector2 position, int health, float speed) : base(texture, position)
        {
            this.health = health;
            this.speed = speed;
        }

        public abstract void Update(GameTime gameTime);
    }
}
