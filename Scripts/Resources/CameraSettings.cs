using Godot;

public partial class CameraSettings : Resource
{
    // Rotation settings
    [Export]
    public float RotationSensitivity { get; set; } = .1f;

    // Base movement settings
    [Export]
    public float CameraMoveSpeed { get; set; } = 20f;

    // Zoom settings
    [Export]
    public float CameraZoomSpeed { get; set; }  = 5f;
    [Export]
    public float CameraZoomMin { get; set; }  = 1f;
    [Export]
    public float CameraZoomMax { get; set; }  = 20f;
    [Export]
    public float CameraZoomStep { get; set; }  = .4f;
   
}