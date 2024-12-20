using System.Collections.Generic;
using Godot;



public partial class UnitManager : Manager<UnitManager>
{
	private List<Node3D> _units;

	public List<Node3D> Units {
		get { return _units; }
		set {
			_units = value.FindAll(unit => unit is CharacterBody3D);

			if (_units.Count == 0)
				_shouldMoveToTarget = false;
		}
	}

	private Vector3 _targetPosition;
	private bool _shouldMoveToTarget;

	public override void _EnterTree()
	{
		base._EnterTree();

		GD.Print("Loading Unit Manager");

		Units = [];
		
		GD.Print("Loaded Unit Manager");
	}

	public void AddUnit(Node3D body)
	{
		GD.Print("Adding ", body, " to singleton");
		if (body is CharacterBody3D)
			Units.Add(body);
	}

	public void RemoveUnit(Node3D body)
	{
		GD.Print("Removing ", body, " from singleton");
		Units.Remove(body);
	}

	private void MoveUnitsTo(float delta)
	{
		_units.ForEach(unit => {
			unit.Position = unit.Position.MoveToward(
				_targetPosition with { Y = unit.Position.Y },
				delta);
		});
	}

	public void SetUnitsTarget(Vector3 position)
	{
		_targetPosition = position;
		_shouldMoveToTarget = true;
	}



    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		if(_shouldMoveToTarget)
			MoveUnitsTo((float) delta);
    }

}
