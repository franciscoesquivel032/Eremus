using System;
using Godot;

public partial class UnitManager : Manager<UnitManager>
{

    /// <summary>
	/// Projects 4 rect corners into space, onto a viewing plane at z distance from the given camera 
	/// projection is done using given camera's perspective projection settings 
	/// </summary>
	/// <param name="rect"></param>
	/// <param name="camera"></param>
	/// <param name="z"></param>
	/// <returns></returns>
	static Vector3[] ProjectSelection(Rect2 rect, Camera3D camera, float z)
	{
		return [
			camera.ProjectPosition(rect.Position, z),
			camera.ProjectPosition(rect.Position + new Vector2(rect.Size.X, 0.0f), z),
			camera.ProjectPosition(rect.Position + new Vector2(rect.Size.X, rect.Size.Y), z),
			camera.ProjectPosition(rect.Position + new Vector2(0.0f, rect.Size.Y), z)
		];
	}


    /// <summary>
	/// Sets the shape to be a polygon based on the current selection from the viewport
	/// </summary>
	/// <param name="shape">The shape to modify</param>
	/// <param name="rect"></param>
	/// <param name="camera"></param>
	static void ModifyFrustumCollisionMesh(Shape3D shape, Rect2 rect, Camera3D camera)
	{
		// Project 4 corners of the rect to the camera near plane
		var pnear = ProjectSelection(rect, camera, camera.Near + NEAR_FAR_MARGIN);

		// project 4 corners of the rect to the camera far plane
		var pfar = ProjectSelection(rect, camera, camera.Far - NEAR_FAR_MARGIN);

		if (shape is ConvexPolygonShape3D polygonShape)
		{
			// create a frustum mesh from 8 projected points, 6 planes, 2 triangles per plane, 3 vertices per triangle
			polygonShape.Points = [
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
			];
		} else {
			throw new Exception("The selection shape is invalid");
		}
	}

    /// <summary>
	/// Ensures that the selection is within limits and regenerates its shape
	/// </summary>
	void RedrawSelectionShape()
	{
		// Get frustum mesh and assign it as a collider and assign it to the area 3d
		Rect.Size = new(Mathf.Max(1, Rect.Size.X), Mathf.Max(1, Rect.Size.Y));

		ModifyFrustumCollisionMesh(Shape, Rect.GetRect(), CameraManager.Instance.Camera);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		switch (_selectionState)
		{
			case SelectionState.Selecting:
				{
					RedrawSelectionShape();
					
					break;
				}
			case SelectionState.LastCheck:
				{
					RedrawSelectionShape();
					
					_selectionState = SelectionState.None;
					break;
				}
		}

	}
	
}