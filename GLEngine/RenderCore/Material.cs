namespace GLEngine.RenderCore;

public class Material
{
    public Shader Shader { get; protected set; }
    protected Dictionary<string, Uniform> _uniforms;
    protected List<Texture> _textures;

    public Material(Shader shader)
    {
        Shader = shader;
        _uniforms = [];
        _textures = [];
    }

    public void AddTexture(Texture texture)
    {
        _textures.Add(texture);
    }

    public void SetTexture(Texture texture, int index)
    {
        _textures.Insert(index, texture);
    }

    public void SetParameter<T>(string paramName, T param) where T : Uniform
    {
        _uniforms[paramName] = param;
    }

    public Uniform GetParameter(string paramName)
    {
        return _uniforms[paramName];
    }

    public void Bind()
    {
        Shader.Use();   
    }
}