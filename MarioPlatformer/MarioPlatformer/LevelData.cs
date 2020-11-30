using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarioPlatformer
{       
    class LevelData
    {
        private string spritesheetFilePath;
        private Tile[] tiles;
        private Tile[] objects;

        public LevelData(string spritesheetFilePath, Tile[] tiles, Tile[] objects)
        {
            this.spritesheetFilePath = spritesheetFilePath;
            this.tiles = tiles;
            this.objects = objects;
        }

        public string SpriteSheetFilePath => this.spritesheetFilePath;

        public int Size => Tiles.Length;

        public Tile[] Tiles => this.tiles;

        public Tile[] Objects => this.objects;

        public static LevelData LoadLevelData(string filePath)
        {
            LevelData data = null;
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                string spritesPath = reader.ReadString();
                int tileCount = reader.ReadInt32();

                Tile[] tiles = new Tile[tileCount];

                for(int i = 0; i < tileCount; i++)
                {
                    int value = reader.ReadInt32();
                    float x = reader.ReadSingle();
                    float y = reader.ReadSingle();
                    int type = value;
                    tiles[i] = new Tile(null, null, new Vector2(x,y), type);
                }

                int objectCount = reader.ReadInt32();

                Tile[] objects = new Tile[objectCount];
                for (int i = 0; i < objectCount; i++)
                {
                    int value = reader.ReadInt32();
                    float x = reader.ReadSingle();
                    float y = reader.ReadSingle();
                    int type = value;
                    objects[i] = new Tile(null, null, new Vector2(x, y), type);
                }

                data = new LevelData(spritesPath, tiles, objects);
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
                    writer.Write((int)tile.IDType);
                    writer.Write(tile.Position.X);
                    writer.Write(tile.Position.Y);
                }

                writer.Write(levelData.Objects.Length);
                for (int i = 0; i < levelData.Objects.Length; i++)
                {
                    Tile tile = levelData.Objects[i];
                    writer.Write((int)tile.IDType);
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
