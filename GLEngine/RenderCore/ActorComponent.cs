namespace GLEngine.RenderCore;

public class ActorComponent
{
    public Transform RelativeTransform { get; set; } = new();
    public uint Id { get; set; }
    public World? World { get; set; }
    public Actor? Owner { get; set; }
    
    public virtual void BeginPlay() {}
    public virtual void Render(Renderer renderer, Camera camera) {}
    public virtual void Update() {}
    public virtual void EndPlay() {}
}