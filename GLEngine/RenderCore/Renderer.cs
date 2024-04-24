using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace GLEngine.RenderCore;
public class Renderer
{
    public void Render(World world, Camera camera)
    {
        foreach (Actor actor in world.GetAllActors())
            actor.Render(this, camera);
    }

    public void RenderModel(ModelAsset model, Transform transform, Camera camera)
    {
        if (model.Mesh is null)
            throw new Exception("Failed to render invalid mesh");
        
        Matrix4 viewMatrix = camera.ViewMatrix();
        Matrix4 projectionMatrix = camera.ProjectionMatrix(Game.Instance.Window.Size);
        Matrix4 modelMatrix = transform.GetModelMatrix();
        
        model.Mesh.GetMaterial(0).Shader.SetVec3("objectColor", new Vector3(1f, .5f, .31f));
        model.Mesh.GetMaterial(0).Shader.SetVec3("lightColor", new Vector3(1f, 1f, 1f));
        model.Mesh.GetMaterial(0).Shader.SetVec3("lightPos", new Vector3(1000f, 1000f, 1000f));
        model.Mesh.GetMaterial(0).Shader.SetVec3("viewPos", Camera.Main.Transform.Position);
        
        model.Mesh.GetMaterial(0).Shader.SetMatrix4("model", ref modelMatrix);
        model.Mesh.GetMaterial(0).Shader.SetMatrix4("view", ref viewMatrix);
        model.Mesh.GetMaterial(0).Shader.SetMatrix4("projection", ref projectionMatrix);
        model.Mesh.Render();
    }
    
    public void SetDepthTestEnabled(bool newEnable)
    {
        if (newEnable)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
        }
        else
        {
            GL.Disable(EnableCap.DepthTest);
        }
    }

    public void ClearScreen(Color4 clearColor)
    {
        GL.ClearColor(clearColor.R, clearColor.G, clearColor.B, clearColor.A);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }

    public void SetViewportSize(Vector2i size)
    {
        GL.Viewport(0, 0, size.X, size.Y);
    }
}