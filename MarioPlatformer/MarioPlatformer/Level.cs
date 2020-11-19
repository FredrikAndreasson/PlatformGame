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
        
        private Tile[] tiles;

        private bool isDay = false;

        public Level(SpriteSheetLoader loader, LevelData levelData)
        {
            this.levelData = levelData;
            
            Create(loader);
        }


        public bool IsDay => isDay;

        public Tile[] Tiles => tiles;


        private void Create(SpriteSheetLoader loader)
        {
            this.spritesheet = loader.LoadSpriteSheet(levelData.SpriteSheetFilePath, Vector2.Zero, new Vector2(16, 16), 0);

            this.tiles = new Tile[levelData.Size];
            for(int i = 0; i < tiles.Length;i++)
            {
                Tile tile = levelData.Tiles[i];

                int sx = (int)tile.Type % spritesheet.Columns;
                int sy = (int)tile.Type / spritesheet.Columns;

                SpriteSheet sheet = loader.LoadSpriteSheet(levelData.SpriteSheetFilePath, Vector2.Zero, new Vector2(16, 16), 0);
                sheet.XIndex = sx;
                sheet.YIndex = sy;

                tiles[i] = new Tile(sheet, this, tile.Position, tile.Type);
            }
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
        
    }
}
