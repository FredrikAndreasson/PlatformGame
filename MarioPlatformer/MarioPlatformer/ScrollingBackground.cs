using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class ScrollingBackground
    {
        Texture2D background;
        Rectangle position;
        Rectangle source;

        public ScrollingBackground(Texture2D background, Rectangle position)
        {
            //int levelWidth... get width of level so that the background width can be scaled accordingly instead of *10

            this.background = background;
            this.position = position;
            this.position.Width *= 3; // *= levelWidth...
            this.position.Height *= 3; // *= levelHeight...
            source = this.position;
            
        }

        public void Update(GameTime gameTime, Vector2 offset, float layerMultiplier)
        {
            //source.X++;
            
            source.X = (int)(offset.X * layerMultiplier);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, position, source, Color.White);
        }
    }
}
