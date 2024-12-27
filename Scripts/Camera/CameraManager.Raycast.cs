using Godot;
using Godot.Collections;


public partial class CameraManager : Manager<CameraManager>
{

    /// <summary>
	/// Performs a ray cast from the viewport at the mouse position into the world
	/// </summary>
	/// <returns></returns>
	private Dictionary PerformMouseRayCast(uint collisionMask = 1)
	{
        GD.Print("Performing ray cast with collision mask: " + collisionMask);

		var mousePos = GetViewport().GetMousePosition();
		_mouseQuery.From = Camera.ProjectRayOrigin(mousePos);
		_mouseQuery.To = _mouseQuery.From + Camera.ProjectRayNormal(mousePos) * MOUSE_QUERY_RAY_LENGTH;

        _mouseQuery.CollisionMask = collisionMask;

		return Camera.GetWorld3D().DirectSpaceState.IntersectRay(_mouseQuery);
	}

    /// <summary>
	/// Gives the position of the mouse in world space from the viewport
	/// </summary>
	/// <returns></returns>
	public Vector3 GetMouseWorldPosition()
	{
		return PerformMouseRayCast().TryGetValue("position", out Variant pos) ? (Vector3)pos : Vector3.Zero;
	}

    /// <summary>
	/// Gives the hit object a ray cast from the viewport at the mouse position into the world
	/// </summary>
	/// <returns></returns>
	public CollisionObject3D GetMouseWorldHit(uint collisionMask = 1)
	{
		return PerformMouseRayCast(collisionMask).TryGetValue("collider", out Variant pos) ? (CollisionObject3D) pos : null;
	}

}