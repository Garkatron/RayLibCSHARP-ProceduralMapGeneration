using System;
using TerraX69.core.global;
using TerraX69.core.interfaces;

namespace TerraX69.core.objects
{
    internal abstract class TileObject : ITileObject
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public TileObject(int id, string name)
        {
            this.id = id;
            this.Name = name;
            this.x = 0;
            this.y = 0;
        }

        public void HandleCollision(object obj)
        {
            // Lógica de colisión que puede ser común a todas las subclases
        }
        public abstract void Update();
        public abstract void Draw();
    }
}
