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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            texture.Sprite.Draw(spriteBatch, position, Game1.Scale);
        }

        public GameObject(SpriteSheet texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }
    }
}
