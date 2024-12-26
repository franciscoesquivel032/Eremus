using Godot;
using System;
using System.Diagnostics;

/// <summary>
/// 
/// Camera that handles camera rotation
/// 
/// </summary>
public partial class CameraRotationHandler : Node3D, ICameraHandler
{

    // SpringArm reference
    private Node3D _camera;

    // Rotation sensitivity
    private float _rotationSensitivity;

    /// <summary>
    ///  Settings default values initialization 
    ///  Future refactor incoming { settings = _resourceSettings }
    /// </summary>
    public void Init()
    {
        _camera = CameraManager.Instance.Camera;

        CameraSettings settings = CameraManager.Instance.Settings;
        _rotationSensitivity = settings.RotationSensitivity;
    }

	 public void RotateCamera(float deltaX)
    {
        // Rotate SpringArm3D around Y axis
        _camera.RotateY(Mathf.DegToRad(-deltaX * _rotationSensitivity));
    }

    // TEMP
    // Currently unused.
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