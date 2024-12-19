using Godot;
using System;
using System.Diagnostics;

public partial class CameraMovementHandler : Node3D
{
    
    // Settings
    //TODO [Resource refactor] 
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
        _cameraMoveSpeed = 20f;
        _cameraCanMoveBase = true;
        _parent = GetParent<Node3D>();
    }

    public void Process(Vector3 direction, double delta){
        if(_cameraCanMoveBase)
            _parent.Position += direction.Normalized() * _cameraMoveSpeed * (float)delta;
    }


}