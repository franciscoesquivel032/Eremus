using System.Collections.Generic;
using Godot;

// TODO: Implement Inventory.Equipment

/// <summary>
/// Inventory equipment class ~
/// Manages equipment slots and items equipped
/// </summary>
public partial class Inventory : Node
{
    private Dictionary<EquipmentSlot, ItemData> _equipment;
    public Dictionary<EquipmentSlot, ItemData> Equipment => _equipment;

    /// <summary>
    /// Temporary until we implement databases
    /// New Dictionary which stores each EquipmentSlot and null (for now)
    /// </summary>
    public override void _Ready()
    {
        base._Ready();
        _equipment = new Dictionary<EquipmentSlot, ItemData>();

        EquipmentSlot[] slots = (EquipmentSlot[])System.Enum.GetValues(typeof(EquipmentSlot));
        slots.ForEach(slot => _equipment.Add(slot, null));
    }

}