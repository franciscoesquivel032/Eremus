



using Godot;

public delegate void SelectedHandler();

public delegate void FocusedHandler();

/// <summary>
/// A node that allows an entity to listen for selection events from the Manager
/// </summary>
[GlobalClass]
public partial class Selectable: Area3D
{
    private MeshInstance3D _halo;

    /// <summary>
    /// This event is invoked when the entity is selected by the Manager.
    /// </summary>
    public event SelectedHandler Selected;

    /// <summary>
    /// This event is invoked when the entity is no longer selected by the Manager.
    /// </summary>
    public event SelectedHandler Deselected;

    /// <summary>
    /// This event is invoked when the entity is selected by the Manager.
    /// </summary>
    public event FocusedHandler GotFocus;

    /// <summary>
    /// This event is invoked when the entity is no longer selected by the Manager.
    /// </summary>
    public event FocusedHandler LostFocus;


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
            UnitManager.Instance.HandleSelectableEntered(this);
        };
        
        AreaExited += (value) => {
            UnitManager.Instance.HandleSelectableExited(this);
        };

        _halo = new()
        {
            Mesh = AssetManager.Instance.GetMesh("selected_secondary_halo.tres"),
            Visible = false
        };
        

        AddChild(_halo);


        GotFocus += () => {
            if (IsSelected)
            {
                // TODO: Make it static in Selectable?
                _halo.Mesh = AssetManager.Instance.GetMesh("selected_main_halo.tres");
            }
        };

        LostFocus += () => {
            if (IsSelected)
            {
                // TODO: Make it static in Selectable?
                _halo.Mesh = AssetManager.Instance.GetMesh("selected_secondary_halo.tres");
            }
        };
    }

    public void InvokeSelected()
    {
        Selected?.Invoke();
        IsSelected = true;
        _halo.Visible = true;
    }

    public void InvokeDeSelected()
    {
        Deselected?.Invoke();
        IsSelected = false;
        _halo.Visible = false;
    }

}