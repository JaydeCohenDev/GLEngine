using OpenTK.Mathematics;

namespace GLEngine.RenderCore;

public class Mesh
{
    public Transform Transform;    
    
    protected Vector3[] _vertices = [];
    protected uint[] _triangles = [];
    protected Vector2[] _uvs = [];
    protected Vector3[] _vertexColors = [];
    protected Vector3[] _normals = [];
    
    protected VertexBufferObject _vbo;
    protected VertexArrayObject _vao;
    protected ElementBufferObject _ebo;
    protected Material? _material;
    
    public Mesh()
    {
        Transform = new Transform();
        _vbo = new VertexBufferObject();
        _vao = new VertexArrayObject();
        _ebo = new ElementBufferObject();
    }

    public void CreateMeshSection(int SectionIndex, Vector3[] vertices, uint[] triangles, Vector2[] uvs, Vector3[] vertexColors, Vector3[] normals)
    {
        _vertices = vertices;
        _triangles = triangles;
        _uvs = uvs;
        _vertexColors = vertexColors;
        _normals = normals;
        
        _vbo.Bind();
        _vbo.SetData(GenGLVertices());
        
        _vao.Bind();
        _vao.AddAttribute("aPosition", 3);
        _vao.AddAttribute("aTexCoord", 2);
        _vao.AddAttribute("aColor", 3);
        _vao.AddAttribute("aNormal", 3);
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

    protected float[] GenGLVertices()
    {
        var glVertices = new float[_vertices.Length * 11];

        for (var i = 0; i < _vertices.Length; i++)
        {
            glVertices[i * 11 + 0] = _vertices[i].X;
            glVertices[i * 11 + 1] = _vertices[i].Y;
            glVertices[i * 11 + 2] = _vertices[i].Z;
            glVertices[i * 11 + 3] = _uvs[i].X;
            glVertices[i * 11 + 4] = _uvs[i].Y;
            glVertices[i * 11 + 5] = 0f;//_vertexColors[i].X;
            glVertices[i * 11 + 6] = 0f;//_vertexColors[i].Y;
            glVertices[i * 11 + 7] = 0f; //_vertexColors[i].Z;
            glVertices[i * 11 + 8] = _normals[i].X;
            glVertices[i * 11 + 9] = _normals[i].Y;
            glVertices[i * 11 + 10] = _normals[i].Z;
        }
        
        return glVertices;
    }
}