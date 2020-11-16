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
        private ScrollingBackground background2;

        private Level level;

        public InGameState(SpriteSheetLoader loader, GraphicsDevice graphicsDevice, GameWindow window)
        {
            SpriteSheet playerAnimationSheet = loader.LoadSpriteSheet("player", new Vector2(0, 0), new Vector2(12, 16), new Vector2(12, 16));
            Texture2D backgroundTex = loader.LoadTexture("background");
            Texture2D background2Tex = loader.LoadTexture("background2");

            this.player = new Player(playerAnimationSheet, new Vector2(250, 250), 5, 500.0f);
            this.camera = new Camera(graphicsDevice.Viewport);
            this.background = new ScrollingBackground(backgroundTex, new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height));
            this.background2 = new ScrollingBackground(background2Tex, new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height));

            this.level = new Level(loader, LevelData.LoadLevelData("Content\\Level1.lvl"));
        }
        public override void Update(GameTime gameTime)
        {
            background.Update(gameTime, player.Position, 0.1f);
            background2.Update(gameTime, player.Position, 0.2f);
            player.Update(gameTime);

            //Update last so that the player stays centered
            camera.SetPosition(player.Position);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null, null);
            background.Draw(spriteBatch);
            background2.Draw(spriteBatch);
            spriteBatch.End();


            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null, null, camera.Transform);

            //background.Draw(spriteBatch);
            player.Draw(spriteBatch);
            level.Draw(spriteBatch);
            
            spriteBatch.End();
        }
    }
}
