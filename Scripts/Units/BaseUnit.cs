using Godot;

/// <summary>
/// The basic unit, for now it will serve as the player
/// </summary>
[GlobalClass]
public partial class BaseUnit : CharacterBody3D /* , IInteractable */
{

	private Selectable _selectable;
	private Movable _movable;


	public override void _EnterTree()
	{
		base._EnterTree();

		_selectable = GetNode<Selectable>("Selectable");
		_movable = GetNode<Movable>("Movable");
		
		_selectable.Selected += () => _movable.CanUpdateTarget = true;
		_selectable.Deselected += () => _movable.CanUpdateTarget = false;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		// If the movable node is moving, then move towards its target parallel to the ground
		if (_movable.Moving)
		{
			Position = Position.MoveToward(
				_movable.Target with { Y = Position.Y },
				(float) delta);

			// If the position is within tolerance, then stop moving
			if (Position.DistanceTo(_movable.Target) < _movable.Tolerance)
				_movable.Moving = false;
		}

	}

}
