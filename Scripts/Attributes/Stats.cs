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
    public Godot.Collections.Dictionary<string, Attribute> Attributes { get; set; }

    // Utility methods
    public Attribute GetAttribute(string name) => Attributes[name];
    public void SetAttribute(string name, Attribute attribute) => Attributes[name] = attribute;
    public void SetAttributeValue(string name, float value) => Attributes[name].CurrentValue = value;
    public void SetAttributeMaxValue(string name, float value) => Attributes[name].MaxValue = value;   
}