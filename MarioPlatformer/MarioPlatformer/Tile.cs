using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class Tile : GameObject
    {
        public static readonly int SIZE = 16;

        private TileType type;

        public Tile(SpriteSheet spritesheet, Vector2 position, TileType type) : base(spritesheet, position, new Vector2(SIZE))
        {
            this.type = type;
        }

        public TileType Type => type;
        
    }
}
