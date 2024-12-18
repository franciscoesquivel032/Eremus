using Godot;
using System;
using System.Diagnostics;

//			 TODO [Camera Panning script]

/*
	--- CAMERA CONTROLL SCRIPT ---

	17 / 12 / 2024

	Camera system will have 3 Nodes.
		~ Base moves Z and X axis
		~ Camera Socket handles rotation
		~ Camera3D handles zoom in and out via Camera3D Z axis


	Currently listening to 4 basic inputs to move Camera_Base Position
	up, down, left and right being inputs ->
	W, S, A, D respectively
	
	+

	Handle zoom in and zoom out moving Camera3D Position Z axis
	being inputs ->
	mouse wheel up || numPad +
	mouse wheel down || numPad -
	
	Various settings declared as a float and serialized in the inspector as a Range [0 - 100 - 1]

	Declaring boolean flags in order to prevent unnecesaryly early return instruction for readability just in case I need them.

	GD.Prints all over the place for debugging purposes.

	tengo el pene chiquitito~~

*/


public partial class Camera : Node3D
{
	// Camera referencies
	private Camera3D _camera;

	// Camera move speed
	private float _cameraMoveSpeed = 40f;

	// Camera Zoom
	private float _cameraZoomDirection; // zoom direction

	private float _cameraZoomSpeed = 30f; // zoom speed

	private float _cameraZoomMin = 1f; // min zoom 

	private float _cameraZoomMax = 50f; // max zoom
	private float _cameraDampingSpeed = .5f;

	// Flags
	private bool _cameraCanProcess;
	private bool _cameraCanMoveBase;
	private bool _cameraCanZoom;

	public override void _Ready()
	{
 		//_springArm = GetNode<SpringArm3D>("SpringArm3D");
		_camera = GetNode<Camera3D>("SpringArm3D/Camera3D");

		_cameraCanMoveBase = true;
		_cameraCanProcess = true;
		_cameraCanZoom = true;
	}

	public override void _Process(double delta)
	{
		if(_cameraCanProcess){
			// GD.Print("Camera can process");
			CameraBaseMove(delta);
			CameraZoomUpdate(delta);

		}
	}

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

	// Base Camera move handle
	public void CameraBaseMove(double delta){

		Vector3 directionVector = Vector3.Zero;

		if(_cameraCanMoveBase)
		{
			// GD.Print("Camera can move base");
			if(Input.IsActionPressed("camera_forward")) { directionVector -= Transform.Basis.Z; }
			if(Input.IsActionPressed("camera_backwards")) { directionVector += Transform.Basis.Z; }
			if(Input.IsActionPressed("camera_right")) { directionVector += Transform.Basis.X; }
			if(Input.IsActionPressed("camera_left")) { directionVector -= Transform.Basis.X; }

			Position += directionVector.Normalized() * _cameraMoveSpeed * (float)delta ;

			// GD.Print(directionVector);
		}
	}


	// Zoom handle
	private void CameraZoomUpdate(double delta){

		float newZoom;

		if(_cameraCanZoom){

			//GD.Print("Camera can zoom");	
			
			// New Z value
			newZoom = _camera.Position.Z + _cameraZoomSpeed * _cameraZoomDirection * (float)delta;

			// Value restricted
			newZoom = Mathf.Clamp(newZoom, _cameraZoomMin, _cameraZoomMax);

			// Move Camera.Position.Z
			_camera.Position = new Vector3(_camera.Position.X, _camera.Position.Y, newZoom);

			// Smooth camera zoom stop
			_cameraZoomDirection *= _cameraDampingSpeed;

		}
	}

}
