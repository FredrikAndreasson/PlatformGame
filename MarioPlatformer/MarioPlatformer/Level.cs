using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class Level
    {

        private LevelData levelData;
        private SpriteSheet spritesheet;

        private Sprite[] sprites;

        public Level(SpriteSheetLoader loader, LevelData levelData)
        {
            this.levelData = levelData;

            Create(loader);
        }

        private void Create(SpriteSheetLoader loader)
        {
            this.spritesheet = loader.LoadSpriteSheet(levelData.SpriteSheetFilePath, Vector2.Zero, new Vector2(80,80), new Vector2(16, 16), 0);

            this.sprites = new Sprite[levelData.Width * levelData.Height];
            for(int i = 0; i < sprites.Length;i++)
            {
                int x = i % levelData.Width;
                int y = i / levelData.Width;
                int value = (int) levelData.Tiles[x, y];

                int sx = value % spritesheet.Columns;
                int sy = value / spritesheet.Columns;               
                

                sprites[i] = spritesheet.GetAt(sx, sy);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spritesheet.Sprite.Draw(spriteBatch, Vector2.Zero, Vector2.One);

            int index = 0;
            foreach(Sprite sprite in sprites)
            {
                sprite.Draw(spriteBatch, new Vector2(index * sprite.SpriteSize.X, 0), Vector2.One);
                index++;
            }
        }

    }
}
