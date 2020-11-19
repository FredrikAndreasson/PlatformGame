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
        protected Vector2 position;
        protected Vector2 size;

        public GameObject(SpriteSheet texture, Vector2 position, Vector2 size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
        }

        public Vector2 Position => position;

        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, (int)(size.X * Game1.Scale.X), (int)(size.Y * Game1.Scale.Y));

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            texture.Sprite.Draw(spriteBatch, position, Game1.Scale);
        }        
    }
}
