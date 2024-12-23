



using Godot;

public delegate void SelectedHandler();

/// <summary>
/// A node that allows an entity to listen for selection events from the Manager
/// </summary>
[GlobalClass]
public partial class Selectable: Area3D
{

    /// <summary>
    /// This event is invoked when the entity is selected by the Manager.
    /// </summary>
    public event SelectedHandler Selected;

    /// <summary>
    /// This event is invoked when the entity is no longer selected by the Manager.
    /// </summary>
    public event SelectedHandler Deselected;


    /// <summary>
    /// Is the entity selected right now by the Manager?
    /// </summary>
    [Export]
    public bool IsSelected { get; set; }

    

    public override void _EnterTree()
    {
        base._EnterTree();

        SetCollisionLayerValue(1, false);
        SetCollisionMaskValue(1, false);

        // TODO: Create more layers? Since another selectable could fire false positives
        SetCollisionLayerValue((int) UnitManager.Layers.Selectable, true);
        SetCollisionMaskValue((int) UnitManager.Layers.Selectable, true);

        AreaEntered += (value) => {
            IsSelected = true;
            Selected?.Invoke();
        };
        
        AreaExited += (value) => {
            IsSelected = false;
            Deselected?.Invoke();
        };
    }

}