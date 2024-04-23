using Assimp;
using OpenTK.Mathematics;

namespace GLEngine.RenderCore;

public abstract class Asset
{
    public abstract void LoadFrom(string path);
}

public class ModelAsset : Asset
{
    public Mesh? Mesh { get; protected set; }
    protected Scene? Scene;

    public override void LoadFrom(string path)
    {
        var importer = new AssimpContext();
        Scene? scene = importer.ImportFile(path, PostProcessPreset.TargetRealTimeMaximumQuality);
        
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

public class ShaderAsset : Asset
{
    public Shader? Shader;

    public override void LoadFrom(string path)
    {
        Shader = new Shader(path+".vert", path+".frag");
    }
}

public static class AssetManager
{
    private static readonly Dictionary<string, Asset> LoadedAssets = [];

    public static T Load<T>(string path) where T : Asset
    {
        if (LoadedAssets.TryGetValue(path, out Asset? value)) 
            return value as T;
        
        Asset asset = Activator.CreateInstance(typeof(T)) as Asset ?? throw new InvalidOperationException("Failed to load asset");
        asset.LoadFrom(path);
        LoadedAssets.Add(path, asset);
        return asset as T;
    }

    public static T? Get<T>(string path) where T : Asset
    {
        return LoadedAssets[path] as T;
    }
}