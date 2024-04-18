using OpenTK.Mathematics;

namespace GLEngine.RenderCore;

public struct Rotator
{
    private Quaternion _quaternion;
    
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
}