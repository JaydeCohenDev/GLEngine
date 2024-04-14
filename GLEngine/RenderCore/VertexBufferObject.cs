using OpenTK.Graphics.OpenGL4;

namespace GLEngine.RenderCore;

public class VertexBufferObject
{
    public int Handle { get; protected set; }

    protected List<float> _vertices = [];
    
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
        _vertices = [];
        _vertices.AddRange(vertices); // save data copy for cpu access...
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
    }

    public void Draw(VertexArrayObject vao)
    {
        GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Count / vao.Stride);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(Handle);
    }
}