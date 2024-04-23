using GLEngine.RenderCore;

namespace CityBuilder;

public class MonkeyHead : Actor
{
    public MonkeyHead()
    {
        var shader = AssetManager.Load<ShaderAsset>("res/shaders/lit");
        var cubeMat = new Material(shader.Shader);
        
        var modelAsset = AssetManager.Load<ModelAsset>("res/models/suzanne.fbx");
        var sm = AddComponent<StaticMeshComponent>();
        sm.SetMesh(modelAsset);
        sm.SetMaterial(0, cubeMat);
    }
}