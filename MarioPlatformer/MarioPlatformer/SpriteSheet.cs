using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PacMan
{
    public class SpriteSheet
    {
        private Vector2 start;
        private Vector2 spriteSize;

        private Sprite internalSprite;
        private int xIndex,  yIndex;
        private int columns, rows;
        private int offset;

        public SpriteSheet(Texture2D texture)
        {
            this.Texture = texture;
            this.start = Vector2.Zero;
            this.spriteSize = new Vector2(texture.Width, texture.Height);
            this.internalSprite = new Sprite(texture, start, spriteSize);
        }

        public SpriteSheet(Texture2D texture, Vector2 start, Vector2 dimensions, Vector2 spriteSize, int offset = 0)
        {
            this.Texture = texture;
            this.start = start;
            this.spriteSize = spriteSize;

            this.internalSprite = new Sprite(texture, start, spriteSize);

            this.columns = (int)(dimensions.X / spriteSize.X);
            this.rows = (int)(dimensions.Y / spriteSize.Y);
            this.offset = offset;
        }

        public Texture2D Texture
        {
            get;
            private set;
        }

        public Vector2 Start
        {
            get
            {
                return this.start;
            }
        }

        public int XIndex
        {
            get { return xIndex; }
            set { this.xIndex = value % columns; }
        }

        public int YIndex
        {
            get { return yIndex; }
            set { this.yIndex = value % rows; }
        }

        public int Columns
        {
            get { return columns; }
        }

        public int Rows
        {
            get { return rows; }
        }

        public Sprite GetAt(int x, int y)
        {
            Vector2 position = new Vector2(start.X + (spriteSize.X * x) + (offset * x), start.Y + (spriteSize.Y * y) + (offset * y));
            return new Sprite(Texture, position, spriteSize);
        }

        public Sprite Sprite
        {
            get
            {
                internalSprite.SpriteSheetPosition = new Vector2(start.X + (spriteSize.X * XIndex) + (offset * XIndex), start.Y + (spriteSize.Y * YIndex) + (offset * YIndex));
                return internalSprite;
            }
        }
    }
}
