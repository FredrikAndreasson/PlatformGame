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

        protected Vector2 velocity;

        public Character(SpriteSheet texture, Level level, Vector2 position, Vector2 size, int health, float speed) : base(texture, level, position, size)
        {
            this.health = health;
            this.speed = speed;
        }

        protected void UpdateGravity(GameTime gameTime)
        {
            if(GetCollider(level.Tiles) == null)
            {
                velocity.Y += 9.82f * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.05f;
            }
            else
            {
                velocity.Y = 0f;
            }
        }

        public GameObject GetCollider(GameObject[] colliders)
        {
            foreach(GameObject collider in colliders)
            {
                if(Bounds.Intersects(collider.Bounds))
                {
                    return collider;
                }
            }
            return null;
        }

        public abstract void Update(GameTime gameTime);
    }
}
