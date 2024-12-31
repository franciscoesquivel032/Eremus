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


    //TODO Stats calcs
    /// <summary>
    /// Equip an item to a slot
    /// if the item is null throw an exception
    /// if the item is not equipable throw an exception
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="IllegalEquipableStateException"></exception>
    /// <exception cref="System.ArgumentNullException"></exception>
    public void EquipItem(Equipable item)
    {
        if(item != null)
        {
            if(item.Type == ItemType.Equipable)
            {
                _equipment[item.Slot] = item;
            }
            else
            {
                throw new IllegalEquipableStateException("Item is not equipable...");
            }
        }
        else
        {
            throw new System.ArgumentNullException("Item cannot be null...");
        }

    }
    
    //TODO Stats calcs
    /// <summary>
    /// Unequip an item from a slot
    /// if the slot does not exist throw an exception
    /// </summary>
    /// <param name="slot"></param>
    /// <exception cref="System.ArgumentException"></exception>
    public void UnequipItem(EquipmentSlot slot)
    {
        if(_equipment.ContainsKey(slot))
        {
            _equipment[slot] = null;
        }
        else
        {
            throw new System.ArgumentException("Slot does not exist...");
        }
    }

}