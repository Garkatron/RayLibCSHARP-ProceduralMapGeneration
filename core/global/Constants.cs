using System;
using System.Collections.Generic;

namespace TerraX69.core.global
{
    internal class Constants
    {

        public static readonly int TILE_SIZE = 12;
        public static readonly int CHUNK_SIZE = 16;

        public static readonly string SPACE = "SPACE";
        public static readonly string PLAYER = "PLAYER";
        public static readonly string WATER = "WATER";
        public static readonly string GRASS = "GRASS";
        public static readonly string DIRTH = "DIRTH";
        public static readonly string MOUNTAIN = "MOUNTAIN";
        public static readonly string CAVE = "CAVE";
        public static readonly string STONE = "STONE";
        public static readonly string STONE_FLOOR = "STONE_FLOOR";
        public static readonly string CITY = "CITY";
        public static readonly string LONG_GRASS = "LONG_GRASS";
        public static readonly string SHORT_GRASS = "SHORT_GRASS";

        public static readonly Dictionary<string, string> DIRECTION = new Dictionary<string, string>
        {
            { "NORTH", "NORTH" },
            { "SOUTH", "SOUTH" },
            { "EAST", "EAST" },
            { "WEST", "WEST" }
        };

        public static string OVERWORLD(int value)
        {
            if (value < 100)
            {
                return WATER;

            } else if(value < 140)
            {
                return GRASS;
            }
            else if (value < 150)
            {
                return DIRTH;
            }
            else if (value < 240)
            {
                return MOUNTAIN;
            }


            return SPACE;
        }

        public static string CAVE_WORLD(int value)
        {
            if (value < 20)
            {
                return WATER;
            }
            else if (value < 30)
            {
                return STONE_FLOOR;
            }
            else if (value < 40)
            {
                return MOUNTAIN;
            }
            else if (value < 50)
            {
                return STONE_FLOOR;
            }
            else if (value < 55)
            {
                return STONE;
            }
            else if (value < 60)
            {
                return SHORT_GRASS;
            }
            else if (value < 700)
            {
                return WATER;
            }
            return WATER;
        }

        public static readonly List<string> OBSTACLE = new List<string>
        {
            WATER,
            MOUNTAIN
        };

        public static readonly List<string> CITY_OBSTACLE = new List<string>
        {
            WATER,
            MOUNTAIN,
            CAVE
        };

        public static readonly List<string> DESTROYABLE = new List<string>
        {
            MOUNTAIN
        };
    }
}
