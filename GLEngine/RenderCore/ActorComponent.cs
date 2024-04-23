namespace GLEngine.RenderCore;

public class ActorComponent
{
    public Transform Transform { get; set; } = new();
    public uint Id { get; set; }
    public World? World { get; set; }
    public Actor? Owner { get; set; }
    
    public virtual void BeginPlay() {}
    public virtual void Render() {}
    public virtual void Update() {}
    public virtual void EndPlay() {}
}