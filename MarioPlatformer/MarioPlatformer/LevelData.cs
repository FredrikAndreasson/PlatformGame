using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarioPlatformer
{

    public enum TileType
    {
        Empty,
        Block1,
        Block2,
        Block3,
    }

    class LevelData
    {
        private string spritesheetFilePath;       
        private Tile[] tiles;

        public LevelData(string spritesheetFilePath, Tile[] tiles)
        {
            this.spritesheetFilePath = spritesheetFilePath;
            this.tiles = tiles;
        }

        public string SpriteSheetFilePath => this.spritesheetFilePath;

        public int Size => Tiles.Length;

        public Tile[] Tiles => this.tiles;

        public static LevelData LoadLevelData(string filePath)
        {
            LevelData data = null;
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                string spritesPath = reader.ReadString();
                int size = reader.ReadInt32();

                Tile[] tiles = new Tile[size];

                for(int i = 0; i < size; i++)
                {
                    int value = reader.ReadInt32();
                    float x = reader.ReadSingle();
                    float y = reader.ReadSingle();
                    TileType type = (TileType)value;
                    tiles[i] = new Tile(null, null, new Vector2(x,y), type);
                }

                data = new LevelData(spritesPath, tiles);
            }
            return data;
        }

        public static void SaveData(LevelData levelData, string filePath)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                writer.Write(levelData.spritesheetFilePath);
                writer.Write(levelData.Size);
                                
                for(int i = 0; i < levelData.Size; i++)
                {
                    Tile tile = levelData.tiles[i];
                    writer.Write((int)tile.Type);
                    writer.Write(tile.Position.X);
                    writer.Write(tile.Position.Y);
                }
            }
        }
        
    }

    public static class BinaryReaderExtensions
    {
        public static string ReadString(this BinaryReader reader)
        {
            string s = "";
            while(true)
            {
                char c = reader.ReadChar();
                if(c == '\0')
                {
                    break;
                }
                s += c;
            }
            return s;
        }

    }
}
