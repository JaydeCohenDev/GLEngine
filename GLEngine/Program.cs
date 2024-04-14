using OpenTK.Graphics.OpenGL4;

namespace GLEngine;

internal static class Program
{
    public static void Main(string[] args)
    {
        using (var game = new Game())
        {
            
            int nAttributes = 0;
            GL.GetInteger(GetPName.MaxVertexAttribs, out nAttributes);
            Console.WriteLine("Maximum number of vertex attributes supported: " + nAttributes);
            
            game.Run();
        }
    }
}