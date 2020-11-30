using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    abstract class Enemy : Character
    {
        public bool isDead = false;

        private static Dictionary<int, Enemy> enemies;

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

        public static void Load(SpriteSheetLoader loader, Level level)
        {
            enemies[90] = new ShootingObstacle(loader.LoadSpriteSheet("Obstacles\\canon", Vector2.Zero, new Vector2(16, 16), 0), level, Vector2.Zero, new Vector2(16 * Game1.Scale.X, 16 * Game1.Scale.Y), new Vector2(-1, 0), loader.LoadSpriteSheet("Obstacles\\bullet", Vector2.Zero, new Vector2(16, 13), 0));
        }

        public static Enemy Get(int id)
        {
            if(enemies.ContainsKey(id))
            {
                return enemies[id];
            }

            return null;
        }
    }
}
