using Godot;

/// <summary>
/// 
///  Item data model resource
/// 
/// </summary>
public abstract partial class EntityAttribute : Resource
{

    [Export]
    public string Name { get; set; }

    [Export(PropertyHint.MultilineText)]
    public string Description { get; set; }

    [Export]
    public float BaseValue;
    
}
