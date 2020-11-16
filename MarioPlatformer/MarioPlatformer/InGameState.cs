using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class InGameState : GameState
    {
        private Player player;
        private Camera camera;

        public InGameState(Player player, Camera camera)
        {
            this.player = player;
            this.camera = camera;
        }
        public override void Update(GameTime gameTime)
        {
            camera.SetPosition(player.Position);
            player.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
        }
    }
}
