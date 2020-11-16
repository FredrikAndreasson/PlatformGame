using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PacMan
{
    public class Sprite
    {
        private Texture2D texture;
        
        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            this.SpriteSheetPosition = Vector2.Zero;
            this.SpriteSize = new Vector2(texture.Width, texture.Height);
        }

        public Sprite(Texture2D texture, Vector2 start, Vector2 spriteSize)
        {
            this.texture = texture;
            this.SpriteSheetPosition = start;
            this.SpriteSize = spriteSize;
        }

        public Vector2 SpriteSheetPosition
        {
            get;
            set;
        }

        public Vector2 SpriteSize
        {
            get;
            set;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 scale, SpriteEffects spriteEffects = SpriteEffects.None, Color? color = null)
        {
            if (color == null)
            {
                color = Color.White;
            }

            Rectangle source = new Rectangle((int)SpriteSheetPosition.X, (int)SpriteSheetPosition.Y, (int)SpriteSize.X, (int)SpriteSize.Y);
            spriteBatch.Draw(texture, position, source, color.Value, 0.0f, Vector2.Zero, scale, spriteEffects, 1.0f);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 scale, Vector2 origin, float rotation = 0, SpriteEffects spriteEffects = SpriteEffects.None, Color? color = null)
        {
            if (color == null)
            {
                color = Color.White;
            }
            Rectangle source = new Rectangle((int)SpriteSheetPosition.X, (int)SpriteSheetPosition.Y, (int)SpriteSize.X, (int)SpriteSize.Y);
            origin = new Vector2(source.Width, source.Height) * origin;
            spriteBatch.Draw(texture, position, source, color.Value, rotation, origin, scale, spriteEffects, 1.0f);
        }


    }
}
