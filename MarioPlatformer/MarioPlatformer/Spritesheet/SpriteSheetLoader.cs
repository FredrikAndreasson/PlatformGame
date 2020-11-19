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
        private GraphicsDevice graphicsDevice;

        public SpriteSheetLoader(ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.content = content;
            this.graphicsDevice = graphicsDevice;
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

        public Texture2D CreateRectangleTexture(int width, int height, Color lineColor)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);

            Color[] colors = new Color[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    {
                        colors[index] = lineColor;
                    }
                    else
                    {
                        colors[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colors);


            return texture;
        }

        public Texture2D CreateFilledTexture(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);

            Color[] colors = new Color[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;
                    colors[index] = color;
                }
            }

            texture.SetData(colors);


            return texture;
        }
    }
}
