using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    public class Cannon : Enemy
    {
        private double spawnTimer;
        private List<Bullet> bullets;

        private SpriteSheet bulletTexture;

        public Cannon(SpriteSheet texture, Level level, Vector2 position, Vector2 size, SpriteSheet bulletTexture) : base(texture, level, position, size, 1, 0.0f)
        {
            this.bulletTexture = bulletTexture;
            this.bullets = new List<Bullet>();
        }

        protected override void ChangeDirection()
        {
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 shootDirection = Vector2.Zero;
            shootDirection.X = level.MyPlayer.Position.X > position.X ? 1 : -1;

            spawnTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (spawnTimer <= 0)
            {
                level.AddEnemy(new Bullet(bulletTexture, level, position, new Vector2(16, 13), 70.0f, 2000.0f, shootDirection));
                spawnTimer = 5000.0f;
            }

            foreach (Bullet bullet in bullets)
            {
                bullet.Update(gameTime);
            }
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Bullet bullet in bullets)
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
