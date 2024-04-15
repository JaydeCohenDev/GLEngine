using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GLEngine.RenderCore;

public class Camera
{
    public Matrix4 ViewMatrix
    {
        get => Matrix4.LookAt(Position, Position + Forward, Up);
        protected set { ViewMatrix = value; }
    }

    protected Vector2 _lastPos;

    public float Sensitivity = 0.25f;
    public float Speed = 1.5f;
    public Vector3 Position = new(0.0f, 0.0f,  3.0f);
    public Vector3 Forward = new(0.0f, 0.0f, -1.0f);
    public Vector3 Up = new(0.0f, 1.0f,  0.0f);
    public Vector3 Right => Vector3.Normalize(Vector3.Cross(Forward, Up));
    public float Pitch = 0f;
    public float Yaw = 0f;
    
    public Camera()
    {
    }

    public void OnUpdateFrame(FrameEventArgs e, KeyboardState input, Vector2 mousePos)
    {
        float frameSpeed = Speed * (float)e.Time;
        
        if (input.IsKeyDown(Keys.W))
            Position += Forward * frameSpeed;
        
        if (input.IsKeyDown(Keys.S))
            Position -= Forward * frameSpeed;

        if (input.IsKeyDown(Keys.A))
            Position -= Right * frameSpeed;
        
        if (input.IsKeyDown(Keys.D))
            Position += Right * frameSpeed;

        if (input.IsKeyDown(Keys.Space))
            Position += Up * frameSpeed;
        
        if (input.IsKeyDown(Keys.LeftShift))
            Position -= Up * frameSpeed;

        
        float deltaX = mousePos.X - _lastPos.X;
        float deltaY = mousePos.Y - _lastPos.Y;
        _lastPos = new Vector2(mousePos.X, mousePos.Y);

        Yaw += deltaX * Sensitivity;
        Pitch = Math.Clamp(Pitch - deltaY * Sensitivity, -89f, 89f);
        
        Forward.X = (float)Math.Cos(MathHelper.DegreesToRadians(Pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(Yaw));
        Forward.Y = (float)Math.Sin(MathHelper.DegreesToRadians(Pitch));
        Forward.Z = (float)Math.Cos(MathHelper.DegreesToRadians(Pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(Yaw));
        Forward = Vector3.Normalize(Forward);
        
    }
    
}