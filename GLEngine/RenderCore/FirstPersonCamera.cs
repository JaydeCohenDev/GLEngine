﻿using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GLEngine.RenderCore;

public class Camera
{
    public Transform Transform;
    public float NearClip = 0.1f;
    public float FarClip = 100f;
    public float Fov = 90f;

    public static Camera Main; // TODO REMOVE... :(
    
    public Camera()
    {
        Transform = new Transform();
        Main = this; // TODO FIXME
    }

    public Matrix4 ViewMatrix()
    {
        // Invert the model matrix to create the view matrix
        return Matrix4.Invert(Transform.GetModelMatrix());
    }

    public Matrix4 ProjectionMatrix(Vector2i WindowSize)
    {
        float fov = MathHelper.DegreesToRadians(Fov);
        float aspectRatio = (float)WindowSize.X / (float)WindowSize.Y;
        return Matrix4.CreatePerspectiveFieldOfView(fov, aspectRatio, NearClip, FarClip);
    }
}

public class FirstPersonCamera : Camera
{
    protected Vector2 _lastPos;
    public float Sensitivity = 0.25f;
    public float Speed = 1.5f;

    public void OnUpdateFrame(KeyboardState input, Vector2 mousePos)
    {
        float frameSpeed = Speed * Time.DeltaSeconds;

        if (_lastPos == Vector2.Zero)
            _lastPos = new Vector2(mousePos.X, mousePos.Y);

        if (input.IsKeyDown(Keys.W))
            Transform.Position += Transform.GetForwardVector() * frameSpeed;
        
        if (input.IsKeyDown(Keys.S))
            Transform.Position += -Transform.GetForwardVector() * frameSpeed;
        
        if (input.IsKeyDown(Keys.A))
            Transform.Position += -Transform.GetRightVector() * frameSpeed;
        
        if (input.IsKeyDown(Keys.D))
            Transform.Position += Transform.GetRightVector() * frameSpeed;
        
        if (input.IsKeyDown(Keys.Space))
            Transform.Position += Vector3.UnitY * frameSpeed;
        
        if (input.IsKeyDown(Keys.LeftShift))
            Transform.Position += -Vector3.UnitY * frameSpeed;

        float deltaX = mousePos.X - _lastPos.X;
        float deltaY = mousePos.Y - _lastPos.Y;
        _lastPos = new Vector2(mousePos.X, mousePos.Y);

        // Handle mouse input for looking around
        Transform.RotateLocal(Vector3.UnitX, -deltaY * Sensitivity);
        Transform.Rotate(Vector3.UnitY, -deltaX * Sensitivity);
    }

}