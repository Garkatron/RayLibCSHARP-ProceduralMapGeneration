using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using TerraX69.core.global;
using TerraX69.core.objects;
using Windows.UI.StartScreen;

namespace TerraX69.core.world
{
    internal class Chunk : TileObject
    {
        private World world;
        public int size { get; private set;}
        private string[,] map;
        private Dictionary<string, Texture2D> textureList;
        private MetaData metadata;

        public Chunk(int id, string name, int x, int y, World world, int size, string[,] map)
            : base(id, name)
        {

            this.size = size;

            this.x = x * this.size;
            this.y = y * this.size;

            this.world = world;
            this.map = map;

            this.textureList = Global.Instance.textureList;

            this.metadata = new MetaData();
        }

        public override void Draw()
        {
            float cameraLeft = Program.camera.Target.X - Raylib.GetScreenWidth() / 2 / Program.camera.Zoom;
            float cameraRight = Program.camera.Target.X + Raylib.GetScreenWidth() / 2 / Program.camera.Zoom;
            float cameraTop = Program.camera.Target.Y - Raylib.GetScreenHeight() / 2 / Program.camera.Zoom;
            float cameraBottom = Program.camera.Target.Y + Raylib.GetScreenHeight() / 2 / Program.camera.Zoom;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    string id = this.map[i, j];
                    var texture = this.textureList[id];

                    // Calculamos la posición global de la tile
                    int globalX = (this.x + j) * Constants.TILE_SIZE;
                    int globalY = (this.y + i) * Constants.TILE_SIZE;

                    // Verificamos si la tile está dentro del rango visible de la cámara
                    if (globalX + Constants.TILE_SIZE > cameraLeft && globalX < cameraRight &&
                        globalY + Constants.TILE_SIZE > cameraTop && globalY < cameraBottom)
                    {
                        // Dibuja la textura de la tile si está en el rango visible
                        Raylib.DrawTexture(texture, globalX, globalY, Raylib_cs.Color.White);
                    }
                }
            }
        }


        public override void Update()
        {
        }


    }

}
