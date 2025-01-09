using System.Collections.Generic;
using Godot;

// TODO: Implement Inventory.Equipment

/// <summary>
/// Inventory equipment class ~
/// Manages equipment slots and items equipped
/// </summary>
public partial class Inventory : Node
{
    private Dictionary<EquipmentSlot, Equipable> _equipment;
    public Dictionary<EquipmentSlot, Equipable> Equipment => _equipment;

    private EntityStatsHandler entityStats;

    /// <summary>
    /// Temporary until we implement databases
    /// New Dictionary which stores each EquipmentSlot and null (for now)
    /// </summary>
    private void InitEquipment()
    {
        Prints.Loading("Initializing equipment...");

        base._Ready();

        _equipment = new Dictionary<EquipmentSlot, Equipable>();

        EquipmentSlot[] slots = (EquipmentSlot[])System.Enum.GetValues(typeof(EquipmentSlot));
        slots.ForEach(slot => _equipment.Add(slot, null));
        
        entityStats = GetNode<EntityStatsHandler>("../EntityStats");
        Prints.Loaded("Equipment initialized.");
    }


    //TODO Stats calcs
    /// <summary>
    /// Equip an item to a slot
    /// if the slot does not exist throw an exception
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
                if(!IsSlotFree(item.Slot))
                    entityStats.RemoveStats(_equipment[item.Slot]);

                _equipment[item.Slot] = item;
                entityStats.AddStats(item);
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

    /// <summary>
    /// Check if a given slot is free
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    private bool IsSlotFree(EquipmentSlot slot) => _equipment[slot] == null;

    
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
            
            if(!IsSlotFree(slot))
                entityStats.RemoveStats(_equipment[slot]);
            
            _equipment[slot] = null;
        }
        else
        {
            throw new System.ArgumentException("Slot does not exist...");
        }
    }

}