using OpenTK.Mathematics;

namespace GLEngine.RenderCore;

public class Transform
{
    protected Vector3 _worldPosition;
    protected Rotator _worldRotation;
    protected Vector3 _worldScale;

    protected Matrix4 _worldMatrix;
    protected bool _isDirty;

    public Transform()
    {
        _worldPosition = Vector3.Zero;
        _worldRotation = Rotator.FromEuler(Vector3.Zero);
        _worldScale = Vector3.One;
        _worldMatrix = Matrix4.Zero;
        
        MarkDirty();
    }
    
    public void SetWorldLocation(Vector3 newLocation)
    {
        _worldPosition = newLocation;
        MarkDirty();
    }

    public Vector3 GetWorldLocation()
    {
        return _worldPosition;
    }

    public void SetWorldRotation(Rotator newRotation)
    {
        _worldRotation = newRotation;
        MarkDirty();
    }

    public Rotator GetWorldRotation()
    {
        return _worldRotation;
    }

    public void SetWorldScale(Vector3 newScale)
    {
        _worldScale = newScale;
        MarkDirty();
    }

    public Vector3 GetWorldScale()
    {
        return _worldScale;
    }
    
    public Matrix4 GetMatrix()
    {
        if (!_isDirty) return _worldMatrix;
        
        var translation = Matrix4.CreateTranslation(_worldPosition);
             
        Quaternion q = Quaternion.FromEulerAngles(_worldRotation.GetEuler());
        var rotation = Matrix4.CreateFromQuaternion(q);

        var scale = Matrix4.CreateScale(_worldScale);

        _worldMatrix = translation * rotation * scale;

        _isDirty = false;

        return _worldMatrix;
    }

    public void MarkDirty()
    {
        _isDirty = true;
    }
}