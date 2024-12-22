using Godot;
using System;
using System.Diagnostics;

/// <summary>
/// 
/// Camera that handles Camera zoom 
/// Currently movement in Y axis
/// 
/// </summary>

public partial class CameraZoomHandler : Node3D, IInitializer
{
    private Node3D _parent; // Parent node reference

    // fields obtained from Manager
	private float _cameraZoomSpeed; // zoom speed
	private float _cameraZoomMin; // min zoom 
	private float _cameraZoomMax; // max zoom
    private float _cameraZoomStep; // distance traveled per input


    /// <summary>
    ///  Settings initialization 
    ///  Future refactor incoming { settings = _resourceSettings }
    /// </summary>
    public override void _Ready()
    {
        InitReferences();
    }

     public void InitReferences()
    {
        _parent = GetParent<Node3D>();

        // Get settings
        CameraSettings settings = CameraManager.Instance.Settings;

        _cameraZoomSpeed = settings.CameraZoomSpeed;
        _cameraZoomMin = settings.CameraZoomMin;
        _cameraZoomMax = settings.CameraZoomMax;
        _cameraZoomStep = settings.CameraZoomStep;
    }


    /// <summary>
    /// Calculates Position.Y value based on _cameraZoomSpeed and _cameraZoomDirection
    /// Constraints new Z to _cameraZoomMin and _cameraZoomMax values
    /// Adjusts parent Position.Y on input 
    /// </summary>
    /// <param name="delta"></param>
    public void Process(float direction){

		// Calculate new Y based on direction and zoom step setting
        float newY = _parent.Position.Y + _cameraZoomStep * direction;

        // Constraint new Y value
        newY = Mathf.Clamp(newY, _cameraZoomMin, _cameraZoomMax);

        // Update position
        _parent.Position = new Vector3(_parent.Position.X, newY, _parent.Position.Z);
	}

   

}