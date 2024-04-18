using OpenTK.Mathematics;

namespace GLEngine.RenderCore;

public struct Rotator
{
    private Quaternion _quaternion;

    public float Pitch => GetEuler().X;
    public float Yaw => GetEuler().Y;
    public float Roll => GetEuler().Z;
    
    public Rotator(float pitch, float yaw, float roll)
    {
        SetEuler(new Vector3(pitch, yaw, roll));
    }

    public Rotator(Quaternion quaternion)
    {
        _quaternion = quaternion;
    }
    
    public static Rotator FromEuler(Vector3 eulerAngles)
    {
        var rot = new Rotator();
        rot.SetEuler(eulerAngles);
        return rot;
    }

    public Rotator SetEuler(Vector3 eulerAngles)
    {
        _quaternion = Quaternion.FromEulerAngles(eulerAngles);
        return this;
    }


    public Vector3 ForwardVector()
    {
        return _quaternion * -Vector3.UnitZ;
    }

    public Vector3 RightVector()
    {
        return _quaternion * Vector3.UnitX;
    }

    public Vector3 UpVector()
    {
        return _quaternion * Vector3.UnitY;
    }

    public Vector3 GetEuler()
    {
        return _quaternion.ToEulerAngles();
    }

    public static Rotator GetLookAt(Vector3 forward)
    {
        forward = Vector3.Normalize(forward);
        Matrix4 rotMatrix = Matrix4.LookAt(Vector3.Zero, forward, Vector3.UnitY);

        return new Rotator(rotMatrix.ExtractRotation());
    }

    public Quaternion GetQuaternion()
    {
        return _quaternion;
    }
}