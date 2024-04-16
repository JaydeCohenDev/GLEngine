using Assimp;
using OpenTK.Mathematics;

namespace GLEngine.RenderCore;

public class CubeMesh : Mesh
{
    public CubeMesh()
    {
        Model? model = AssetManager.LoadModel("res/models/suzanne_smooth.fbx");
        if (model is null) throw new Exception("Unable to load unit_cube.fbx!");
        
        Assimp.Mesh mesh = model.Scene.Meshes[0];
        
        // Load vertices
        var vertices = new Vector3[mesh.Vertices.Count];
        for (var i = 0; i < mesh.Vertices.Count; i++)
            vertices[i] = new Vector3(mesh.Vertices[i].X, mesh.Vertices[i].Y, mesh.Vertices[i].Z);

        var texCoords = mesh.TextureCoordinateChannels[0];
        var uvs = new Vector2[texCoords.Count];
        for (var i = 0; i < texCoords.Count; i++)
            uvs[i] = new Vector2(texCoords[i].X, texCoords[i].Y);

        var normals = new Vector3[mesh.Normals.Count];
        for (var i = 0; i < mesh.Normals.Count; i++)
            normals[i] = new Vector3(mesh.Normals[i].X, mesh.Normals[i].Y, mesh.Normals[i].Z);
        
        CreateMeshSection(0, vertices, mesh.GetUnsignedIndices(), uvs, [], normals);
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