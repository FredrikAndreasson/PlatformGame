using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace MarioPlatformer
{
    class GameObject
    {
        protected SpriteSheet texture;
        protected Vector2 position;

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            texture.Sprite.Draw(spriteBatch, position, Game1.Scale);
        }
    }
}
