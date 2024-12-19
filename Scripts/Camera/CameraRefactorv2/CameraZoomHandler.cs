using Godot;
using System;
using System.Diagnostics;

public partial class CameraZoomHandler : Node3D
{
    private Camera3D _camera; // Camera node reference

    // TODO, [Resource refactor]
	private float _cameraZoomDirection; // zoom direction
    public float CameraZoomDirection { 
        get{return _cameraZoomDirection;} 
        set {_cameraZoomDirection = value;} 
    }

	private float _cameraZoomSpeed; // zoom speed
	private float _cameraZoomMin; // min zoom 
	private float _cameraZoomMax; // max zoom
	private float _cameraZoomDampingSpeed; // zoom smooth stop


    /// <summary>
    ///  Settings initialization 
    ///  Future refactor incoming { settings = _resourceSettings }
    /// </summary>
    public override void _Ready()
    {
        _camera = GetNode<Camera3D>("../SpringArm3D/Camera3D");
        _cameraZoomSpeed = 10f;
        _cameraZoomMin = -5f;
        _cameraZoomMax = 10f;
        _cameraZoomDampingSpeed = .92f;
    }



    /// <summary>
    /// Calculates Position.Z value based on _cameraZoomSpeed and _cameraZoomDirection over time
    /// Constraints new Z to _cameraZoomMin and _cameraZoomMax values
    /// Adjusts camera Position 
    /// Softens camera zoom movement end
    /// </summary>
    /// <param name="delta"></param>
    public void Process(double delta){

		float newZoom;

			if(Mathf.Abs(_cameraZoomDirection) > 0.01){
                // New Z value
                newZoom = _camera.Position.Z + _cameraZoomSpeed * _cameraZoomDirection * (float)delta;

                // Value restricted
                newZoom = Mathf.Clamp(newZoom, _cameraZoomMin, _cameraZoomMax);
                GD.Print(newZoom);
                // Move Camera.Position.Z
                _camera.Position = new Vector3(_camera.Position.X, _camera.Position.Y, newZoom);

                // Soften camera zoom stop
                _cameraZoomDirection *= _cameraZoomDampingSpeed;

            if(Mathf.Abs(_cameraZoomDirection) < 0.01f)
                _cameraZoomDirection = 0;

            }
		
	}
}