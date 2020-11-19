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

        private Level level;

        GraphicsDevice graphicsDevice;

        ParalaxBackgroundManager backgroundManager;

        public InGameState(SpriteSheetLoader loader, GraphicsDevice graphicsDevice, GameWindow window)
        {
            SpriteSheet playerAnimationSheet = loader.LoadSpriteSheet("player", new Vector2(0, 0), new Vector2(12, 16), new Vector2(12, 16));

            this.graphicsDevice = graphicsDevice;

            this.player = new Player(playerAnimationSheet, new Vector2(250, 250), 5, 200.0f);
            this.camera = new Camera(graphicsDevice.Viewport);

            this.level = new Level(loader, LevelData.LoadLevelData("Content\\Level1.lvl"));

            backgroundManager = new ParalaxBackgroundManager(player, loader, graphicsDevice, window);
        }
        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            //Update after player so that the player stays centered
            camera.SetPosition(player.Position);

            backgroundManager.Update(level.IsDay);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color day = new Color(192, 2248, 248);
            Color night = new Color(0, 0, 24);
            graphicsDevice.Clear(level.IsDay ? day : night);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null, null);
            backgroundManager.Draw(spriteBatch);
            spriteBatch.End();


            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null, null, camera.Transform);
            player.Draw(spriteBatch);
            level.Draw(spriteBatch);
            
            spriteBatch.End();
        }
    }
}
