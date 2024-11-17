using UnityEngine;
using System;

namespace TapBlitz.Game
{
    public enum TileType
    {
        Plain,
        Bomb,
        Rocket
    }

    public class Tile
    {
        public TileType type;
        public Sprite sprite;
        public Color? color;

        public static Tile CreatePlainTile(Color color)
        {
            return new Tile(TileType.Plain, null, color);
        }

        public static Tile CreateSpecialTile(TileType type, Sprite sprite)
        {
            return new Tile(type, sprite, null);
        }

        private Tile(TileType type, Sprite sprite, Color? color)
        {
            this.type = type;
            this.sprite = sprite;
            this.color = color;
        }
    }
}
