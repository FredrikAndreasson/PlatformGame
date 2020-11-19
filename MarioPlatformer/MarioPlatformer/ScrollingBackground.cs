using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class ScrollingBackground
    {
        private Texture2D background;
        private Rectangle destinationRect;
        private Rectangle source;
        private float layerMultiplier;

        public ScrollingBackground(Texture2D background, Rectangle destinationRect, float layerMultiplier, int sourceOffsetY)
        {
            //int levelWidth... get width of level so that the background width can be scaled accordingly instead of *3

            this.background = background;
            this.destinationRect = destinationRect;
            this.destinationRect.Width *= 3; // *= levelWidth...
            source = this.destinationRect;
            source.Y += sourceOffsetY;
            this.layerMultiplier = layerMultiplier;
        }

        public void Update(Vector2 offset)
        {
            //source.X++;
            source.X = (int)(offset.X * layerMultiplier);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, destinationRect, source, Color.White);
        }
    }
}
