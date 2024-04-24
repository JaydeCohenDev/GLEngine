using OpenTK.Mathematics;

namespace GLEngine.RenderCore;

public class StaticMeshComponent : ActorComponent
{
    public ModelAsset? Model { get; protected set; }

    public void SetMesh(ModelAsset mesh)
    {
        Model = mesh;
    }

    public override void Render(Renderer renderer, Camera camera)
    {
        base.Render(renderer, camera);

        if (Model is null) return;
        if (Owner is null) return;
        
        renderer.RenderModel(Model, Owner.Transform, camera);
    }

    public void SetMaterial(int materialIndex, Material material)
    {
        Model?.Mesh.SetMaterial(materialIndex, material);
    }
}