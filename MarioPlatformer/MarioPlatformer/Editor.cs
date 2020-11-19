using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    class Editor : GameState
    {
        private SpriteSheetLoader loader;
        private Dictionary<Vector2, Tile> tiles;
        private Camera camera;
        private Viewport levelViewport;
        private Viewport paletteViewport;

        private Vector2 mouseTile;
        private Vector2 paletteTile;

        private SpriteSheet palette;
        private Texture2D paletteBackground;
        private Texture2D viewportSplit;
        private int viewportLineSplitWidth;
        private SpriteSheet selectedPalette;
        private Vector2 selectedPaletteIndex;
        private Sprite selectionSprite;

        private Point oldMousePosition;        

        public Editor(SpriteSheetLoader loader, GameWindow window)
        {
            this.loader = loader;
            this.tiles = new Dictionary<Vector2, Tile>();            
            this.camera = new Camera(new Viewport());

            this.mouseTile = GetTileOnCursor();

            this.palette = loader.LoadSpriteSheet("blocks", Vector2.Zero, new Vector2(80, 80), new Vector2(16, 16), 0);
            this.paletteBackground = loader.CreateFilledTexture(1, 1, new Color(10, 10, 10));
            this.viewportSplit = loader.CreateFilledTexture(1, 1, new Color(128, 128, 128));
            this.viewportLineSplitWidth = 1;
            this.selectedPaletteIndex = new Vector2(0,0);
            this.selectedPalette = GetSpriteSheetAt((int)selectedPaletteIndex.X, (int)selectedPaletteIndex.Y);
            
            this.levelViewport = new Viewport(0, 0, window.ClientBounds.Width - this.palette.Texture.Width, window.ClientBounds.Height);
            this.paletteViewport = new Viewport(levelViewport.Width, 0, this.palette.Texture.Width, window.ClientBounds.Height);

            Texture2D rectangleTexture = loader.CreateRectangleTexture(Tile.SIZE, Tile.SIZE, Color.White);
            this.selectionSprite = new SpriteSheet(rectangleTexture).Sprite;
        }

        private SpriteSheet GetSpriteSheetAt(int x, int y)
        {
            return this.palette.GetSubAt(x, y, 1, 1);
        }
        
        public Vector2 GetTileOnCursor()
        {
            Vector2 translation = new Vector2(camera.Transform.Translation.X, camera.Transform.Translation.Y);
            Vector2 mousePos = Mouse.GetState().Position.ToVector2();
            Vector2 tilePos = (mousePos - translation) / (Tile.SIZE * Game1.Scale);            
            tilePos.Floor();
            return tilePos;
        }

        public Vector2 GetPaletteOnCursor()
        {
            Vector2 mousePos = Mouse.GetState().Position.ToVector2();            
            Vector2 palettePos = new Vector2(paletteViewport.X, paletteViewport.Y);
            Vector2 tilePos = (mousePos - palettePos + new Vector2(viewportLineSplitWidth, 0)) / Tile.SIZE;
            tilePos.Floor();
            return tilePos;
        }

        public override void Update(GameTime gameTime)
        {
            mouseTile = GetTileOnCursor() * Tile.SIZE * Game1.Scale;
            paletteTile = GetPaletteOnCursor();
            Point mousePos = Mouse.GetState().Position;

            bool insideLevel = levelViewport.Bounds.Contains(mousePos);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if(insideLevel)
                {
                    int type = (int)((selectedPaletteIndex.X) + (selectedPaletteIndex.Y * palette.Columns));
                    tiles[mouseTile] = new Tile(selectedPalette, mouseTile, (TileType)type);
                }
                else
                {
                    selectedPaletteIndex = paletteTile;
                    selectedPalette = GetSpriteSheetAt((int)selectedPaletteIndex.X, (int)selectedPaletteIndex.Y);
                }
            }

            if(Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                if(insideLevel)
                {
                    tiles.Remove(mouseTile);
                }
            }

            if (Mouse.GetState().MiddleButton == ButtonState.Pressed && insideLevel)
            {
                Point delta = mousePos - oldMousePosition;

                Matrix transform = camera.Transform;
                Vector3 translation = transform.Translation;
                translation.X += delta.X;
                translation.Y += delta.Y;

                transform.Translation = translation;
                camera.Transform = transform;
            }
            oldMousePosition = mousePos;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null,null, camera.Transform);
            foreach(Tile tile in tiles.Values)
            {
                tile.Draw(spriteBatch);
            }

            selectionSprite.Draw(spriteBatch, mouseTile, Game1.Scale, SpriteEffects.None, new Color(255,255,255, 128));
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, null, null, null);
            spriteBatch.Draw(paletteBackground, paletteViewport.Bounds, Color.White);
            spriteBatch.Draw(viewportSplit, new Rectangle(paletteViewport.X, paletteViewport.Y, viewportLineSplitWidth, paletteViewport.Height), Color.White);
            spriteBatch.Draw(palette.Texture, new Vector2(paletteViewport.X+ viewportLineSplitWidth, paletteViewport.Y), Color.White);

            if(paletteViewport.Bounds.Contains(Mouse.GetState().Position))
            {
                Vector2 hoveredPalettePosition = new Vector2(paletteViewport.X + viewportLineSplitWidth, paletteViewport.Y) + (paletteTile * Tile.SIZE);
                selectionSprite.Draw(spriteBatch, hoveredPalettePosition, Vector2.One, SpriteEffects.None, Color.White);
            }

            Vector2 selectedTilePosition = new Vector2(paletteViewport.X + viewportLineSplitWidth, paletteViewport.Y) + (selectedPaletteIndex * Tile.SIZE);
            selectionSprite.Draw(spriteBatch, selectedTilePosition, Vector2.One, SpriteEffects.None, new Color(0,255,0,255));
            spriteBatch.End();
        }        
    }
}
