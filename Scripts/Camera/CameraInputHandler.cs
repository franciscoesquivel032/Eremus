using Godot;

/// <summary>
/// 
/// Class that handles Camera system inputs
/// 
/// </summary>

public partial class CameraInputHandler : Node3D, ICameraHandler
{
    // Camera base movement variables
    private Vector3 _directionVector;
    private CameraMovementHandler _movementHandler;

    // Camera rotation variables
    private bool _isRotating;
    private CameraRotationHandler _rotationHandler;

    // Camera zoom variables
    private bool _cameraCanZoom;
    private CameraZoomHandler _zoomHandler;
    private float _zoomDirection;


    public void Init()
    {
        GD.Print("Init input handler");

        // Base movement
        _directionVector = Vector3.Zero;
        _movementHandler = CameraManager.Instance.MovementHandler;

        GD.Print(_movementHandler);

        // Rotation
        _isRotating = false;
        _rotationHandler = CameraManager.Instance.RotationHandler;

        // Zoom
        _cameraCanZoom = true;
        _zoomHandler = CameraManager.Instance.ZoomHandler;
        _zoomDirection = 0;
    }

    	/// <summary>
	/// Handles unhadled inputs related to zoom camera control
	/// </summary>
    public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);

	    if (@event is InputEventMouseButton mouseEvent)
    {
        // Detects scroll up (zoom in)
        if (mouseEvent.IsAction("camera_zoom_in"))
        {
            _zoomHandler.Process(-1); // Zoom in
        }
        // Detects scroll down (zoom out)
        else if (mouseEvent.IsAction("camera_zoom_out"))
        {
            _zoomHandler.Process(1); // Zoom out
        }
    }
        // Si se detecta un gesto de pan (puedes ajustar esta acción a tus necesidades)
       /* else if (@event is InputEventPanGesture gesture)
        {
            _zoomDirection = Mathf.RoundToInt(gesture.Delta.Y);
        }*/

       
	}

    public override void _Process(double delta)
    {
        base._Process(delta);
        HandleBaseMovementInput(delta);
    }

    /// <summary>
    /// Handles input and sets _directionVector consequently
    /// </summary>
    private void HandleBaseMovementInput(double delta){
        _directionVector = Vector3.Zero;

        if(Input.IsActionPressed("camera_forward"))
        _directionVector -= GlobalTransform.Basis.Z; 

		if(Input.IsActionPressed("camera_backwards"))
         _directionVector += GlobalTransform.Basis.Z; 

		if(Input.IsActionPressed("camera_right"))
         _directionVector += GlobalTransform.Basis.X; 

		if(Input.IsActionPressed("camera_left"))
         _directionVector -= GlobalTransform.Basis.X; 

         _movementHandler.Process(_directionVector, delta);
    }


    /// <summary>
    /// Handles an incoming InputEvent
    /// If right mouse button is being pressed 
    /// Then rotate the camera according to the #direction 
    /// 
    /// #direction is determined based on the mouse position relative to the previous position (position at the last frame).
    /// 
    /// ::::: (Currently Unused) ::::: 
    /// -> cursor is hidden and its position locked while performing the action
    /// 
    /// </summary>
    /// <param name="event"></param>
    public override void _Input(InputEvent @event)
    {
        // Detect mouse right click
        if (@event is InputEventMouseButton mouseEvent)
        {

            if (mouseEvent.IsAction("camera_rotate"))
            {
                _isRotating = mouseEvent.Pressed;

                /*if (_isRotating)
                {
                    // Capture cursor
					Input.MouseMode = Input.MouseModeEnum.Captured;
                }
                else
                {
                    // Show cursor
                    Input.MouseMode = Input.MouseModeEnum.Visible; 
                }*/
            }
        }

		if (_isRotating && @event is InputEventMouseMotion motionEvent)
        {
            Vector2 mouseDelta = motionEvent.Relative;

            // Rotate X axis
            _rotationHandler.RotateCamera(mouseDelta.X);
        }

	}



}