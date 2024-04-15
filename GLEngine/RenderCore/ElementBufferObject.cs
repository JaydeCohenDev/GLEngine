using OpenTK.Graphics.OpenGL4;

namespace GLEngine.RenderCore;

public class ElementBufferObject
{
    public int Handle { get; protected set; }
    protected uint[] _indices;
    
    public ElementBufferObject()
    {
        Handle = GL.GenBuffer();
        Bind();
    }
    
    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, Handle);
    }
    public void SetData(uint[] indices)
    {
        _indices = indices; 
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
    }

    public void Unbind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    }
    
    public void Draw()
    {
        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
    }
}