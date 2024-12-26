using Godot;
using System;
using System.Diagnostics;

/// <summary>
/// 
/// Class that handles camera X axis movement
/// 
/// </summary>
public partial class CameraMovementHandler : Node3D, ICameraHandler
{
    
    // Settings
    private float _cameraMoveSpeed;
    // Flags
    private bool _cameraCanMoveBase;

    // Parent reference
    private Camera3D _camera;   

    /// <summary>
    ///  Settings default values initialization 
    ///  Future refactor incoming { settings = _resourceSettings }
    /// </summary>
    public void Init() {
        CameraSettings settings = CameraManager.Instance.Settings;

        _cameraMoveSpeed = settings.CameraMoveSpeed;
        _cameraCanMoveBase = true;
        _camera = CameraManager.Instance.Camera;
    }

    /// <summary>
    /// Called from input script and passing
    /// Vector3 direction to calculate new parent position each frame;
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="delta"></param>
    public void Process(Vector3 direction, double delta){
        if(_cameraCanMoveBase)
            _camera.Position += direction.Normalized() * _cameraMoveSpeed * (float)delta;
    }



}