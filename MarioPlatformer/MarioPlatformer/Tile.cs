using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class Tile : GameObject
    {
        private TileType type;

        public Tile(SpriteSheet spritesheet, Vector2 position, TileType type) : base(spritesheet, position)
        {
            this.type = type;
        }

        public TileType Type => type;
    }
}
