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

        private static Dictionary<int, Enemy> enemies = new Dictionary<int, Enemy>();

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
            enemies.Clear();
            enemies[90] = new Cannon(loader.LoadSpriteSheet("Obstacles\\canon", Vector2.Zero, new Vector2(16, 16), 0), level, Vector2.Zero, new Vector2(16, 16), loader.LoadSpriteSheet("Obstacles\\bullet", Vector2.Zero, new Vector2(16, 13), 0));
            enemies[91] = new PatrollingEnemy(loader.LoadSpriteSheet("Enemies\\DKenemy", Vector2.Zero, new Vector2(15, 20), 1), level, Vector2.Zero, new Vector2(15, 20), 1, 70.0f);
            enemies[92] = new TurtleEnemy(loader.LoadSpriteSheet("Enemies\\TurtleEnemy", Vector2.Zero, new Vector2(16, 24), 1), level, Vector2.Zero, new Vector2(17, 24), 1, 70.0f);
        }

        public static Enemy Get(int id)
        {
            if(enemies.ContainsKey(id))
            {
                return (Enemy)enemies[id].MemberwiseClone();
            }

            return null;
        }
    }
}
