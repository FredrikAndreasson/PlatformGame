using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class SpriteSheetLoader
    {
        private Dictionary<string, Texture2D> textures;

        private ContentManager content;

        public SpriteSheetLoader(ContentManager content)
        {
            this.content = content;
            this.textures = new Dictionary<string, Texture2D>();
        }

        public Texture2D LoadTexture(string name)
        {
            if (textures.ContainsKey(name))
            {
                return textures[name];
            }

            Texture2D texture = content.Load<Texture2D>(name);
            textures.Add(name, texture);
            return texture;
        }

        public SpriteSheet LoadSpriteSheet(string name, Vector2 start, Vector2 dimensions, Vector2 spriteSize, int offset = 0)
        {
            Texture2D texture = LoadTexture(name);
            return new SpriteSheet(texture, start, dimensions, spriteSize, offset);
        }
    }
}
