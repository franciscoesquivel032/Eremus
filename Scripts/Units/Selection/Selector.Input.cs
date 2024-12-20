using Godot;

public partial class Selector : Area3D
{
    
    private void StartSelection(InputEventMouseButton mouseButtonEvent)
    {
        GD.Print("Starting selection");

        _firstPos = mouseButtonEvent.Position;
        _secondPos = mouseButtonEvent.Position;
        _rect.Position = mouseButtonEvent.Position;
        _rect.Size = Vector2.Zero;
        _rect.Visible = true;
        _selectionState = SelectionState.Selecting;
    }

    private void EndSelection()
    {
        GD.Print("Ending selection");

        _rect.Visible = false;
        _selectionState = SelectionState.LastCheck;
    }

    private void MoveSelection(InputEventMouseMotion mouseMotionEvent)
    {
        _secondPos = mouseMotionEvent.Position;
        _rect.Position = new(
            Mathf.Min(_secondPos.X, _firstPos.X),
            Mathf.Min(_secondPos.Y, _firstPos.Y)
        );
        _rect.Size = (mouseMotionEvent.Position - _firstPos).Abs();
    }

    private void HandleSelection(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            switch (mouseEvent.ButtonIndex)
            {
                case MouseButton.Left:
                    {
                        if (mouseEvent.IsPressed())
                            StartSelection(mouseEvent);
                        else
                            EndSelection();
                        break;
                    }
                case MouseButton.Right:
                    {
                        if (mouseEvent.IsPressed())
                        {
                            var target = MouseToWorldPosition(CameraManager.Instance.Camera, mouseEvent);
                            UnitManager.Instance.SetUnitsTarget(target);
                        }
                        break;
                    }
            }
        }

        if (@event is InputEventMouseMotion mouseMotion && mouseMotion.ButtonMask == MouseButtonMask.Left)
            MoveSelection(mouseMotion);
    }

    private Vector3 MouseToWorldPosition(Camera3D camera, InputEventMouse mouseEvent)
    {
        _mouseQuery.From = camera.ProjectRayOrigin(mouseEvent.Position);
        _mouseQuery.To = _mouseQuery.From + camera.ProjectRayNormal(mouseEvent.Position) * MOUSE_QUERY_RAY_LENGTH;

        var collision = GetWorld3D().DirectSpaceState.IntersectRay(_mouseQuery);

        return collision.TryGetValue("position", out Variant pos) ? (Vector3) pos : Vector3.Zero;
    }

}