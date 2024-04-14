using OpenTK.Graphics.OpenGL4;

namespace GLEngine.RenderCore;

public class ElementBufferObject
{
    public int Handle { get; protected set; }
    
    public ElementBufferObject()
    {
        Handle = GL.GenBuffer();
        Bind();
    }
    
    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, Handle);
    }

    public void Unbind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    }

    public void SetData(uint[] indices)
    {
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
    }
}