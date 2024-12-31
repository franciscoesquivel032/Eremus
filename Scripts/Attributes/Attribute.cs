using Godot;

/// <summary>
/// 
///  ¡¡¡¡ Care with inheriting from Attribute if you intend to inherit from System.Attribute !!!
/// 
///  Attribute model resource
///  
/// Each attribute has a 
/// name, 
/// description, 
/// current value, 
/// max value, 
/// min value 
/// and icon
/// 
/// </summary>
public partial class Attribute : Resource
{

    [Export]
    public string Name { get; set; }

    [Export(PropertyHint.MultilineText)]
    public string Description { get; set; }

    [Export]
    public float CurrentValue { get; set; }

    [Export]
    public float MaxValue { get; set; }

    [Export]
    public float MinValue { get; set; }

    [Export]
    public Texture2D Icon { get; set; }

    /// <summary>
    /// Clones the attribute and returns a new instance
    /// Used to avoid reference issues
    /// </summary>
    /// <returns></returns>
    public Attribute Clone()
    {

        var clone = new Attribute
        {
            Name = this.Name,
            Description = this.Description,
            CurrentValue = this.CurrentValue,
            MaxValue = this.MaxValue,
            MinValue = this.MinValue,
            Icon = this.Icon
        };

        return clone;
    }
    
}
