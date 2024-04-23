using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace GLEngine.RenderCore;
public class Renderer
{
    public void Render(World world, Camera camera)
    {
        
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