using Godot;

public partial class Equipable : ItemData
{
    [Export]
    public EquipmentSlot Slot { get; set; }
    
    [Export]
    public Stats BonusStats { get; set; }
}