using Godot;

[ManagerOrder(1)]
public partial class CameraManager : Manager<CameraManager>
{
    // Settings
    private CameraSettings _settings;
    public CameraSettings Settings => _settings;

    // Camera parent reference
    private Node3D _cameraParent;
    public Node3D cameraParent => _cameraParent;

    // Camera reference
    private Camera3D _camera;
    public Camera3D Camera => _camera;

    // CameraMovementHandler reference
    private CameraMovementHandler _movementHandler;
    public CameraMovementHandler MovementHandler => _movementHandler;

    // CameraRotationHandler reference
    private CameraRotationHandler _rotationHandler;
    public CameraRotationHandler RotationHandler => _rotationHandler;

    // CameraZoomHandler reference
    private CameraZoomHandler _zoomHandler;
    public CameraZoomHandler ZoomHandler => _zoomHandler;

    /// <summary>
	///  Length of the ray to be cast from the viewport to calculate mouse world position
	/// </summary>
	private const float MOUSE_QUERY_RAY_LENGTH = 100f;

	private PhysicsRayQueryParameters3D _mouseQuery;

    public override void _EnterTree()
    {
        base._EnterTree();

        Prints.Loading(this);

        // Load settings from Resources folder
        _settings = GD.Load<CameraSettings>("res://Data(Resources)/CameraSettings.tres");
        _ = _settings ?? throw new ResourceLoadException("Settings Resource unable to load...");

        // Initialize Camera in the scene
        InitCamera();

        _mouseQuery = new();

        // OK flag
        Prints.ResourceLoadSuccessfully(this);
        Prints.Loaded(this);

        OnManagerReady();
    }

    /// <summary>
    /// Initializes the Camera in the scene
    /// Creates and adds Camera, Camera3D, CameraMovementHandler, CameraRotationHandler, CameraZoomHandler and CameraInputHandler nodes
    /// </summary>
    private void InitCamera()
    {
         // Create and add Camera node
        _cameraParent = new Node3D();
        _cameraParent.Name = "Camera";
        _cameraParent.Position = new Vector3(0, 6, 0);

        // Create and add Camera3D node
        _camera = new Camera3D();
        _camera.Name = "Camera3D";
        _cameraParent.AddChild(_camera);
        _camera.RotationDegrees = new Vector3(-45, 0, 0);

        // Create and add CameraMovementHandler node
        _movementHandler = AddHandler<CameraMovementHandler>("CameraMovementHandler");

        // Create and add CameraRotationHandler node
        _rotationHandler = AddHandler<CameraRotationHandler>("CameraRotationHandler");

        // Create and add CameraZoomHandler node
        _zoomHandler = AddHandler<CameraZoomHandler>("CameraZoomHandler");

        // Create and add CameraInputHandler node
        AddHandler<CameraInputHandler>("CameraInputHandler");

        // Add the cameraParent to the CameraManager
        AddChild(_cameraParent);
    }

    private T AddHandler<T>(string name) where T : Node, ICameraHandler, new()
    {
        var handler = new T();
        handler.Name = name;
        _cameraParent.AddChild(handler);
        handler.Init();

        return handler;
    }

    /// <summary>
	/// Gives the position of the mouse in world space from the viewport
	/// </summary>
	/// <returns></returns>
	public Vector3 GetMouseWorldPosition()
	{
		var mousePos = GetViewport().GetMousePosition();
		_mouseQuery.From = Camera.ProjectRayOrigin(mousePos);
		_mouseQuery.To = _mouseQuery.From + Camera.ProjectRayNormal(mousePos) * MOUSE_QUERY_RAY_LENGTH;

		var collision = Camera.GetWorld3D().DirectSpaceState.IntersectRay(_mouseQuery);

		return collision.TryGetValue("position", out Variant pos) ? (Vector3)pos : Vector3.Zero;
	}
}