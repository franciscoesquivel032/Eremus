using Godot;
using System;
using System.Diagnostics;

public partial class CameraZoom : Node3D
{

    private Node3D _parent; // Camera node in child reference

    // TODO, [Resource refactor]
	private float _cameraZoomDirection; // zoom direction
	private float _cameraZoomSpeed; // zoom speed
	private float _cameraZoomMin; // min zoom 
	private float _cameraZoomMax; // max zoom
	private float _cameraZoomDampingSpeed; // zoom smooth stop

    // flags
    private bool _cameraCanZoom;

    /// <summary>
    ///  Settings initialization 
    ///  Future refactor incoming { settings = _resourceSettings }
    /// </summary>
    public override void _Ready()
    {
        _parent = GetNode<Camera3D>("SpringArm3D/Camera3D");
        _cameraZoomSpeed = 30f;
        _cameraZoomMin = 1f;
        _cameraZoomMax = 5f;
        _cameraZoomDampingSpeed = .92f;
        _cameraCanZoom = true;
    }

	/// <summary>
	/// Handles unhadled inputs related to zoom camera control
	/// </summary>
    public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);
		if(@event.IsAction("camera_zoom_in"))
			_cameraZoomDirection = -1;
		else if(@event.IsAction("camera_zoom_out"))
			_cameraZoomDirection = 1;
			// HACK: Fix for Mac, enhance
		else if(@event is InputEventPanGesture gesture)
			_cameraZoomDirection = Mathf.RoundToInt(gesture.Delta.Y);
		
	}

    /// <summary>
    /// Calculates Position.Z value based on _cameraZoomSpeed and _cameraZoomDirection over time
    /// Constraints new Z to _cameraZoomMin and _cameraZoomMax values
    /// Adjusts camera Position 
    /// Softens camera zoom movement end
    /// </summary>
    /// <param name="delta"></param>
    private void Update(double delta){

		float newZoom;

		if(_cameraCanZoom){
			// New Z value
			newZoom = _parent.Position.Y + _cameraZoomSpeed * _cameraZoomDirection * (float)delta;

			// Value restricted
			newZoom = Mathf.Clamp(newZoom, _cameraZoomMin, _cameraZoomMax);

			// Move Camera.Position.Z
			_parent.Position = new Vector3(_parent.Position.X, newZoom ,_parent.Position.Z);

			// Smooth camera zoom stop
			_cameraZoomDirection *= _cameraZoomDampingSpeed;
		}
	}

}