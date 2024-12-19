using Godot;
using System;
using System.Diagnostics;

public partial class CameraMoveBase : Node3D
{
    
    // Settings
    //TODO [Resource refactor] 
    private float _cameraMoveSpeed;

    // Camera direction based on input
    private Vector3 _directionVector;

    // Flags
    private bool _cameraCanMoveBase;
    

    /// <summary>
    ///  Settings default values initialization 
    ///  Future refactor incoming { settings = _resourceSettings }
    /// </summary>
    public override void _Ready() {
        _cameraMoveSpeed = 20f;
        _directionVector = Vector3.Zero;
    } 

    /// <summary>
    /// Process movement
    /// Handles input and sets the direction based on it
    /// Then establishes position according to _directionVector value
    /// </summary>
    /// <param name="delta"></param>
    public void ProcessMovement(double delta) {
    
        SetPosition(delta);
        
    }

    /// <summary>
    /// Handles input and sets _directionVector consequently
    /// </summary>
    private void HandleInput(double delta){
        _directionVector = Vector3.Zero;

        if(Input.IsActionPressed("camera_forward"))
        _directionVector -= Transform.Basis.Z; 

		if(Input.IsActionPressed("camera_backwards"))
         _directionVector += Transform.Basis.Z; 

		if(Input.IsActionPressed("camera_right"))
         _directionVector += Transform.Basis.X; 

		if(Input.IsActionPressed("camera_left"))
         _directionVector -= Transform.Basis.X; 

         SetPosition(delta);
    }

    /// <summary>
    /// Adjusts Scene position based on direction and speed over time
    /// </summary>
    /// <param name="delta"></param>
    private void SetPosition(double delta){
        Position += _directionVector.Normalized() * _cameraMoveSpeed * (float)delta ;
    }
}