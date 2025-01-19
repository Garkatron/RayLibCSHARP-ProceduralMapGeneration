using Raylib_cs;
using TerraX69.core.global;
using TerraX69.core.world;

class Program
{
    public static Camera2D camera;

    static void Main(string[] args)
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        
        Raylib.InitWindow(800, 600, "Raylib en C#");

        // Define the 2D camera
        // Define la cámara 2D
        camera = new Camera2D
        {
            Target = new System.Numerics.Vector2(800 / 2, 600 / 2), // Centra la cámara en el medio de la ventana
            Offset = new System.Numerics.Vector2(800 / 2, 600 / 2),  // Offset para que la cámara se mantenga centrada
            Rotation = 0.0f,                                          // Rotación de la cámara
            Zoom = 1.0f                                               // Zoom de la cámara
        };

        // Set the target FPS
        Raylib.SetTargetFPS(60);

        Global global = Global.Instance;
        global.SetTiles(
            new Dictionary<string, Texture2D>
            {
                { Constants.CAVE, LoadTexture(Constants.CAVE, TerraX69.Properties.Resources.cave) },
                { Constants.SPACE, LoadTexture(Constants.CAVE, TerraX69.Properties.Resources.space) },
                { Constants.PLAYER, LoadTexture(Constants.PLAYER, TerraX69.Properties.Resources.player) },
                { Constants.STONE_FLOOR, LoadTexture(Constants.STONE_FLOOR, TerraX69.Properties.Resources.stone_floor) },
                { Constants.STONE, LoadTexture(Constants.STONE, TerraX69.Properties.Resources.pebble) },
                { Constants.MOUNTAIN, LoadTexture(Constants.MOUNTAIN, TerraX69.Properties.Resources.stone) },
                { Constants.WATER, LoadTexture(Constants.WATER, TerraX69.Properties.Resources.water) },
                { Constants.GRASS, LoadTexture(Constants.GRASS, TerraX69.Properties.Resources.grass) },
                { Constants.LONG_GRASS, LoadTexture(Constants.LONG_GRASS, TerraX69.Properties.Resources.long_grass) },
                { Constants.SHORT_GRASS, LoadTexture(Constants.SHORT_GRASS, TerraX69.Properties.Resources.short_grass) },
                { Constants.CITY, LoadTexture(Constants.CITY, TerraX69.Properties.Resources.city) },
                { Constants.DIRTH, LoadTexture(Constants.DIRTH, TerraX69.Properties.Resources.dirth) }
            }
        );

        World test = new World(12, 12, "adam", 16);
        test.Generate(Constants.OVERWORLD);

        // Main game loop
        while (!Raylib.WindowShouldClose())
        {
            // Update the camera with user input
            if (Raylib.IsKeyDown(KeyboardKey.W)) camera.Target.Y -= 35.0f; // Move up
            if (Raylib.IsKeyDown(KeyboardKey.S)) camera.Target.Y += 35.0f; // Move down
            if (Raylib.IsKeyDown(KeyboardKey.A)) camera.Target.X -= 35.0f; // Move left
            if (Raylib.IsKeyDown(KeyboardKey.D)) camera.Target.X += 35.0f; // Move right
            if (Raylib.IsKeyDown(KeyboardKey.Up)) camera.Zoom += 0.1f;     // Zoom in
            if (Raylib.IsKeyDown(KeyboardKey.Down)) camera.Zoom -= 0.1f;   // Zoom out

            if (Raylib.IsWindowResized())
            {
                camera.Target = new System.Numerics.Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);
                camera.Offset = new System.Numerics.Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);

            }


            // Begin drawing
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkGray);

            // Start 2D rendering with the camera applied
            Raylib.BeginMode2D(camera);

            // Update and draw the world
            test.Update();
            test.Draw();

            // Draw a simple rectangle at the camera target (camera center)
            Raylib.DrawRectangleV(camera.Target, new System.Numerics.Vector2(50.0f, 50.0f), Color.DarkBlue);

            // End 2D mode (applies the camera)
            Raylib.EndMode2D();

            // Draw text to help the user
            Raylib.DrawText("Use WASD to move the camera", 10, 10, 20, Color.DarkGray);
            Raylib.DrawText("Use arrow keys to zoom in/out", 10, 30, 20, Color.DarkGray);

            // End drawing
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    private static Texture2D LoadTexture(string name, byte[] prop)
    {
        byte[] bytes = prop;
        Image image = Raylib.LoadImageFromMemory(".png", bytes);

        if (image.Width == 0 || image.Height == 0)
        {
            Console.WriteLine("La imagen no se cargó correctamente.");
        }
        else
        {
            Console.WriteLine("Imagen cargada correctamente.");
        }
        return Raylib.LoadTextureFromImage(image);
    }
}
