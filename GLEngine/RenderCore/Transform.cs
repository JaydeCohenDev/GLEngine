using OpenTK.Mathematics;

namespace GLEngine.RenderCore;

public class Transform
{
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public Vector3 Scale { get; set; }

    // Additional field for local rotation
    private Quaternion LocalRotation { get; set; } // can this be combined into one world vector?

    public Transform()
    {
        Position = Vector3.Zero;
        Rotation = Quaternion.Identity;
        Scale = Vector3.One;
        LocalRotation = Quaternion.Identity;
    }

    public Transform(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
        LocalRotation = Quaternion.Identity;
    }

    public Matrix4 GetModelMatrix()
    {
        // Apply transformations in the correct order for OpenGL
        // Local rotation is applied before world rotation
        return Matrix4.CreateScale(Scale) * Matrix4.CreateFromQuaternion(LocalRotation) 
               * Matrix4.CreateFromQuaternion(Rotation) * Matrix4.CreateTranslation(Position);
    }

    public void Translate(Vector3 translation)
    {
        Position += translation;
    }

    public void Rotate(Vector3 axis, float angleInDegrees)
    {
        // Rotate around the world axis
        Rotation *= Quaternion.FromAxisAngle(axis, MathHelper.DegreesToRadians(angleInDegrees));
    }

    public void RotateLocal(Vector3 axis, float angleInDegrees)
    {
        // Rotate around the object's local axis
        LocalRotation *= Quaternion.FromAxisAngle(axis, MathHelper.DegreesToRadians(angleInDegrees));
    }

    public void LookAt(Vector3 target)
    {
        // Calculate direction vector from current position to target
        Vector3 direction = target - Position;
        direction.Normalize();

        // Calculate right vector based on world's up vector (usually Y-axis)
        Vector3 right = Vector3.Cross(Vector3.UnitY, direction);
        right.Normalize();

        // Calculate up vector using right and direction vectors
        Vector3 up = Vector3.Cross(direction, right);
        up.Normalize();

        // Create a rotation matrix based on the new right, up, and direction vectors
        Matrix4 lookAt = Matrix4.LookAt(Position, target, up);

        // Extract the quaternion representation from the lookAt matrix
        Rotation = lookAt.ExtractRotation();
    }
    
    public Vector3 GetForwardVector()
    {
        // Combine local and world rotations to get the final forward direction
        return Vector3.Transform(-Vector3.UnitZ, Rotation * LocalRotation);
    }
    
    public Vector3 GetRightVector()
    {
        // Combine local and world rotations to get the final right direction
        return Vector3.Transform(Vector3.UnitX, Rotation * LocalRotation);
    }
}
