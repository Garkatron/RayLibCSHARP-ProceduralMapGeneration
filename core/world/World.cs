using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using TerraX69.core.global;
// https://www.raylib.com/cheatsheet/cheatsheet.html
namespace TerraX69.core.world
{
    internal class World
    {
        public int width { get; private set; }
        public int height { get; private set; }
        public MetaData metadata { get; private set; }
        public int chunkSize { get; private set; }
        public List<string> mapChanges { get; private set; }
        public string seed { get; private set; }
        public int chunkIDs { get; private set; }
        public List<Chunk> chunks { get; private set; }
        public List<object> objects { get; private set; }
        public List<object> entities { get; private set; }
        public float noiseScale { get; private set; }

        public World(int width, int height, string seed = "d...s-", int chunkSize = 16)
        {
            this.width = width;
            this.height = height;
            this.metadata = new MetaData();
            this.chunkSize = chunkSize;

            this.mapChanges = new List<string>();
            this.seed = seed;

            Console.WriteLine("The seed: ", this.seed);

            this.chunkIDs = 0;
            this.chunks = new List<Chunk>();
            this.objects = new List<object>();
            this.entities = new List<object>();
            this.noiseScale = 128f;
        }

        private int genSeed()
        {
            return 0;
        }

        public void GenerateStuff(int min, int max, float prob, object obj, int id, List<int> filterList)
        {
            Random seedRandom = new Random(this.genSeed());

            if (min <= 0) { min = 1; }
            if (max <= 0) { max = 1; }
            if (max < min) { max = min; }

            for (int index = 0; index < this.chunks.Count; index++)
            {
                var chunk = this.chunks[index];

                var chunkRandom = new Random(seedRandom.Next());
                double randomValue = chunkRandom.NextDouble();

                int numberOfStuff = (int)Math.Floor(randomValue * (max - min + 1)) + min;


                if (randomValue < prob)
                {
                    //chunk.GenStuff(numberOfStuff, chunkRandom, obj, id, filterList);
                }
            }
        }

        public object CheckCollision(object entity, params object[] objects)
        {
            /*
            int playerTileX = (int)Math.Floor(entity.x / Constants.TILE_SIZE);
            int playerTileY = (int)Math.Floor(entity.y / Constants.TILE_SIZE);

            foreach (var obj in objects)
            {
                int objTileX = (int)Math.Floor(obj.x / Constants.TILE_SIZE);
                int objTileY = (int)Math.Floor(obj.y / Constants.TILE_SIZE);

                if (playerTileX == objTileX && playerTileY == objTileY)
                {
                    return obj;
                }
            }
            */
            return null;

        }

        public Chunk GetChunkAt(int x, int y)
        {
            foreach (Chunk chunk in this.chunks)
            {
                int startX = chunk.x * chunk.size * Constants.TILE_SIZE;
                int startY = chunk.y * chunk.size * Constants.TILE_SIZE;
                int endX = startX + chunk.size * Constants.TILE_SIZE;
                int endY = startY + chunk.size * Constants.TILE_SIZE;

                if (x * Constants.TILE_SIZE >= startX && x * Constants.TILE_SIZE < endX && y * Constants.TILE_SIZE >= startY && y * Constants.TILE_SIZE < endY)
                {
                    return chunk;
                }
            }

            return null;
        }

        public void Draw()
        {
            foreach (var chunk in this.chunks)
            {
                chunk.Draw();
            }

            foreach (var obj in this.objects)
            {
                //obj.Draw();
            }

            foreach (var entity in this.entities)
            {
                //entity.Draw();
            }
        }

        public void Update()
        {
            foreach (var entity in this.entities)
            {
                var collidedObject = CheckCollision(entity, this.objects.ToArray());

                if (collidedObject != null)
                {
                    //entity.HandleCollision(collidedObject);
                }
            }

            foreach (var chunk in this.chunks)
            {
                //chunk.Update();
            }

            foreach (var obj in this.objects)
            {
                //obj.Update(this);
            }

            foreach (var entity in this.entities)
            {
                //entity.Update();
            }
        }

        public void AddEntity(Object entity)
        {
            this.entities.Add(entity);
        }

        public void AddObject(object obj)
        {
            this.objects.Add(obj);
        }

        public void RemoveObject(object obj)
        {
            int index = this.objects.IndexOf(obj);
            if (index != -1)
            {
                this.objects.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("Object not found in the list.");
            }
        }

        /*public void RemoveEntity(Entity entity)
        {
            int index = this.entities.IndexOf(entity);
            if (index != -1)
            {
                this.entities.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("Entity not found in the list.");
            }
        }*/

        public void Generate(Func<int, string> tileParams)
        {

            int width = this.width;
            int height = this.height;
            int chunkSize = this.chunkSize;

            Image noiseImage = Raylib.GenImagePerlinNoise(this.width * Constants.TILE_SIZE * chunkSize, this.height * Constants.TILE_SIZE * chunkSize, 0, 0, this.noiseScale);

            for (int chunkY = 0; chunkY < height; chunkY++)
            {
                for (int chunkX = 0; chunkX < width; chunkX++)
                {
                    string[,] chunkData = new string[chunkSize, chunkSize];

                    for (int y = 0; y < chunkSize; y++)
                    {
                        int globalY = chunkY * chunkSize + y;

                        for (int x = 0; x < chunkSize; x++)
                        {
                            int globalX = chunkX * chunkSize + x;

                            int noiseX = globalX;
                            int noiseY = globalY;

                            Color pixel = Raylib.GetImageColor(noiseImage, noiseX, noiseY);

                            int grayValue = pixel.R;

                            chunkData[y, x] = tileParams(grayValue);
                        }
                    }
  
                    this.chunks.Add(new Chunk(this.chunkIDs++, "Chunk", chunkX, chunkY, this, chunkSize, chunkData));
                }
            }

            Console.WriteLine("Chunks generated: " + this.chunks.Count);

            Raylib.UnloadImage(noiseImage);
        }
    }
}
