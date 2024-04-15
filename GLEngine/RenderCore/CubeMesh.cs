using OpenTK.Mathematics;

namespace GLEngine.RenderCore;

public class CubeMesh : Mesh
{
    public CubeMesh()
    {
        List<Vector3> vertices = [];
        List<uint> triangles = [];
        List<Vector2> uvs = [];
        List<Vector3> vertexColors = [];

        uint side = 0;
        // Front Face
        vertices.Add(new Vector3(0.5f, 0f, 1f));
        uvs.Add(new Vector2(0f, 0f));
        vertices.Add(new Vector3(0.5f, 0f, 0f));
        uvs.Add(new Vector2(1f, 0f));
        vertices.Add(new Vector3(0.5f, 1f, 1f));
        uvs.Add(new Vector2(0f, 1f));
        vertices.Add(new Vector3(0.5f, 1f, 0f));
        uvs.Add(new Vector2(1f, 1f));
        triangles.Add(side * 4 + 0);
        triangles.Add(side * 4 + 1);
        triangles.Add(side * 4 + 3);
        triangles.Add(side * 4 + 0);
        triangles.Add(side * 4 + 3);
        triangles.Add(side * 4 + 2);
        side++;
        
        // Back Face
        vertices.Add(new Vector3(-0.5f, 0f, 0f));
        uvs.Add(new Vector2(0f, 0f));
        vertices.Add(new Vector3(-0.5f, 0f, 1f));
        uvs.Add(new Vector2(1f, 0f));
        vertices.Add(new Vector3(-0.5f, 1f, 0f));
        uvs.Add(new Vector2(0f, 1f));
        vertices.Add(new Vector3(-0.5f, 1f, 1f));
        uvs.Add(new Vector2(1f, 1f));
        triangles.Add(side * 4 + 0);
        triangles.Add(side * 4 + 1);
        triangles.Add(side * 4 + 3);
        triangles.Add(side * 4 + 0);
        triangles.Add(side * 4 + 3);
        triangles.Add(side * 4 + 2);
        side++;
        
        // Left Face
        vertices.Add(new Vector3(0f, 0f, -0.5f));
        uvs.Add(new Vector2(0f, 0f));
        vertices.Add(new Vector3(1f, 0f, -0.5f));
        uvs.Add(new Vector2(1f, 0f));
        vertices.Add(new Vector3(0f, 1f, -0.5f));
        uvs.Add(new Vector2(0f, 1f));
        vertices.Add(new Vector3(1f, 1f, -0.5f));
        uvs.Add(new Vector2(1f, 1f));
        triangles.Add(side * 4 + 0);
        triangles.Add(side * 4 + 1);
        triangles.Add(side * 4 + 3);
        triangles.Add(side * 4 + 0);
        triangles.Add(side * 4 + 3);
        triangles.Add(side * 4 + 2);
        side++;
        
        CreateMeshSection(0, vertices.ToArray(), triangles.ToArray(), uvs.ToArray(), vertexColors.ToArray());
    }

    public override void Render()
    {
        _material.Bind();
        _vao.Bind();
        _vbo.Bind();
        _ebo.Bind();
        _ebo.Draw();
    }
}