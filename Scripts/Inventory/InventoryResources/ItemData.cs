using Godot;

/// <summary>
/// 
///  Item data model resource
/// 
/// </summary>

[GlobalClass]
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

    /// <summary>
    /// Default constructor
    /// </summary>
    public ItemData()
    {
        Id = "0";
        Name = "DefaultItem";
        Description = "Item description";
        Weight = 1;
        Visuals = null;
        UseAction = null;
    }

    /// <summary>
    /// Constructor with parameters
    /// </summary>
    /// <param name="item"></param>
    public ItemData(ItemData item)
    {
        Id = item.Id;
        Name = item.Name;
        Description = item.Description;
        Weight = item.Weight;
        Visuals = item.Visuals;
        UseAction = item.UseAction;
    }

    /// <summary>
    /// Clones the item and returns a new instance
    /// </summary>
    /// <returns></returns>
    public ItemData Clone() => new ItemData(this);
    
}
