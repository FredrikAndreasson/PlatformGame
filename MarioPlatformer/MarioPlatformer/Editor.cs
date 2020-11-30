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
        private Dictionary<Vector2, Tile> gameObjectTiles;

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
        private KeyboardState oldKeyboardState;

        public Editor(SpriteSheetLoader loader, GameWindow window)
        {
            this.loader = loader;
            this.tiles = new Dictionary<Vector2, Tile>();
            this.gameObjectTiles = new Dictionary<Vector2, Tile>();
            this.camera = new Camera(new Viewport());

            this.mouseTile = GetTileOnCursor();

            this.palette = loader.LoadSpriteSheet("blocks", Vector2.Zero, new Vector2(16, 16), 0);
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
        
        private Vector2 GetTileOnCursor()
        {
            Vector2 translation = new Vector2(camera.Transform.Translation.X, camera.Transform.Translation.Y);
            Vector2 mousePos = Mouse.GetState().Position.ToVector2();
            Vector2 tilePos = (mousePos - translation) / (Tile.SIZE * Game1.Scale);            
            tilePos.Floor();
            return tilePos;
        }

        private Vector2 GetPaletteOnCursor()
        {
            Vector2 mousePos = Mouse.GetState().Position.ToVector2();            
            Vector2 palettePos = new Vector2(paletteViewport.X, paletteViewport.Y);
            Vector2 tilePos = (mousePos - palettePos + new Vector2(0, 0)) / Tile.SIZE;
            tilePos.Floor();
            return tilePos;
        }

        private LevelData CreateLevelData()
        {
            Tile[] tilesData = new Tile[tiles.Count];
            tiles.Values.CopyTo(tilesData, 0);
            Tile[] objectData = new Tile[gameObjectTiles.Count];
            gameObjectTiles.Values.CopyTo(objectData, 0);
            LevelData data = new LevelData("blocks", tilesData, objectData);
            return data;
        }

        public override void Update(GameTime gameTime)
        {
            mouseTile = GetTileOnCursor() * Tile.SIZE * Game1.Scale;
            paletteTile = GetPaletteOnCursor();
            KeyboardState keyboardState = Keyboard.GetState();
            Point mousePos = Mouse.GetState().Position;

            bool insideLevel = levelViewport.Bounds.Contains(mousePos);

            if(oldKeyboardState == null)
            {
                oldKeyboardState = keyboardState;
            }

            if(keyboardState.IsKeyDown(Keys.LeftControl) && keyboardState.IsKeyDown(Keys.S) && !oldKeyboardState.IsKeyDown(Keys.S))
            {
                LevelData.SaveData(CreateLevelData(), "Content\\Level1.lvl");
                System.Diagnostics.Debug.WriteLine("Level1 saved");
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if(insideLevel)
                {
                    int type = (int)((selectedPaletteIndex.X) + (selectedPaletteIndex.Y * palette.Columns));
                    if(type == 90 || type == 91)
                    {
                        gameObjectTiles[mouseTile] = new Tile(selectedPalette, null, mouseTile, type);
                    }
                    else
                    {
                        tiles[mouseTile] = new Tile(selectedPalette, null, mouseTile, type);
                    }
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
            oldKeyboardState = keyboardState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null,null, camera.Transform);
            foreach (Tile tile in tiles.Values)
            {
                tile.Draw(spriteBatch);
            }
            foreach (Tile tile in gameObjectTiles.Values)
            {
                /*int x = entry.Value % palette.Columns;
                int y = entry.Value / palette.Columns;
                Sprite sprite = palette.GetSubAt(x, y, 1, 1).Sprite;
                sprite.Draw(spriteBatch, entry.Key, Game1.Scale);*/

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
