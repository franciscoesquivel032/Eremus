using Godot;
using System;
using System.Diagnostics;

public partial class CameraRotation : Node3D
{

    // Rotation sensitivity
    private float _rotationSensitivity;

    // X axis tilt limits
    public float _minVerticalAngle;
    public float _maxVerticalAngle;

    // Flags
    // Mouse state
    private bool _isRotating = false;

    /// <summary>
    ///  Settings default values initialization 
    ///  Future refactor incoming { settings = _resourceSettings }
    /// </summary>
    public override void _Ready()
    {
        _rotationSensitivity = 0.1f;
        _minVerticalAngle = -45f;
        _maxVerticalAngle = 45f;
    }


    /// <summary>
    /// Handles an incoming InputEvent
    /// If right mouse button is being pressed 
    /// cursor is hidden and its position locked while performing the action
    /// Then rotate the camera according to the #direction 
    /// 
    /// #direction is determined based on the mouse position relative to the previous position (position at the last frame).
    /// </summary>
    /// <param name="event"></param>
    public override void _Input(InputEvent @event)
    {
        // Detect mouse right click
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Right)
            {
                _isRotating = mouseEvent.Pressed;

                if (_isRotating)
                {
                    // Capture cursor
					Input.MouseMode = Input.MouseModeEnum.Captured;
                }
                else
                {
                    // Show cursor
                    Input.MouseMode = Input.MouseModeEnum.Visible; 
                }
            }
        }

		if (_isRotating && @event is InputEventMouseMotion motionEvent)
        {
            Vector2 mouseDelta = motionEvent.Relative;

            // Rotate in Y and X axis
            RotateHorizontal(mouseDelta.X);
            //RotateVertical(-mouseDelta.Y);
        }

	}

	 private void RotateHorizontal(float deltaX)
    {
        // Rotate SpringArm3D around Y axis
        RotateY(Mathf.DegToRad(-deltaX * _rotationSensitivity));
    }

    private void RotateVertical(float deltaY)
    {
        // Get current rotation in X axis
        float currentRotationX = RotationDegrees.X;

        // Clamp the angle for not surpassing limits
        float newRotationX = currentRotationX + deltaY * _rotationSensitivity;
		newRotationX = Mathf.Clamp(newRotationX, _minVerticalAngle, _maxVerticalAngle);

        // Apply new rotation
        RotationDegrees = new Vector3(newRotationX, RotationDegrees.Y, RotationDegrees.Z);
    }

}