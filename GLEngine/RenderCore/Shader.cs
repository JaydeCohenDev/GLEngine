using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;

namespace GLEngine.RenderCore;

public class Shader : IDisposable
{
    public int Handle { get; protected set; }
    private bool disposedValue = false;

    public Shader(string vertPath, string fragPath)
    {
        int VertexShader, FragmentShader;
        
        string vertShaderSource = File.ReadAllText(vertPath);
        string fragShaderSource = File.ReadAllText(fragPath);

        VertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(VertexShader, vertShaderSource);
        
        FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(FragmentShader, fragShaderSource);
        
        GL.CompileShader(VertexShader);
        GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int vsuccess);
        if (vsuccess == 0)
        {
            string infoLog = GL.GetShaderInfoLog(VertexShader);
            Console.WriteLine(infoLog);
        }
        
        GL.CompileShader(FragmentShader);
        GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out int fsuccess);
        if (fsuccess == 0)
        {
            string infoLog = GL.GetShaderInfoLog(FragmentShader);
            Console.WriteLine(infoLog);
        }

        Handle = GL.CreateProgram();
        
        GL.AttachShader(Handle, VertexShader);
        GL.AttachShader(Handle, FragmentShader);
        
        GL.LinkProgram(Handle);
        
        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            string infoLog = GL.GetProgramInfoLog(Handle);
            Console.WriteLine(infoLog);
        }
        
        GL.DetachShader(Handle, VertexShader);
        GL.DetachShader(Handle, FragmentShader);
        GL.DeleteShader(FragmentShader);
        GL.DeleteShader(VertexShader);
    }

    public void Use()
    {
        GL.UseProgram(Handle);
    }

    public void SetInt(string name, int value)
    {
        Use();
        int location = GL.GetUniformLocation(Handle, name);
        GL.Uniform1(location, value);
    }
    
    public void SetVec4(string name, float x, float y, float z, float w)
    {
        Use();
        int location = GL.GetUniformLocation(Handle, name);
        GL.Uniform4(location, x, y, z, w);
    }

    public void SetVec4(string name, Vector4 value)
    {
        Use();
        SetVec4(name, value.X, value.Y, value.Z, value.W);
    }
    
    public void SetVec3(string name, float x, float y, float z)
    {
        Use();
        int location = GL.GetUniformLocation(Handle, name);
        GL.Uniform3(location, x, y, z);
    }

    public void SetVec3(string name, Vector3 value)
    {
        Use();
        SetVec3(name, value.X, value.Y, value.Z);
    }

    public void SetMatrix4(string name, ref Matrix4 matrix)
    {
        Use();
        int location = GL.GetUniformLocation(Handle, name);
        GL.UniformMatrix4(location, true, ref matrix);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            GL.DeleteProgram(Handle);
            disposedValue = true;
        }
    }

    ~Shader()
    {
        if (disposedValue == false)
        {
            Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}