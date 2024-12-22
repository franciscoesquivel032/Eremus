using Godot;
using System;
using System.Runtime.CompilerServices;

/// <summary>
/// 
/// ::: Fran :::
/// 19 / 12 / 2024
/// _______________
/// 
/// Camera Manager Singleton which handles settings and has access to the entire Camera System
/// Currently enclosed to parent Camera node
/// 
/// May the force be with you ~
/// _______________
/// 
/// </summary>
public partial class CameraManager : Manager<CameraManager>, ILoader, IInitializer
{

	// Settings reference
	private CameraSettings _settings;
	public CameraSettings Settings { get { return _settings; } }


	/// <summary>
	///  Length of the ray to be cast from the viewport to calculate mouse world position
	/// </summary>
	private const float MOUSE_QUERY_RAY_LENGTH = 100f;

	private PhysicsRayQueryParameters3D _mouseQuery;


	// Camera reference
	private Camera3D _camera;
	public Camera3D Camera { get { return _camera; } }

	/// <summary>
	/// EnterTree is called when the node is added to the node tree, before it is completly initialized.
	/// This way I ensure settings are loaded and the Singleton instance is prepared before any other Node can refeer it.
	/// </summary>
	public override void _EnterTree()
	{
		base._EnterTree();

		Prints.Loading(this);

		// Load settings from Resources folder
		LoadResources();

		// Get camera reference
		InitReferences();


		_mouseQuery = new();

		GD.Print("Loaded Camera Manager");

		Prints.Loaded(this);
	}

	public void InitReferences()
	{
		_camera = GetCamera3D();
		_ = _camera ?? throw new CameraSystemNullReferenceException("Camera not initialized...");

		// OK flag
		Prints.ResourceLoadSuccessfully(this);
	}

	public void LoadResources()
	{
		_settings = GD.Load<CameraSettings>("res://Data(Resources)/CameraSettings.tres");
		_ = _settings ?? throw new ResourceLoadException("Settings Resource unable to load...");

		// OK flag
		Prints.RefsInitSuccessfully(this);
	}


	// ::: Utility methods ::: 


	/// <summary>
	/// Get child node by name
	/// </summary>
	/// <param name="nodeName"></param>
	/// <returns>  Node -> Might need a cast on call </returns>
	public Node GetNodeByName(string nodeName) => GetNodeOrNull<Node>(nodeName);

	/// <summary>
	/// Get child node by index
	/// </summary>
	/// <param name="index"></param>
	/// <returns> Node -> Might need a cast on call </returns>
	public Node GetNodeByIndex(int index) => GetChild<Node>(index);

	/// <summary>
	/// Returns child Camera3D
	/// </summary>
	/// <returns> Camera3D node </returns>
	private Camera3D GetCamera3D() => GetNode<Camera3D>("/root/World/Camera/Camera3D");
	private Node3D GetBaseMovementHandler() => GetNode<Node3D>("/root/World/Camera/BaseMovementHandler");
	private Node3D GetRotationHandler() => GetNode<Node3D>("/root/World/Camera/RotationHandler");
	private Node3D GetZoomHandler() => GetNode<Node3D>("/root/World/Camera/ZoomHandler");


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
