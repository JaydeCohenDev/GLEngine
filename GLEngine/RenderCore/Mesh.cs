using OpenTK.Mathematics;

namespace GLEngine.RenderCore;

public class Mesh
{
    public Vector3 Position = Vector3.Zero;
    public Vector3 Rotation = Vector3.Zero;
    public Vector3 Scale = Vector3.One;
    
    protected Vector3[] _vertices = [];
    protected uint[] _triangles = [];
    protected Vector2[] _uvs = [];
    protected Vector3[] _vertexColors = [];
    
    protected VertexBufferObject _vbo;
    protected VertexArrayObject _vao;
    protected ElementBufferObject _ebo;
    protected Material? _material;
    
    public Mesh()
    {
        _vbo = new VertexBufferObject();
        _vao = new VertexArrayObject();
        _ebo = new ElementBufferObject();
        
    }

    public void CreateMeshSection(int SectionIndex, Vector3[] vertices, uint[] triangles, Vector2[] uvs, Vector3[] vertexColors)
    {
        _vertices = vertices;
        _triangles = triangles;
        _uvs = uvs;
        _vertexColors = vertexColors;
        
        _vbo.Bind();
        _vbo.SetData(GenGLVertices());
        
        _vao.Bind();
        _vao.AddAttribute("aPosition", 3);
        _vao.AddAttribute("aTexCoord", 2);
        _vao.AddAttribute("aColor", 3);
        _vao.MapAttributes();
        
        _ebo.Bind();
        _ebo.SetData(_triangles.ToArray());
    }

    public void SetMaterial(int index, Material material)
    {
        if (index == 0)
            _material = material;
        else
            throw new NotImplementedException("Mesh material slots other than 0 are not implemented. Please try again later!");
    }

    public Material GetMaterial(int index)
    {
        return _material;
    }

    public virtual void Render()
    {
    }

    public Matrix4 GetTransformMatrix()
    {
        Matrix4 translation = Matrix4.CreateTranslation(Position);
        Quaternion q = Quaternion.FromEulerAngles(Rotation);
        Matrix4 rotation = Matrix4.CreateFromQuaternion(q);
        Matrix4 scale = Matrix4.CreateScale(Scale);

        return scale * rotation * translation;

        //Matrix4 model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(0f));
    }

    protected float[] GenGLVertices()
    {
        var glVertices = new float[_vertices.Length * 8];

        for (var i = 0; i < _vertices.Length; i++)
        {
            glVertices[i * 8 + 0] = _vertices[i].X;
            glVertices[i * 8 + 1] = _vertices[i].Y;
            glVertices[i * 8 + 2] = _vertices[i].Z;
            glVertices[i * 8 + 3] = _uvs[i].X;
            glVertices[i * 8 + 4] = _uvs[i].Y;
            glVertices[i * 8 + 5] = 0f;//_vertexColors[i].X;
            glVertices[i * 8 + 6] = 0f;//_vertexColors[i].Y;
            glVertices[i * 8 + 7] = 0f; //_vertexColors[i].Z;
        }
        
        return glVertices;
    }
}