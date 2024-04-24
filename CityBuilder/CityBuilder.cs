using GLEngine;
using GLEngine.RenderCore;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CityBuilder;

public class CityBuilder : Game
{
    private readonly Vector3 _lightPos = new Vector3(1.2f, 1.0f, 2.0f);
    //protected List<Mesh> _cubes = [];

    protected Renderer _renderer = new();
    protected World _world = new();

    // private Texture _texture1;
    // private Texture _texture2;

    public static void Main(string[] args)
    {
        var game = new CityBuilder();
        game.Run();
    }

    public CityBuilder() : base("City Builder")
    {
        Window.BackgroundColor = Color4.White;
    }

    public override void OnLoad()
    {
        for (int i = 0; i < 50; i++)
        {
            var monkeyActor = new MonkeyHead();
            
            Vector3 spawnPos = new Vector3();
            spawnPos.X = (float)Random.Shared.NextDouble() * 20f - 10f;
            spawnPos.Y = (float)Random.Shared.NextDouble() * 20f - 10f;
            spawnPos.Z = (float)Random.Shared.NextDouble() * 20f - 10f;
            monkeyActor.Transform.Position = spawnPos;

            Vector3 spawnRot = new Vector3();
            spawnRot.X = MathHelper.DegreesToRadians(-90f);
            spawnRot.Z = MathHelper.DegreesToRadians((float)Random.Shared.NextDouble() * 360f);
            monkeyActor.Transform.Rotation = Rotator.FromEuler(spawnRot).GetQuaternion();
            
            _world.Spawn(monkeyActor);
        }
        
        // _texture1 = new Texture("res/textures/container.jpg");
        // _texture2 = new Texture("res/textures/awesomeface.png");
        // _shader.SetInt("texture1", 0);
        // _shader.SetInt("texture2", 1);
    }

    public override void OnRender()
    {
        // _texture1.Use(TextureUnit.Texture0);
        // _texture2.Use(TextureUnit.Texture1);
        
        _renderer.Render(_world, Camera.Main);
    }

    public override void OnUpdate()
    {
        KeyboardState? input = Window.KeyboardState;
        
        if(Window.IsFocused)
            _mainFirstPersonCamera.OnUpdateFrame(input, Window.MousePosition);
        
        if (input.IsKeyPressed(Keys.Escape)) 
            Window.Close();
        
        if (input.IsKeyPressed(Keys.F11)) 
            Window.WindowState = Window.WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;
    }
}