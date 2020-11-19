using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class ParalaxBackgroundManager
    {
        private Player player;

        //Triple scrolling background to create the illusion of different depths'
        private List<ScrollingBackground> dayBackgrounds;
        private List<ScrollingBackground> nightBackgrounds;

        private bool isDay = true;

        public ParalaxBackgroundManager(Player player, SpriteSheetLoader loader, GraphicsDevice graphicsDevice, GameWindow window)
        {
            this.player = player;

            dayBackgrounds = new List<ScrollingBackground>();
            nightBackgrounds = new List<ScrollingBackground>();

            Texture2D cloudsTex = loader.LoadTexture("background");
            Texture2D nightHillTex = loader.LoadTexture("Backgrounds\\NightHill");
            Texture2D nightHillBackTex = loader.LoadTexture("Backgrounds\\NightHillBack");
            Texture2D nightSkyTex = loader.LoadTexture("Backgrounds\\NightSky");
            Texture2D moonTex = loader.LoadTexture("Backgrounds\\moon");
            Texture2D dayHillTex = loader.LoadTexture("Backgrounds\\DayHill");
            Texture2D dayHillBackTex = loader.LoadTexture("Backgrounds\\DayHillBack");
            Texture2D daySkyTex = loader.LoadTexture("Backgrounds\\DaySky");


            
            
            dayBackgrounds.Add(new ScrollingBackground(dayHillBackTex, new Rectangle(0, window.ClientBounds.Height - dayHillBackTex.Height, window.ClientBounds.Width, (int)(dayHillBackTex.Height)), 0.25f,-4));
            dayBackgrounds.Add(new ScrollingBackground(dayHillTex, new Rectangle(-100, window.ClientBounds.Height - dayHillTex.Height+30, window.ClientBounds.Width, (int)(dayHillTex.Height)), 0.3f, -34));
            dayBackgrounds.Add(new ScrollingBackground(daySkyTex, new Rectangle(0, 0, window.ClientBounds.Width, daySkyTex.Height), 0.1f,0));
            dayBackgrounds.Add(new ScrollingBackground(cloudsTex, new Rectangle(0, 0, window.ClientBounds.Width, cloudsTex.Height), 0.5f, 0));

            nightBackgrounds.Add(new ScrollingBackground(nightHillBackTex, new Rectangle(0, window.ClientBounds.Height - nightHillBackTex.Height, window.ClientBounds.Width, (int)(nightHillBackTex.Height)), 0.25f, -4));
            nightBackgrounds.Add(new ScrollingBackground(nightHillTex, new Rectangle(-100, window.ClientBounds.Height - nightHillTex.Height + 30, window.ClientBounds.Width, (int)(nightHillTex.Height)), 0.3f, -34));
            nightBackgrounds.Add(new ScrollingBackground(nightSkyTex, new Rectangle(0, 0, window.ClientBounds.Width, nightSkyTex.Height), 0.1f, 0));
            nightBackgrounds.Add(new ScrollingBackground(moonTex, new Rectangle((int)(window.ClientBounds.Width*0.8), (int)(window.ClientBounds.Height * 0.01), moonTex.Width/3, moonTex.Height), 0.1f,0));
        }

        public void Update(bool isDay)
        {
            this.isDay = isDay;
            if (isDay)
            {
                foreach (ScrollingBackground bkground in dayBackgrounds)
                {
                    bkground.Update(player.Position);
                }
            }
            else
            {
                foreach (ScrollingBackground bkground in nightBackgrounds)
                {
                    bkground.Update(player.Position);
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isDay)
            {
                foreach (ScrollingBackground bkground in dayBackgrounds)
                {
                    bkground.Draw(spriteBatch);
                }
            }
            else
            {
                foreach (ScrollingBackground bkground in nightBackgrounds)
                {
                    bkground.Draw(spriteBatch);
                }
            }
        }
    }
}
