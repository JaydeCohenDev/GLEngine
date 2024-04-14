using System.Diagnostics;
using GLEngine.RenderCore;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GLEngine;

public class Game : GameWindow
{
    // pos, col, uv
    private readonly float[] _vertices =
    [
        0.5f,  0.5f, 0.0f,    1.0f, 0.0f, 0.0f,     1.0f, 1.0f,  // top right
        0.5f, -0.5f, 0.0f,    0.0f, 1.0f, 0.0f,     1.0f, 0.0f, // bottom right
        -0.5f, -0.5f, 0.0f,    0.0f, 0.0f, 1.0f,     0.0f, 0.0f, // bottom left
        -0.5f,  0.5f, 0.0f,    0.0f, 0.0f, 0.0f,     0.0f, 1.0f, // top left
    ];
        
    private readonly float[] _cubeVertices = 
    [ 
        -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
        0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
        0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
        0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
        -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

        -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
        0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
        0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
        0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
        -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

        -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
        -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
        -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

        0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
        0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
        0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
        0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
        0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
        0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

        -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
        0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
        0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
        0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

        -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
        0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
        0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
        0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
        -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
        -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
    ];

    private uint[] _indices =
    [
        0, 1, 3,
        1, 2, 3
    ];

    private Stopwatch _timer;

    protected Camera _camera;
        
    private VertexBufferObject _vertexBufferObject;
    private VertexArrayObject _vertexArrayObject;
    private ElementBufferObject _elementBufferObject;

    private Shader _shader;

    private Texture _texture1;
    private Texture _texture2;
        
    public Game()
        : base(GameWindowSettings.Default, new NativeWindowSettings()
        {
            ClientSize = (1280, 720), Title = "OpenTK Matrices! MPW woo"
        })
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();
            
        GL.Enable(EnableCap.DepthTest);

        _camera = new Camera();
        
        _timer = new Stopwatch();
        _timer.Start();

        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        _vertexBufferObject = new VertexBufferObject();
        _vertexBufferObject.SetData(_cubeVertices);

        _vertexArrayObject = new VertexArrayObject();
        
        _vertexArrayObject.AddAttribute("aPosition", 3);
        _vertexArrayObject.AddAttribute("aTexCoord", 2);
        _vertexArrayObject.MapAttributes();

        _elementBufferObject = new ElementBufferObject();
        _elementBufferObject.SetData(_indices);
            
        _shader = new Shader("res/shaders/shader.vert", "res/shaders/shader.frag");
        _shader.Use();
            
        _texture1 = new Texture("res/textures/container.jpg");
        _texture2 = new Texture("res/textures/awesomeface.png");
        _shader.SetInt("texture1", 0);
        _shader.SetInt("texture2", 1);
        
        CursorState = CursorState.Grabbed;
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        _texture1.Use(TextureUnit.Texture0);
        _texture2.Use(TextureUnit.Texture1);
            
        _shader.Use();

        double timeValue = _timer.Elapsed.TotalSeconds;
        float greenValue = (float)Math.Sin(timeValue) / 2.0f + 0.5f;
        _shader.SetVec4("ourColor", 0f, greenValue, 0f, 1f);
        
        Matrix4 model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(timeValue * 0f));
        _shader.SetMatrix4("model", ref model);

        Matrix4 view = _camera.ViewMatrix;
        _shader.SetMatrix4("view", ref view);
        
        float fov = MathHelper.DegreesToRadians(90f);
        float aspectRatio = (float)Size.X / (float)Size.Y;
        float nearClip = 0.1f;
        float farClip = 100f;
        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(fov, aspectRatio, nearClip, farClip);
        _shader.SetMatrix4("projection", ref projection);
            
        _vertexArrayObject.Bind();
        _vertexBufferObject.Draw(_vertexArrayObject);
        
        //GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            
        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
        
        var input = KeyboardState;

        if(IsFocused)
            _camera.OnUpdateFrame(e, input, MousePosition);
        
        if (input.IsKeyDown(Keys.Escape)) Close();
        if (input.IsKeyDown(Keys.F11)) WindowState = WindowState.Fullscreen;
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, Size.X, Size.Y);
    }
        
    protected override void OnUnload()
    {
        base.OnUnload();
            
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
        GL.UseProgram(0);

        _vertexBufferObject.Dispose();
        _vertexArrayObject.Dispose();

        _shader.Dispose();
    }
}