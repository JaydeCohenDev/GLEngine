using OpenTK.Graphics.OpenGL4;

namespace GLEngine.RenderCore;

public class VertexBufferObject
{
    public int Handle { get; protected set; }
    
    public VertexBufferObject()
    {
        Handle = GL.GenBuffer();
        Bind();
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);    
    }

    public void Unbind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);    
    }

    public void SetData(float[] vertices)
    {
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(Handle);
    }
}