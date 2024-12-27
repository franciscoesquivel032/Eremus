using System;
using Godot;

public partial class UnitManager : Manager<UnitManager>
{

    private Vector2 _firstPos;
	private Vector2 _secondPos;

    public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		HandleSelection(@event);
	}

    private void StartSelection(InputEventMouseButton mouseButtonEvent)
    {
        GD.Print("Starting selection");

        _firstPos = mouseButtonEvent.Position;
        _secondPos = mouseButtonEvent.Position;
        Rect.Position = mouseButtonEvent.Position;
        Rect.Size = Vector2.Zero;
        Rect.Visible = true;
        _selectionState = SelectionState.Selecting;
    }

    private void EndSelection()
    {
        GD.Print("Ending selection");

        Rect.Visible = false;
        _selectionState = SelectionState.LastCheck;
    }

    private void MoveSelection(InputEventMouseMotion mouseMotionEvent)
    {
        _secondPos = mouseMotionEvent.Position;
        Rect.Position = new(
            Mathf.Min(_secondPos.X, _firstPos.X),
            Mathf.Min(_secondPos.Y, _firstPos.Y)
        );
        Rect.Size = (mouseMotionEvent.Position - _firstPos).Abs();
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
                        {
                            DeselectedAllUnits();
                            StartSelection(mouseEvent);
                        }
                        else
                            EndSelection();
                        break;
                    }
                case MouseButton.Right:
                    {
                        if (mouseEvent.IsPressed())
                        {
                            SetUnitsTarget(CameraManager.Instance.GetMouseWorldPosition());
                        }
                        break;
                    }
            }
        }

        if (@event is InputEventMouseMotion mouseMotion && mouseMotion.ButtonMask == MouseButtonMask.Left)
            MoveSelection(mouseMotion);
    }

    

}