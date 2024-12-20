
using System;
using Godot;

public partial class Selector : Area3D
{

	const float NEAR_FAR_MARGIN = .1f; // # frustum near/far planes distance from camera near/far planes

	private ReferenceRect _rect;
	private CollisionShape3D _shape;
	private Camera3D _camera;

	private Vector2 _firstPos;
	private Vector2 _secondPos;

	public override void _Ready()
	{
		_camera = CameraManager.Instance.Camera;
		if(_camera == null) { GD.PrintErr("Camera3D null in Script 'Selector.cs' "); }

		_shape = GetNode<CollisionShape3D>("CollisionShape3D");
		_rect = GetNode<ReferenceRect>("ReferenceRect");

		_rect.EditorOnly = false;
		_rect.Visible = false;

		
		// When a body enters or exists the selection shape, notified to the UnitManager
		BodyShapeEntered += (body_rid, body, body_shape_index, local_shape_index) => {
			UnitManager.Instance.AddUnit(body);
		};

		BodyShapeExited += (body_rid, body, body_shape_index, local_shape_index) => {
			UnitManager.Instance.RemoveUnit(body);
		};

		// So that the selection it's empty at first
		RedrawSelectionShape();
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			if (mouseEvent.IsPressed())
			{   
				_firstPos = mouseEvent.Position;
				_secondPos = mouseEvent.Position;
				_rect.Position = mouseEvent.Position;
				_rect.Size = Vector2.Zero;
				_rect.Visible = true;
				GD.Print("Starting selection");
			}
			else
			{
				_rect.Visible = false;
				GD.Print("Ending selection");
				RedrawSelectionShape();
			}
		}
		
		if (@event is InputEventMouseMotion mouseMotion && mouseMotion.ButtonMask == MouseButtonMask.Left)
		{
			_secondPos = mouseMotion.Position;
			_rect.Position = new (
				Mathf.Min(_secondPos.X, _firstPos.X),
				Mathf.Min(_secondPos.Y, _firstPos.Y)
			);
			_rect.Size = (mouseMotion.Position - _firstPos).Abs();
		}
	}

	static ConvexPolygonShape3D CreateFrustumCollisionMesh(Rect2 rect, Camera3D camera)
	{
		// Create a convex polygon collision shape
		ConvexPolygonShape3D shape = new();

		// Project 4 corners of the rect to the camera near plane
		var pnear = ProjectSelection(rect, camera, camera.Near + NEAR_FAR_MARGIN);
		
		// project 4 corners of the rext to the camera far plane
		var pfar = ProjectSelection(rect, camera, camera.Far - NEAR_FAR_MARGIN);

		// create a frustum mesh from 8 projected points, 6 planes, 2 triangles per plane, 3 vertices per triangle
		shape.Points = new Vector3[] {
			// Near plane
			pnear[0], pnear[1], pnear[2], 
			pnear[1], pnear[2], pnear[3],
			// Far plane
			pfar[2], pfar[1], pfar[0],
			pfar[2], pfar[0], pfar[3],
			// Top plane
			pnear[0], pfar[0], pfar[1],
			pnear[0], pfar[1], pnear[1],
			// Bottom plane
			pfar[2], pfar[3], pnear[3],
			pfar[2], pnear[3], pnear[2],
			// Left plane
			pnear[0], pnear[3], pfar[3],
			pnear[0], pfar[3], pfar[0],
			// Right plane
			pnear[1], pfar[1], pfar[2],
			pnear[1], pfar[2], pnear[2]
		};

		return shape;
	}

	/// <summary>
	/// Projects 4 rect corners into space, onto a viewing plane at z distance from the given camera 
	/// projection is done using given camera's perspective projection settings 
	/// </summary>
	/// <param name="rect"></param>
	/// <param name="camera"></param>
	/// <param name="z"></param>
	/// <returns></returns>
	static Vector3[] ProjectSelection(Rect2 rect, Camera3D camera, float z) {
		return new Vector3[] {
			camera.ProjectPosition(rect.Position, z),
			camera.ProjectPosition(rect.Position + new Vector2(rect.Size.X, 0.0f), z),
			camera.ProjectPosition(rect.Position + new Vector2(rect.Size.X, rect.Size.Y), z),
			camera.ProjectPosition(rect.Position + new Vector2(0.0f, rect.Size.Y), z)
		};
	}

	void RedrawSelectionShape()
	{
		// Get frustum mesh and assign it as a collider and assign it to the area 3d
		_rect.Size = new (
			Mathf.Max(1, _rect.Size.X),
			Mathf.Max(1, _rect.Size.Y)
		);

		_shape.Shape = CreateFrustumCollisionMesh(_rect.GetRect(), _camera);
	}

}
