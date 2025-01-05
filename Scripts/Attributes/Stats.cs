using System;
using System.Linq;
using System.Reflection;
using Godot;

/// <summary>
/// 
/// Stats resource model ~
/// Stores an array of Attribute
/// 
/// </summary>

[GlobalClass]
[Tool]
public partial class Stats : Resource
{
    [Export] 
    public Godot.Collections.Array<Attribute> Attributes { get; set; }

    // Utility methods
    public Attribute GetAttribute(StatName name) => Attributes.Where(a => a.Equals(name)).FirstOrDefault();
    public void SetAttribute(StatName name, Attribute attribute)
    {
        var existingAttribute = GetAttribute(name);
        if (existingAttribute != null)
        {
            Type t = typeof(Attribute);
            FieldInfo[] fields = t.GetFields();

            foreach (var field in fields)
            {
                existingAttribute.Set(field.Name, attribute.Get(field.Name));
            }
        }
    }
    public void SetAttributeValue(StatName name, float value)
    {
        var attribute = GetAttribute(name);
        if (attribute != null)
        {
            attribute.CurrentValue = value;
        }
    }
    public Stats Clone()
    {
        var clone = new Stats
        {
            Attributes = new Godot.Collections.Array<Attribute>()
        };

        foreach (var attribute in Attributes)
        {
            clone.Attributes.Add(attribute.Clone());
        }

        return clone;
    }

}