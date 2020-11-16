using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    abstract class Enemy : Character
    {

        public Enemy(SpriteSheet texture, Vector2 position, int health, float speed) : base(texture, position, health, speed)
        {
            
        }

        public override abstract void Update(GameTime gameTime);
    }
}
