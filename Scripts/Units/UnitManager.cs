using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public delegate void TargetChangedHandler(Vector3 Target);

public partial class UnitManager : Manager<UnitManager>
{

	public ConvexPolygonShape3D Shape { get; private set; }
	public ReferenceRect Rect { get; private set; }

	public event TargetChangedHandler TargetChanged;
	

	[Flags]
	public enum Layers
	{
		None = 0,
		Selectable = 30,
		Movable = 31,
		Interactable = 32
	}

	private enum SelectionState
	{
		None,
		LastCheck,
		Selecting
	}

	private SelectionState _selectionState;

	/// <summary>
	/// Frustum near/far planes distance from camera near/far planes
	/// </summary>
	private const float NEAR_FAR_MARGIN = .1f;

	// These handler allow for different layers of interaction
	private Area3D _interactionHandler;
	private Area3D _selectionHandler;
	private Area3D _movementHandler;

	public override void _EnterTree()
	{
		GD.Print("Loading Unit Manager");

		Shape = new();
		Rect = new()
		{
			EditorOnly = false,
			Visible = false
		};

		AddChild(Rect);

		PrepareHandlerArea(_interactionHandler, Layers.Interactable);
		PrepareHandlerArea(_selectionHandler, Layers.Selectable);
		PrepareHandlerArea(_movementHandler, Layers.Movable);

		_selectionState = SelectionState.None;

		base._EnterTree();	
		
		GD.Print("Loaded Unit Manager");
	}

	/// <summary>
	/// Given a reference to an Area3D, it will prepare it for checking on a given layer within the selection shape
	/// </summary>
	/// <param name="handler"></param>
	/// <param name="layer"></param>
	public void PrepareHandlerArea(Area3D handler, Layers layer)
    {
		// It receives a reference to the handle to generate
        handler = new Area3D();

		// Add the area as a child of the UnitManager Instance
		AddChild(handler);

		// Set the main layer as false, since its defaulted to true
        handler.SetCollisionLayerValue(1, false);
        handler.SetCollisionMaskValue(1, false);

		// Set the layer this Area should handle
		handler.SetCollisionLayerValue((int) layer, true);
        handler.SetCollisionMaskValue((int) layer, true);

		// Create a collision shape that shares the same internal shape as the rest of the areas
		// Since its being recalculated each time the user selects, its cheaper this way
        var colShape = new CollisionShape3D
        {
            Shape = Shape
        };

		// Add the collision shape to the area
        handler.AddChild(colShape);
    }

	/// <summary>
	/// Notifies all movable units that a new target has been set
	/// </summary>
	/// <param name="position"></param>
	public void SetUnitsTarget(Vector3 position)
	{
		GD.Print("Asking units to move");
		TargetChanged?.Invoke(position);
	}

}
