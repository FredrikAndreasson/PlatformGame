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

        private GameWindow window;
        private HUD hud;
        private KeyboardState oldState;

        public MenuState(SpriteFont font, GameWindow window, HUD hud)
        {
            this.font = font;
            this.window = window;
            this.hud = hud;
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

        private void DrawHighscores(SpriteBatch spriteBatch, Vector2 position)
        {
            float x = position.X;
            float y = position.Y;

            hud.HighscoreSprite.Draw(spriteBatch, new Vector2(x - (hud.HighscoreSprite.SpriteSize.X / 2), y), Vector2.One);
            y += (hud.HighscoreSprite.SpriteSize.Y) + 15;

            int scoreLength = (int)(8 * 6);

            if (hud.Highscores.Count == 0)
            {
                Vector2 highscorePosition = new Vector2(x - (scoreLength / 2), y);
                hud.DrawScore(spriteBatch, 0, highscorePosition, Vector2.One, Color.White);
                return;
            }

            bool hasShownYourScore = false;
            for (int i = 0; i < 7; i++)
            {
                if (i >= hud.Highscores.Count)
                {
                    break;
                }

                Vector2 highscorePosition = new Vector2(x - (scoreLength / 2), y);

                hud.DrawScore(spriteBatch, hud.Highscores[i], highscorePosition, Vector2.One, (hud.Highscores[i] == hud.Score && !hasShownYourScore) ? Color.Yellow : Color.White);
                if (hud.Highscores[i] == hud.Score)
                {
                    hasShownYourScore = true;
                }
                y += 15;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null);

            float characterHeight = font.MeasureString("A").Y + 5;

            DrawHighscores(spriteBatch, new Vector2(window.ClientBounds.Width / 2, 50));

            for (int i = 0; i < Actions.Count; i++)
            {
                Vector2 menuDimensions = new Vector2(font.MeasureString(Actions[0].Text).X, characterHeight * Actions.Count);
                Color color = i == selectedActionIndex ? Color.Green : Color.White;
                Vector2 position = new Vector2((window.ClientBounds.Width / 2) - (int)(menuDimensions.X / 2), (window.ClientBounds.Height / 2) - (int)(menuDimensions.Y / 2) + (characterHeight * i));
                spriteBatch.DrawString(font, Actions[i].Text, position, color);
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
