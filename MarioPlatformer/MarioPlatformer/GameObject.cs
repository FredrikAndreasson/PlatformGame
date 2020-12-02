using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace MarioPlatformer
{
    public abstract class GameObject
    {
        protected SpriteSheet currentSpriteSheet;
        protected Level level;
        protected Vector2 position;
        protected Vector2 size;

        public bool collidable = true;

        public GameObject(SpriteSheet texture, Level level, Vector2 position, Vector2 size)
        {
            this.currentSpriteSheet = texture;
            this.level = level;
            this.position = position;
            this.size = size;
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Vector2 Size => size;

        public virtual Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, (int)(size.X * Game1.Scale.X), (int)(size.Y * Game1.Scale.Y));
        
        public GameObject[] GetColliders(GameObject[] colliders)
        {
            return GetColliders(colliders, Bounds);
        }

        public GameObject[] GetColliders(GameObject[] colliders, Rectangle bounds)
        {
            List<GameObject> collidingObjects = new List<GameObject>();
            foreach (GameObject collider in colliders)
            {
                if (!collider.collidable)
                {
                    continue;
                }
                if (bounds.Intersects(collider.Bounds))
                {
                    collidingObjects.Add(collider);
                }
            }
            return collidingObjects.ToArray();
        }

        public bool IsOnTopOf(GameObject collider)
        {
            if (!collider.collidable)
            {
                return false;
            }
            int yDistance = collider.Bounds.Top - Bounds.Bottom;
            int leftDistance = collider.Bounds.Left - Bounds.Right;
            int rightDistance = Bounds.Left - collider.Bounds.Right;
            if (yDistance >= -5 && yDistance <= 4 && leftDistance <= -5 && rightDistance <= -5)
            {
                return true;
            }
            return false;
        }

        public bool IsOnTopOf(Enemy collider)
        {
            if (!collider.collidable)
            {
                return false;
            }
            int yDistance = collider.Bounds.Top - Bounds.Bottom;
            if (yDistance >= -5)
            {
                return true;
            }
            return false;
        }

        public bool IsBelow(GameObject collider)
        {
            if (!collider.collidable)
            {
                return false;
            }
            int yDistance = collider.Bounds.Bottom - Bounds.Top;
            int leftDistance = Bounds.Right - collider.Bounds.Left;
            int rightDistance = collider.Bounds.Right - Bounds.Left;
            if (yDistance <= 10 && leftDistance >= 5 && rightDistance >= 5)
            {
                return true;
            }
            return false;
        }
        public bool IsBelow(PowerupBlock collider)
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



        public bool IsLeftOf(GameObject collider)
        {
            if (!collider.collidable)
            {
                return false;
            }
            int leftDistance = Bounds.Left - collider.Bounds.Right;
            int rightDistance = Bounds.Right - collider.Bounds.Right;
            int topDistance = collider.Bounds.Top - Bounds.Bottom;
            if (leftDistance <= 0 && rightDistance > 0 && topDistance <= -5)
            {
                return true;
            }
            return false;
        }

        public bool IsRightOf(GameObject collider)
        {
            if (!collider.collidable)
            {
                return false;
            }
            int rightDistance = collider.Bounds.Left - Bounds.Right;
            int leftDistance = collider.Bounds.Left - Bounds.Left;
            int topDistance = collider.Bounds.Top - Bounds.Bottom;
            if (rightDistance <= 0 && leftDistance > 0 && topDistance <= -5)
            {
                return true;
            }
            return false;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            currentSpriteSheet.Sprite.Draw(spriteBatch, position, Game1.Scale);
        }        
    }
}
