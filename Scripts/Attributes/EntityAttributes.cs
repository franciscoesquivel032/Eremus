using Godot;

[Tool]
public partial class EntityAttributes : Node
{
    private Stats _stats;

    [Export]
    public Stats Stats 
    {
        get => _stats; 
        set => _stats = value;
    }

    public override void _Ready()
    {
        base._Ready();
        _stats = GD.Load<Stats>("res://Data(Resources)/Attributes/EntityStats/EntityStatsTest/EntityStatsTest.tres");
    }

    public void AddStatsToCurrent(Stats stats)
    {
        GD.Print("Adding stats to current...");
        
        stats.Attributes.ForEach(SummAttribute);
        
    }

    public void SummAttribute(Attribute attribute)
    {
        var existingAttribute = _stats.GetAttribute(attribute.StatName);
        if (existingAttribute != null)
        {
            existingAttribute.CurrentValue += attribute.CurrentValue;
        }
    }

    public void RemoveStatsFromCurrent(Stats stats)
    {
        stats.Attributes.ForEach(subtractAttribute);
    }

    public void subtractAttribute(Attribute attribute)
    {
        var existingAttribute = _stats.GetAttribute(attribute.StatName);
        if (existingAttribute != null)
        {
            existingAttribute.CurrentValue -= attribute.CurrentValue;
        }
    }
}