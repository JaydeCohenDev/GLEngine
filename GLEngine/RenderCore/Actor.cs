namespace GLEngine.RenderCore;

public class Actor
{
    public Transform Transform { get; set; } = new();
    public uint Id { get; set; }
    public World? World { get; set; }

    protected uint _nextComponentId = 1;
    protected readonly Dictionary<uint, ActorComponent> _components = [];

    public virtual void BeginPlay() {}
    public virtual void EndPlay() {}
    
    public ActorComponent AddComponent(ActorComponent component)
    {
        component.Id = _nextComponentId;
        _nextComponentId++;
        
        _components.Add(component.Id, component);
        component.Owner = this;
        component.World = World;
        component.BeginPlay();
        return component;
    }

    public T AddComponent<T>() where T : ActorComponent
    {
        if (Activator.CreateInstance(typeof(T)) is not T component)
        {
            throw new Exception("ActorComponent was unable to be created.");
        }

        AddComponent(component);
        return component;
    }

    public void RemoveComponent(ActorComponent component)
    {
        component.EndPlay();
        component.World = null;
        _components.Remove(component.Id);
    }

    public void Render(Renderer renderer, Camera camera)
    {
        foreach(KeyValuePair<uint, ActorComponent> component in _components)
            component.Value.Render(renderer, camera);
    }
}