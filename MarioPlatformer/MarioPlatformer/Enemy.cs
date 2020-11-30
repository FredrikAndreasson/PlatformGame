using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    public abstract class Enemy : Character
    {
        public bool isDead = false;
        
        public Enemy(SpriteSheet texture, Level level, Vector2 position, Vector2 size, int health, float speed) : base(texture, level, position, size, health, speed)
        {

        }

        

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {

        }

        protected abstract void ChangeDirection();

        protected override void InternalUpdate(GameTime gameTime)
        {

        }
        
        public static Enemy Get(SpriteSheetLoader loader, Level level, int id)
        {
            return id switch
            {
                90 => new Cannon(loader.LoadSpriteSheet("Obstacles\\canon", Vector2.Zero, new Vector2(16, 16), 0), level, Vector2.Zero, new Vector2(16, 16), loader.LoadSpriteSheet("Obstacles\\bullet", Vector2.Zero, new Vector2(16, 13), 0)),
                91 => new PatrollingEnemy(loader.LoadSpriteSheet("Enemies\\DKenemy", Vector2.Zero, new Vector2(15, 20), 1), level, Vector2.Zero, new Vector2(15, 20), 1, 70.0f),
                92 => new TurtleEnemy(loader.LoadSpriteSheet("Enemies\\TurtleEnemy", Vector2.Zero, new Vector2(16, 24), 1), level, Vector2.Zero, new Vector2(17, 24), 1, 70.0f),
                _ => null,
            };
        }
    }
}
