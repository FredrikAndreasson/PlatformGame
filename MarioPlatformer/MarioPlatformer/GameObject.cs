using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace MarioPlatformer
{
    abstract class GameObject
    {
        protected SpriteSheet texture;
        protected Level level;
        protected Vector2 position;
        protected Vector2 size;

        public GameObject(SpriteSheet texture, Level level, Vector2 position, Vector2 size)
        {
            this.texture = texture;
            this.level = level;
            this.position = position;
            this.size = size;
        }

        public Vector2 Position => position;

        public Vector2 Size => size;

        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, (int)(size.X * Game1.Scale.X), (int)(size.Y * Game1.Scale.Y));
        
        public GameObject[] GetColliders(GameObject[] colliders)
        {
            return GetColliders(colliders, Bounds);
        }

        public GameObject[] GetColliders(GameObject[] colliders, Rectangle bounds)
        {
            List<GameObject> collidingObjects = new List<GameObject>();
            foreach (GameObject collider in colliders)
            {
                if (bounds.Intersects(collider.Bounds))
                {
                    collidingObjects.Add(collider);
                }
            }
            return collidingObjects.ToArray();
        }

        public bool IsOnTopOf(GameObject collider)
        {
            int yDistance = collider.Bounds.Top - Bounds.Bottom;
            int leftDistance = Bounds.Right - collider.Bounds.Left;
            int rightDistance = collider.Bounds.Right - Bounds.Left;
            if (yDistance >= -10 && leftDistance >= 5 && rightDistance >= 5)
            {
                return true;
            }
            return false;
        }

        public bool IsBelow(GameObject collider)
        {
            int yDistance = collider.Bounds.Bottom - Bounds.Top;
            int leftDistance = Bounds.Right - collider.Bounds.Left;
            int rightDistance = collider.Bounds.Right - Bounds.Left;
            if (yDistance <= 10 && leftDistance >= 5 && rightDistance >= 5)
            {
                return true;
            }
            return false;
        }


        public bool IsLeftOf(GameObject collider)
        {
            int xDistance = collider.Bounds.Left - Bounds.Right;
            int topDistance = Bounds.Bottom - collider.Bounds.Top;
            if (xDistance >= -10 && topDistance >= 5)
            {
                return true;
            }
            return false;
        }

        public bool IsRightOf(GameObject collider)
        {
            int xDistance = collider.Bounds.Right - Bounds.Left;
            int topDistance = Bounds.Bottom - collider.Bounds.Top;
            if (xDistance <= 5 && topDistance >= 5)
            {
                return true;
            }
            return false;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            texture.Sprite.Draw(spriteBatch, position, Game1.Scale);
        }        
    }
}
