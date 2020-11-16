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
        private ScrollingBackground background;

        public InGameState(Player player, Camera camera, ScrollingBackground background)
        {
            this.player = player;
            this.camera = camera;
            this.background = background;
        }
        public override void Update(GameTime gameTime)
        {
            background.Update(gameTime, (int)player.Position.Y);
            player.Update(gameTime);

            //Update last so that the player stays centered
            camera.SetPosition(player.Position);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            player.Draw(spriteBatch);
        }
    }
}
