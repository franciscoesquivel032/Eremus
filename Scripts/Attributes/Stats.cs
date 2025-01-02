using System.Collections.Generic;
using Godot;

/// <summary>
/// 
/// Stats resource model ~
/// 
/// </summary>
public partial class Stats : Resource
{
    [Export] // regular Dictionaries cannot be exported to the editor
    public Godot.Collections.Dictionary<StatName, Attribute> Attributes { get; set; }

    // Utility methods
    public Attribute GetAttribute(StatName name) => Attributes[name];
    public void SetAttribute(StatName name, Attribute attribute) => Attributes[name] = attribute;
    public void SetAttributeValue(StatName name, float value) => Attributes[name].CurrentValue = value;
    public void SetAttributeMaxValue(StatName name, float value) => Attributes[name].MaxValue = value;  
    public void SetAttributeMinValue(StatName name, float value) => Attributes[name].MinValue = value; 

    public Stats Clone()
    {
        var clone = new Stats
        {
            Attributes = new Godot.Collections.Dictionary<StatName, Attribute>()
        };

        foreach (var attribute in Attributes)
        {
            clone.Attributes.Add(attribute.Key, attribute.Value.Clone());
        }

        return clone;
    }

}