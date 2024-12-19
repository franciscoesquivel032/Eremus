using Godot;
using System;

/// <summary>
/// 
/// ::: Fran :::
/// 19 / 12 / 2024
/// _______________
/// 
/// Camera Manager Singleton which handles settings and has access to the entire Camera System
/// Attached currently to parent Camera node
/// 
/// May the force be with you ~
/// _______________
/// 
/// </summary>

public partial class CameraManager : Node3D
{
	// Singleton
	private static CameraManager _instance; // private field
	public static CameraManager Instance  // public property
	{
		get
		{
			// null instance flag
			if(_instance == null)
				GD.Print("CameraManager instance not initialized...");

			return _instance;
		}
	}

	// Settings
	private CameraSettings _settings;
	public CameraSettings Settings { get { return _settings; } }

	/// <summary>
	/// EnterTree is called when the node is added to the node tree, before it is completly initialized.
	/// This way I ensure settings are loaded and the Singleton instance is prepared before any other Node can refeer it.
	/// </summary>
    public override void _EnterTree()
    {
        base._EnterTree();
		
		if(_instance != null)
		{
			// multiple instances flag
			GD.PrintErr("Multiple instances of CameraManager...");
			return;
		}

		// Ensure this object has one singular instance
		_instance = this;


		// Load settings from Resources folder
		_settings = GD.Load<CameraSettings> ("res://Data(Resources)/CameraSettings.tres");
    }

}
