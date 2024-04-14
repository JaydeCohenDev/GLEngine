using OpenTK.Graphics.OpenGL4;

namespace GLEngine.RenderCore;

public struct VertexAttribute
{
    public string Name;
    public int NumElements;
    
    public VertexAttribute(string name, int numElements)
    {
        Name = name;
        NumElements = numElements;
    }
}

public class VertexArrayObject
{
    public int Handle { get; protected set; }

    protected List<VertexAttribute> _vertexAttributes = [];
    protected int _stride = 0;
    
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

    public void AddAttribute(string name, int numElements)
    {
        _vertexAttributes.Add(new VertexAttribute(name, numElements));
        _stride += numElements;
    }

    /**
     * Must be called after all attributes have been added.
     */
    public void MapAttributes()
    {
        var offset = 0;
        for (var i = 0; i < _vertexAttributes.Count; i++)
        {
            GL.VertexAttribPointer(i, _vertexAttributes[i].NumElements, VertexAttribPointerType.Float, false, _stride * sizeof(float), offset * sizeof(float));
            GL.EnableVertexAttribArray(i);
            offset += _vertexAttributes[i].NumElements;
        }
    }

    public void Dispose()
    {
        GL.DeleteVertexArray(Handle);
    }
}