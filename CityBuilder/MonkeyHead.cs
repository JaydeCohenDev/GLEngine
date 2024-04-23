using GLEngine.RenderCore;

namespace CityBuilder;

public class MonkeyHead : Actor
{
    public MonkeyHead()
    {
        var cubeShader = new Shader("res/shaders/lit.vert", "res/shaders/lit.frag");
        var cubeMat = new Material(cubeShader);
        
        ModelAsset modelAsset = AssetManager.LoadModel("res/models/suzanne.fbx");
        var sm = AddComponent<StaticMeshComponent>();
        sm.SetMesh(modelAsset);
        sm.SetMaterial(0, cubeMat);
    }
}