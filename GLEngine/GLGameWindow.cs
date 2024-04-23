using GLEngine.RenderCore;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace GLEngine;

public class GLGameWindow : GameWindow
{
    private static DebugProc DebugMessageDelegate = OnDebugMessage;
    
    public Color4 BackgroundColor { get; set; } = Color4.White;

    public Action OnLoaded;
    public Action OnFrameRender;
    public Action OnUpdate;

    protected Renderer _renderer;
    
    public GLGameWindow(string gameName)
        : base(GameWindowSettings.Default, new NativeWindowSettings()
        {
            ClientSize = (1280, 720), Title = gameName,
            Flags = ContextFlags.Debug
        })
    {
        _renderer = new Renderer();
        
        int nAttributes = 0;
        GL.GetInteger(GetPName.MaxVertexAttribs, out nAttributes);
        Console.WriteLine("Maximum number of vertex attributes supported: " + nAttributes);
        
        Console.WriteLine("HELLO GLLLL BABY");
        
        GL.Enable(EnableCap.CullFace);
        
        GL.DebugMessageCallback(DebugMessageDelegate, IntPtr.Zero);
        GL.Enable(EnableCap.DebugOutput);
        
        // Optionally
        //GL.Enable(EnableCap.DebugOutputSynchronous);
    }

    private static void OnDebugMessage(
        DebugSource source, // Source of the debugging message.
        DebugType type, // Type of the debugging message.
        int id, // ID associated with the message.
        DebugSeverity severity, // Severity of the message.
        int length, // Length of the string in pMessage.
        IntPtr pMessage, // Pointer to message string.
        IntPtr pUserParam) // The pointer you gave to OpenGL, explained later.
    {
        // In order to access the string pointed to by pMessage, you can use Marshal
        // class to copy its contents to a C# string without unsafe code. You can
        // also use the new function Marshal.PtrToStringUTF8 since .NET Core 1.1.
        string message = Marshal.PtrToStringAnsi(pMessage, length);

        // The rest of the function is up to you to implement, however a debug output
        // is always useful.
        Console.WriteLine("[{0} source={1} type={2} id={3}] {4}", severity, source, type, id, message);
        
        // Potentially, you may want to throw from the function for certain severity
        // messages.
        if (type == DebugType.DebugTypeError)
        {
            throw new Exception(message);
        }
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        
        OnLoaded.Invoke(); // user loading
        
        _renderer.SetDepthTestEnabled(true);
        
        CursorState = CursorState.Grabbed;
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        _renderer.ClearScreen(BackgroundColor);

        OnFrameRender.Invoke(); // user rendering
        
        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        Time.DeltaSeconds = (float)e.Time;
        Time.ElapsedSeconds += Time.DeltaSeconds;
        
        OnUpdate.Invoke(); // user updating
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        _renderer.SetViewportSize(Size);
    }
}