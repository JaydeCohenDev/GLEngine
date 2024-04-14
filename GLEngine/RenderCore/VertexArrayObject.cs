using OpenTK.Graphics.OpenGL4;

namespace GLEngine.RenderCore;

public class VertexArrayObject
{
    public int Handle { get; protected set; }
    
    public VertexArrayObject()
    {
        Handle = GL.GenVertexArray();
        Bind();
    }
    
    public void Bind()
    {
        GL.BindVertexArray(Handle);
    }

    public void Unbind()
    {
        GL.BindVertexArray(0);
    }

    public void Dispose()
    {
        GL.DeleteVertexArray(Handle);
    }
}