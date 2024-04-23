using GLEngine.RenderCore;

namespace CityBuilder;

public class MonkeyHead : Actor
{
    public MonkeyHead()
    {
        Model monkeyMesh = AssetManager.LoadModel("res/models/suzanne.fbx");
        
        StaticMeshComponent sm = AddComponent<StaticMeshComponent>();
        sm.SetMesh(monkeyMesh);
    }
}