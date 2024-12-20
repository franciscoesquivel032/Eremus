using Godot;
using System;

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
public partial class CameraManager : Manager<CameraManager>
{

	// Settings reference
	private CameraSettings _settings;
	public CameraSettings Settings { get { return _settings; } }


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

		GD.Print("Loading Camera Manager");

		// Load settings from Resources folder
		_settings = GD.Load<CameraSettings> ("res://Data(Resources)/CameraSettings.tres");
		
		// Get camera reference
		_camera = GetCamera3D();
		if(_camera == null){GD.Print("camera null");}

		GD.Print("Loaded Camera Manager");
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

}
