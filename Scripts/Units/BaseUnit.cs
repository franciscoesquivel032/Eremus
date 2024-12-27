using Godot;

/// <summary>
/// The basic unit, for now it will serve as the player
/// </summary>
[GlobalClass]
public partial class BaseUnit : CharacterBody3D /* , IInteractable */
{

	private Selectable _selectable;
	private MovementHandler _movementHandler;


	public override void _EnterTree()
	{
		base._EnterTree();

	
		_selectable = GetNode<Selectable>("Selectable");
		_movementHandler = GetNode<MovementHandler>("MovementHandler");
		
		_selectable.Selected += () => _movementHandler.CanUpdateTarget = true;
		_selectable.Deselected += () => _movementHandler.CanUpdateTarget = false;
	}

	

}
