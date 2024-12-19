using Godot;
using System;
using System.Diagnostics;

/// <summary>
/// 
/// Class that handles camera X axis movement
/// 
/// </summary>
public partial class CameraMovementHandler : Node3D
{
    
    // Settings
    private float _cameraMoveSpeed;
    // Flags
    private bool _cameraCanMoveBase;

    // Parent reference
    private Node3D _parent;   

    /// <summary>
    ///  Settings default values initialization 
    ///  Future refactor incoming { settings = _resourceSettings }
    /// </summary>
    public override void _Ready() {

        CameraSettings settings = CameraManager.Instance.Settings;

        _cameraMoveSpeed = settings.CameraMoveSpeed;
        _cameraCanMoveBase = true;
        _parent = GetParent<Node3D>();
    }

    /// <summary>
    /// Called from input script and passing
    /// Vector3 direction to calculate new parent position each frame;
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="delta"></param>
    public void Process(Vector3 direction, double delta){
        if(_cameraCanMoveBase)
            _parent.Position += direction.Normalized() * _cameraMoveSpeed * (float)delta;
    }


}