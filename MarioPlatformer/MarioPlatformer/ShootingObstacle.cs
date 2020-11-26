using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class ShootingObstacle : GameObject
    {
        Vector2 direction;
        float spawnTimer;
        public List<Bullet> BulletList = new List<Bullet>(); //keep track of its own bullets?

        SpriteSheet bulletTexture;

        public ShootingObstacle(SpriteSheet texture, Level level, Vector2 position, Vector2 size, Vector2 direction, SpriteSheet bulletTexture) : base(texture, level, position, size)
        {
            this.direction = direction;
            this.bulletTexture = bulletTexture;
        }


        public void Update(GameTime gameTime)
        {
            if (spawnTimer <= 0)
            {
                BulletList.Add(new Bullet(bulletTexture, level, position, new Vector2(16, 13), 70.0f, 2000.0f, direction));
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Bullet bullet in BulletList)
            {
                bullet.Draw(spriteBatch);
            }
        }

    }
}
