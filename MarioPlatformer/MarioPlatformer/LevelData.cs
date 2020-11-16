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
        private int width;
        private int height;

        private TileType[,] tiles;

        public LevelData(string spritesheetFilePath, int width, int height, TileType[,] tiles)
        {
            this.spritesheetFilePath = spritesheetFilePath;
            this.width = width;
            this.height = height;
            this.tiles = tiles;
        }

        public string SpriteSheetFilePath => this.spritesheetFilePath;

        public int Width => this.width;
        public int Height => this.height;

        public TileType[,] Tiles => this.tiles;

        public static LevelData LoadLevelData(string filePath)
        {
            LevelData data = null;
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                string s = reader.ReadString();
                int w = reader.ReadInt32();
                int h = reader.ReadInt32();

                TileType[,] tiles = new TileType[w, h];

                for(int i = 0; i < w * h; i++)
                {
                    int x = i % w;
                    int y = i / w;

                    int value = reader.ReadInt32();
                    TileType type = (TileType)value;
                    tiles[x, y] = type;
                }

                data = new LevelData(s, w, h, tiles);
            }
            return data;
        }

        public static void SaveData(LevelData levelData, string filePath)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                writer.Write(levelData.spritesheetFilePath);
                writer.Write(levelData.width);
                writer.Write(levelData.height);
                                
                for(int i = 0; i < levelData.width * levelData.height;i++)
                {
                    int x = i % levelData.width;
                    int y = i / levelData.width;

                    int value = (int)levelData.tiles[x, y];
                    writer.Write(value);
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
