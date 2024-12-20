





using Godot;

public partial class Selector : Area3D
{


    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        
    }


    private void StartSelection(InputEventMouseButton mouseButtonEvent)
    {
		GD.Print("Starting selection");

        _firstPos = mouseButtonEvent.Position;
		_secondPos = mouseButtonEvent.Position;
		_rect.Position = mouseButtonEvent.Position;
		_rect.Size = Vector2.Zero;
		_rect.Visible = true;
    }

    private void EndSelection()
    {
		GD.Print("Ending selection");
        
        _rect.Visible = false;
		RedrawSelectionShape();
    }

    private void MoveSelection(InputEventMouseMotion mouseMotionEvent)
    {
        _secondPos = mouseMotionEvent.Position;
		_rect.Position = new (
			Mathf.Min(_secondPos.X, _firstPos.X),
			Mathf.Min(_secondPos.Y, _firstPos.Y)
		);
		_rect.Size = (mouseMotionEvent.Position - _firstPos).Abs();
    }

    private void HandleSelection(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			if (mouseEvent.IsPressed())
                StartSelection(mouseEvent);
			else
				EndSelection();
		}
		
		if (@event is InputEventMouseMotion mouseMotion && mouseMotion.ButtonMask == MouseButtonMask.Left)
            MoveSelection(mouseMotion);

        
    }

}