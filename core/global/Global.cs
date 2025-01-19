using System;
using System.Collections.Generic;
using Raylib_cs;

namespace TerraX69.core.global
{
    internal class Global
    {
        private static readonly Global _instance = new Global();

        public static Global Instance
        {
            get
            {
                return _instance;
            }
        }

        public object player { get; private set; }
        public Dictionary<string, Texture2D> textureList { get; private set; }
        public MetaData globalMeta { get; private set; }
        public object overworld { get; private set; }

        private Global()
        {
            player = null;
            textureList = new Dictionary<string, Texture2D>();
            globalMeta = new MetaData();
            overworld = null;
        }

        public Global AddTiles(string key, Texture2D tile)
        {
            textureList[key] = tile;
            return this;
        }

        // Método para configurar todos los tiles a la vez
        public Global SetTiles(Dictionary<string, Texture2D> tiles)
        {
            textureList = tiles;
            return this;
        }
    }
}
