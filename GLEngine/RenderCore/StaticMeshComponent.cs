using OpenTK.Mathematics;

namespace GLEngine.RenderCore;

public class StaticMeshComponent : ActorComponent
{
    public Model? Model { get; protected set; }

    public void SetMesh(Model mesh)
    {
        Model = mesh;
    }

    public override void Render()
    {
        base.Render();

        if (Model is null) return; // No model to render
        
        Matrix4 view = Camera.Main.ViewMatrix();
        Matrix4 projection = Camera.Main.ProjectionMatrix(Game.Instance.Window.Size);
        Matrix4 transform = Owner.Transform.GetModelMatrix();
        
        Model.Mesh.GetMaterial(0).Shader.SetVec3("objectColor", new Vector3(1f, .5f, .31f));
        Model.Mesh.GetMaterial(0).Shader.SetVec3("lightColor", new Vector3(1f, 1f, 1f));
        Model.Mesh.GetMaterial(0).Shader.SetVec3("lightPos", new Vector3(1000f, 1000f, 1000f));
        Model.Mesh.GetMaterial(0).Shader.SetVec3("viewPos", Camera.Main.Transform.Position);
        
        Model.Mesh.GetMaterial(0).Shader.SetMatrix4("model", ref transform);
        Model.Mesh.GetMaterial(0).Shader.SetMatrix4("view", ref view);
        Model.Mesh.GetMaterial(0).Shader.SetMatrix4("projection", ref projection);
        Model.Mesh.Render();
    }

    public void SetMaterial(int materialIndex, Material material)
    {
        Model?.Mesh.SetMaterial(materialIndex, material);
    }
}