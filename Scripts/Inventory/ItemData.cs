using Godot;

/// <summary>
/// 
///  Item data model resource
/// 
/// </summary>
public partial class ItemData : Resource
{
    [Export]
    public string Id { get; set; }

    [Export]
    public ItemType Type;

    [Export]
    public string Name { get; set; }

    [Export(PropertyHint.MultilineText)]
    public string Description { get; set; }

    [Export]
    public int Weight { get; set; }

    [Export]
    public Mesh Visuals { get; set; }

    [Export]
    public ItemAction UseAction { get; set; }
}
