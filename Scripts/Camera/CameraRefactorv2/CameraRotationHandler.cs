using Godot;
using System;
using System.Diagnostics;

public partial class CameraRotationHandler : Node3D
{

    // SpringArm reference
    private Node3D _parent;

    // Rotation sensitivity
    private float _rotationSensitivity;
    // X axis tilt limits
    public float _minVerticalAngle;
    public float _maxVerticalAngle;

    /// <summary>
    ///  Settings default values initialization 
    ///  Future refactor incoming { settings = _resourceSettings }
    /// </summary>
    public override void _Ready()
    {
        _parent = GetParent<Node3D>();

        _rotationSensitivity = 0.1f;
        _minVerticalAngle = -45f;
        _maxVerticalAngle = 45f;
    }

	 public void RotateCamera(float deltaX)
    {
        // Rotate SpringArm3D around Y axis
        _parent.RotateY(Mathf.DegToRad(-deltaX * _rotationSensitivity));
    }

   /* private void RotateVertical(float deltaY)
    {
        // Get current rotation in X axis
        float currentRotationX = RotationDegrees.X;

        // Clamp the angle for not surpassing limits
        float newRotationX = currentRotationX + deltaY * _rotationSensitivity;
		newRotationX = Mathf.Clamp(newRotationX, _minVerticalAngle, _maxVerticalAngle);

        // Apply new rotation
        RotationDegrees = new Vector3(newRotationX, RotationDegrees.Y, RotationDegrees.Z);
    }*/

}