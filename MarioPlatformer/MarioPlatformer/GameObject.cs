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

        public bool PixelCollision(GameObject collider, Rectangle bounds)
        {
            Texture2D texA = currentSpriteSheet.Sprite.Texture;
            Texture2D texB = collider.currentSpriteSheet.Sprite.Texture;

            Color[] colorsA = new Color[texA.Width * texA.Height];
            texA.GetData(colorsA);

            Color[] colorsB = new Color[texB.Width * texB.Height];
            texB.GetData(colorsB);

            int top = Math.Max(bounds.Top, collider.Bounds.Top);
            int bottom = Math.Min(bounds.Bottom, collider.Bounds.Bottom);
            int left = Math.Max(bounds.Left, collider.Bounds.Left);
            int right = Math.Min(bounds.Right, collider.Bounds.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colorA = colorsA[((x - bounds.Left) + (y - bounds.Top) * bounds.Width) / (int)Game1.Scale.X];
                    Color colorB = colorsB[((x - collider.Bounds.Left) + (y - collider.Bounds.Top) * collider.Bounds.Width) / (int)Game1.Scale.X];

                    if (colorA.A != 0 && colorB.A != 0)
                    {                        
                        return true;
                    }
                }
            }

            return false;
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
            if (yDistance >= -5 && leftDistance <= -5 && rightDistance <= -5)
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
            if (yDistance >= -10)
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
            int bottomDistance = collider.Bounds.Bottom - Bounds.Top;
            if (leftDistance <= 0 && rightDistance > 0 && topDistance <= -5 && bottomDistance >= 0)
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
            int bottomDistance = collider.Bounds.Bottom - Bounds.Top;
            if (rightDistance <= 0 && leftDistance > 0 && topDistance <= -5 && bottomDistance >= 0)
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
