namespace GLEngine.RenderCore;

public class World
{
    protected uint _nextActorId = 1;
    protected readonly Dictionary<uint, Actor> _actors = [];

    public void Spawn(Actor actor)
    {
        actor.World = this;
        actor.Id = _nextActorId;
        _nextActorId++;
        _actors.Add(actor.Id, actor);
        actor.BeginPlay();
    }

    public void Destroy(Actor actor)
    {
        actor.EndPlay();
        actor.World = null;
        _actors.Remove(actor.Id);
    }

    public void Render()
    {
        foreach (KeyValuePair<uint, Actor> actor in _actors)
            actor.Value.Render();
    }
}