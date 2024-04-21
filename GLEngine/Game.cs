

using GLEngine.RenderCore;

namespace GLEngine;

public class Game
{
    public GLGameWindow Window { get; protected set; }
    public string Name { get; protected set; }
    public Camera MainCamera = new();
    
    public Game(string gameName)
    {
        Name = gameName;
        Window = new GLGameWindow(Name);

        Window.OnLoaded += OnLoad;
        Window.OnFrameRender += OnRender;
        Window.OnUpdate += OnUpdate;
    }

    public void Run()
    {
        Window.Run();
    }

    public virtual void OnLoad() {}
    public virtual void OnRender() {}
    public virtual void OnUpdate() {}
}