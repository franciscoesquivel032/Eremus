


using Godot;


/// <summary>
/// A node that allows an entity to listen for movement events from the Manager
/// </summary>
[GlobalClass]
public partial class Movable: Area3D
{
	
	private Vector3 _target;

	/// <summary>
	/// The point to reach by this entity
	/// </summary>
	[Export]
	public Vector3 Target
	{
		get => _target;
		set {
			_target = value;
			Moving = true;
		}
	}
	
	/// <summary>
	/// How much leeway is allowed to be between the entity and the target
	/// </summary>
	[Export]
	public float Tolerance { get; set; }

	/// <summary>
	/// Is the entity moving?
	/// </summary>
	[Export]
	public bool Moving { get; set; }

	/// <summary>
	/// Is the entity listening to the target updates from the Manager?
	/// </summary>
	[Export]
	public bool CanUpdateTarget { get; set; }


    public override void _EnterTree()
    {
        base._EnterTree();

		SetCollisionLayerValue(1, false);
        SetCollisionMaskValue(1, false);

        SetCollisionLayerValue((int) UnitManager.Layers.Movable, true);
        SetCollisionMaskValue((int) UnitManager.Layers.Movable, true);

		UnitManager.Instance.TargetChanged += UpdateTarget;
    }

	// TODO: Having all movable objects checking each time 
	// if the target its changed doesn't seem very efficient

	/// <summary>
	/// Updates the target if it is able
	/// </summary>
	/// <param name="target"></param>
	private void UpdateTarget(Vector3 target)
	{
		if (CanUpdateTarget) 
			Target = target;
	}

}