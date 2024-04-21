using System.Diagnostics;
using GLEngine;
using GLEngine.RenderCore;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CityBuilder;

public class CityBuilder : Game
{
    private readonly Vector3 _lightPos = new Vector3(1.2f, 1.0f, 2.0f);
    protected List<Mesh> _cubes = [];
    
    private Stopwatch? _timer;
    
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
        
        var cubeShader = new Shader("res/shaders/lit.vert", "res/shaders/lit.frag");
        var cubeMat = new Material(cubeShader);
        
        for (int i = 0; i < 50; i++)
        {
            var cubeMesh = new CubeMesh();
            Vector3 spawnPos = new Vector3();
            spawnPos.X = (float)Random.Shared.NextDouble() * 20f - 10f;
            spawnPos.Y = (float)Random.Shared.NextDouble() * 20f - 10f;
            spawnPos.Z = (float)Random.Shared.NextDouble() * 20f - 10f;
            cubeMesh.Transform.Position = spawnPos;

            Vector3 spawnRot = new Vector3();
            spawnRot.X = MathHelper.DegreesToRadians(-90f);
            spawnRot.Z = MathHelper.DegreesToRadians((float)Random.Shared.NextDouble() * 360f);
            cubeMesh.Transform.Rotation = Rotator.FromEuler(spawnRot).GetQuaternion();
            
            cubeMesh.SetMaterial(0, cubeMat);
            _cubes.Add(cubeMesh);
        }
        
        // _camera = new Camera();
        
        _timer = new Stopwatch();
        _timer.Start();
        
        // _texture1 = new Texture("res/textures/container.jpg");
        // _texture2 = new Texture("res/textures/awesomeface.png");
        // _shader.SetInt("texture1", 0);
        // _shader.SetInt("texture2", 1);
    }

    public override void OnRender()
    {
        // _texture1.Use(TextureUnit.Texture0);
        // _texture2.Use(TextureUnit.Texture1);
        
        Matrix4 view = MainCamera.ViewMatrix();
        Matrix4 projection = MainCamera.ProjectionMatrix(Window.Size);
        
        foreach (var cube in _cubes)
        {
            Matrix4 transform = cube.Transform.GetModelMatrix();
            cube.GetMaterial(0).Shader.SetVec3("objectColor", new Vector3(1f, .5f, .31f));
            cube.GetMaterial(0).Shader.SetVec3("lightColor", new Vector3(1f, 1f, 1f));
            cube.GetMaterial(0).Shader.SetVec3("lightPos", new Vector3(1000f, 1000f, 1000f));
            cube.GetMaterial(0).Shader.SetVec3("viewPos", MainCamera.Transform.Position);
            cube.GetMaterial(0).Shader.SetMatrix4("model", ref transform);
            cube.GetMaterial(0).Shader.SetMatrix4("view", ref view);
            cube.GetMaterial(0).Shader.SetMatrix4("projection", ref projection);
            cube.Render();
        }
    }

    public override void OnUpdate()
    {
        KeyboardState? input = Window.KeyboardState;
        
        if(Window.IsFocused)
            MainCamera.OnUpdateFrame(input, Window.MousePosition);
        
        if (input.IsKeyPressed(Keys.Escape)) 
            Window.Close();
        
        if (input.IsKeyPressed(Keys.F11)) 
            Window.WindowState = Window.WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;
    }
}