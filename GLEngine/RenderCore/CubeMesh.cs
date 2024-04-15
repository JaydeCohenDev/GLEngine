using OpenTK.Mathematics;

namespace GLEngine.RenderCore;

public class CubeMesh : Mesh
{

    struct cubeTri
    {
        public uint One;
        public uint Two;
        public uint Three;

        public cubeTri(uint one, uint two, uint three)
        {
            One = one;
            Two = two;
            Three = three;
        }
    }
    
    public CubeMesh()
    {
        List<Vector3> vertices = [];
        List<uint> triangles = [];
        List<Vector2> uvs = [];
        List<Vector3> vertexColors = [];

        float size = 1f;
        float half = size / 2f;
        
        vertices.Add(new Vector3(-1, -1, -1));
        vertices.Add(new Vector3(1, -1, -1));
        vertices.Add(new Vector3(1, 1, -1));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);
        
        vertices.Add(new Vector3(1, 1, -1));
        vertices.Add(new Vector3(-1, 1, -1));
        vertices.Add(new Vector3(-1, -1, -1));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        triangles.Add(3);
        triangles.Add(4);
        triangles.Add(5);
        
        
        vertices.Add(new Vector3(-1, -1, 1));
        vertices.Add(new Vector3(1, -1, 1));
        vertices.Add(new Vector3(1, 1, 1));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        triangles.Add(6);
        triangles.Add(7);
        triangles.Add(8);
        
        vertices.Add(new Vector3(1, 1, 1));
        vertices.Add(new Vector3(-1, 1, 1));
        vertices.Add(new Vector3(-1, -1, 1));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        triangles.Add(9);
        triangles.Add(10);
        triangles.Add(11);
        
        vertices.Add(new Vector3(-1, 1, 1));
        vertices.Add(new Vector3(-1, 1, -1));
        vertices.Add(new Vector3(-1, -1, -1));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        triangles.Add(12);
        triangles.Add(13);
        triangles.Add(14);
        
        
        vertices.Add(new Vector3(-1, -1, -1));
        vertices.Add(new Vector3(-1, -1, 1));
        vertices.Add(new Vector3(-1, 1, 1));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        triangles.Add(15);
        triangles.Add(16);
        triangles.Add(17);
        
        
        vertices.Add(new Vector3(1, 1, 1));
        vertices.Add(new Vector3(1, 1, -1));
        vertices.Add(new Vector3(1, -1, -1));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        triangles.Add(18);
        triangles.Add(19);
        triangles.Add(20);
        
        
        vertices.Add(new Vector3(1, -1, -1));
        vertices.Add(new Vector3(1, -1, 1));
        vertices.Add(new Vector3(1, 1, 1));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        triangles.Add(21);
        triangles.Add(22);
        triangles.Add(23);
        
        
        
        vertices.Add(new Vector3(-1, -1, -1));
        vertices.Add(new Vector3(1, -1, -1));
        vertices.Add(new Vector3(1, -1, 1));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        triangles.Add(24);
        triangles.Add(25);
        triangles.Add(26);
        
        
        vertices.Add(new Vector3(1, -1, 1));
        vertices.Add(new Vector3(-1, -1, 1));
        vertices.Add(new Vector3(-1, -1, -1));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        triangles.Add(27);
        triangles.Add(28);
        triangles.Add(29);
        
        
        vertices.Add(new Vector3(-1, 1, -1));
        vertices.Add(new Vector3(1, 1, -1));
        vertices.Add(new Vector3(1, 1, 1));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        triangles.Add(30);
        triangles.Add(31);
        triangles.Add(32);
        
        
        vertices.Add(new Vector3(1, 1, 1));
        vertices.Add(new Vector3(-1, 1, 1));
        vertices.Add(new Vector3(-1, 1, -1));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        triangles.Add(33);
        triangles.Add(34);
        triangles.Add(35);
        
        //uint side = 0;
        // // Front Face
        // vertices.Add(new Vector3(0.5f, 0f, 1f));
        // uvs.Add(new Vector2(0f, 0f));
        // vertices.Add(new Vector3(0.5f, 0f, 0f));
        // uvs.Add(new Vector2(1f, 0f));
        // vertices.Add(new Vector3(0.5f, 1f, 1f));
        // uvs.Add(new Vector2(0f, 1f));
        // vertices.Add(new Vector3(0.5f, 1f, 0f));
        // uvs.Add(new Vector2(1f, 1f));
        // triangles.Add(side * 4 + 0);
        // triangles.Add(side * 4 + 1);
        // triangles.Add(side * 4 + 3);
        // triangles.Add(side * 4 + 0);
        // triangles.Add(side * 4 + 3);
        // triangles.Add(side * 4 + 2);
        // side++;
        //
        // // Back Face
        // vertices.Add(new Vector3(-0.5f, 0f, 0f));
        // uvs.Add(new Vector2(0f, 0f));
        // vertices.Add(new Vector3(-0.5f, 0f, 1f));
        // uvs.Add(new Vector2(1f, 0f));
        // vertices.Add(new Vector3(-0.5f, 1f, 0f));
        // uvs.Add(new Vector2(0f, 1f));
        // vertices.Add(new Vector3(-0.5f, 1f, 1f));
        // uvs.Add(new Vector2(1f, 1f));
        // triangles.Add(side * 4 + 0);
        // triangles.Add(side * 4 + 1);
        // triangles.Add(side * 4 + 3);
        // triangles.Add(side * 4 + 0);
        // triangles.Add(side * 4 + 3);
        // triangles.Add(side * 4 + 2);
        // side++;
        //
        // // Left Face
        // vertices.Add(new Vector3(-0.5f, 0f, 1f));
        // uvs.Add(new Vector2(0f, 0f));
        // vertices.Add(new Vector3(.5f, 0f, 1f));
        // uvs.Add(new Vector2(1f, 0f));
        // vertices.Add(new Vector3(-0.5f, 1f, 1f));
        // uvs.Add(new Vector2(0f, 1f));
        // vertices.Add(new Vector3(.5f, 1f, 1f));
        // uvs.Add(new Vector2(1f, 1f));
        // triangles.Add(side * 4 + 0);
        // triangles.Add(side * 4 + 1);
        // triangles.Add(side * 4 + 3);
        // triangles.Add(side * 4 + 0);
        // triangles.Add(side * 4 + 3);
        // triangles.Add(side * 4 + 2);
        // side++;
        
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