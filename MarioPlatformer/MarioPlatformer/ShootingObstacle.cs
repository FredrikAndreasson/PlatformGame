using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class ShootingObstacle : Enemy
    {
        float spawnTimer;
        public List<Bullet> BulletList = new List<Bullet>(); //keep track of its own bullets?

        SpriteSheet bulletTexture;

        public ShootingObstacle(SpriteSheet texture, Level level, Vector2 position, Vector2 size, Vector2 direction, SpriteSheet bulletTexture) : base(texture, level, position, size, 1, 0.0f)
        {
            this.direction = direction;
            this.bulletTexture = bulletTexture;
        }

        protected override Vector2 ChangeDirection(Vector2 playerPosition)
        {

            return new Vector2(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (level.MyPlayer.Position.X > position.X)
            {
                direction.X = 1;
            }
            else
            {
                direction.X = -1;
            }
            if (spawnTimer <= 0)
            {
                level.AddEnemy(new Bullet(bulletTexture, level, position, new Vector2(16, 13), 70.0f, 2000.0f, direction));
                spawnTimer = 500.0f;
            }
            else
            {
                spawnTimer--;
            }

            foreach (Bullet bullet in BulletList)
            {
                bullet.Update(gameTime);
            }

        }

        private void TerrainCollison()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Bullet bullet in BulletList)
            {
                bullet.Draw(spriteBatch);
            }
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
        }

        protected override void InternalUpdate(GameTime gameTime)
        {

        }
    }
}
