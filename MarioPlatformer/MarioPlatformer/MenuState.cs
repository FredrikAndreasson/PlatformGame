using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
   
    

    class MenuState : GameState
    {

        private int selectedActionIndex;
        private SpriteFont font;

        private KeyboardState oldState;

        public MenuState(SpriteFont font)
        {
            this.font = font;
            this.Actions = new List<MenuOption>();
        }        
        public List<MenuOption> Actions
        {
            get;
        }

        private bool IsKeyDown(Keys key)
        {
            return !oldState.IsKeyDown(key) && Keyboard.GetState().IsKeyDown(key);
        }

        public override void Update(GameTime gameTime)
        {
            if(oldState == null)
            {
                oldState = Keyboard.GetState();
            }

            if (IsKeyDown(Keys.Up))
            {
                selectedActionIndex = (selectedActionIndex - 1) % Actions.Count;                
            }
            if (IsKeyDown(Keys.Down))
            {
                selectedActionIndex = (selectedActionIndex + 1) % Actions.Count;                
            }

            if (IsKeyDown(Keys.Enter))
            {
                Actions[selectedActionIndex].Action();
            }

            oldState = Keyboard.GetState();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for(int i = 0; i < Actions.Count; i++)
            {
                Color color = i == selectedActionIndex ? Color.Blue : Color.White;
                spriteBatch.DrawString(font, Actions[i].Text, new Vector2(-100, (i * font.MeasureString(Actions[i].Text).Y) + 10), color);
            }
            spriteBatch.End();
        }
    }

    public class MenuOption
    {
        public delegate void internalAction();
        public MenuOption(string text, internalAction action)
        {
            this.Text = text;
            this.Action = action;
        }

        public string Text
        {
            get;
        }

        public internalAction Action
        {
            get;
        }
    }
}
