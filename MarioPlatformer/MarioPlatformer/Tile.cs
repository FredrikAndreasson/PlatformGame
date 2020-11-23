using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class Tile : GameObject
    {
        public static readonly int SIZE = 16;

        private int ID;

        public Tile(SpriteSheet spritesheet, Level level, Vector2 position, int ID) : base(spritesheet, level, position, new Vector2(SIZE))
        {
            this.ID = ID;
        }

        public int IDType => ID; // basically index in spriteSheet; canon would be index 90 in total AKA type 90
        
    }
}
