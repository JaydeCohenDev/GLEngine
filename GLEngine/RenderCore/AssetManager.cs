using Assimp;

namespace GLEngine.RenderCore;

public abstract class Asset {}

public class Model : Asset
{
    public Scene Scene;

    public Model(Scene scene)
    {
        Scene = scene;
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
        Scene? scene = importer.ImportFile(path, PostProcessPreset.TargetRealTimeMaximumQuality);

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