using Godot;



/// <summary>
/// A node that handles an entity's movement by listening from the Manager events
/// </summary>
[GlobalClass]
public partial class MovementHandler : Movable
{
    protected override void Move(double delta)
    {
        _parent.Position = _parent.Position.MoveToward(
			Target with { Y = _parent.Position.Y },
				(float) delta);

		// If the position is within tolerance, then stop moving
		if (_parent.Position.DistanceTo(Target) < Tolerance)
			Moving = false;
    }
}