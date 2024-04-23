

using GLEngine.RenderCore;

namespace GLEngine;

public class Game
{
    public GLGameWindow Window { get; protected set; }
    public string Name { get; protected set; }
    public FirstPersonCamera _mainFirstPersonCamera = new();

    public static Game Instance;
    
    public Game(string gameName)
    {
        Game.Instance = this;
        
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