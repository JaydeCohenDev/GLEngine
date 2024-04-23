using Assimp;
using OpenTK.Mathematics;

namespace GLEngine.RenderCore;

public abstract class Asset {}

public class Model : Asset
{
    public Scene Scene;
    public Mesh Mesh { get; }

    public Model(Scene scene)
    {
        Scene = scene;
        
        Assimp.Mesh mesh = scene.Meshes[0];
        
        // Load vertices
        Vector3[] vertices = new Vector3[mesh.Vertices.Count];
        for (int i = 0; i < mesh.Vertices.Count; i++)
            vertices[i] = new Vector3(mesh.Vertices[i].X, mesh.Vertices[i].Y, mesh.Vertices[i].Z);

        List<Vector3D>? texCoords = mesh.TextureCoordinateChannels[0];
        Vector2[] uvs = new Vector2[texCoords.Count];
        for (int i = 0; i < texCoords.Count; i++)
            uvs[i] = new Vector2(texCoords[i].X, texCoords[i].Y);

        Vector3[] normals = new Vector3[mesh.Normals.Count];
        for (int i = 0; i < mesh.Normals.Count; i++)
            normals[i] = new Vector3(mesh.Normals[i].X, mesh.Normals[i].Y, mesh.Normals[i].Z);

        Mesh = new Mesh();
        Mesh.CreateMeshSection(0, vertices, mesh.GetUnsignedIndices(), uvs, [], normals);
    }
}

public static class AssetManager
{
    private static readonly Dictionary<string, Model> LoadedModels = [];
    
    
    public static Model? LoadModel(string path)
    {
        // Escape if already loaded!
        if (LoadedModels.ContainsKey(path))
            return GetModel(path); 
        
        var importer = new AssimpContext();
        Assimp.Scene? scene = importer.ImportFile(path, PostProcessPreset.TargetRealTimeMaximumQuality);

        if (scene is null) return null;
        
        LoadedModels.Add(path, new Model(scene));
        return GetModel(path);

    }

    public static Model? GetModel(string path)
    {
        LoadedModels.TryGetValue(path, out Model? value);
        return value;
    }
}