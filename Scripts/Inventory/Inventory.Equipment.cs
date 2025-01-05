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

    /// <summary>
    /// Temporary until we implement databases
    /// New Dictionary which stores each EquipmentSlot and null (for now)
    /// </summary>
    public override void _Ready()
    {
        base._Ready();
        _equipment = new Dictionary<EquipmentSlot, Equipable>();

        EquipmentSlot[] slots = (EquipmentSlot[])System.Enum.GetValues(typeof(EquipmentSlot));
        slots.ForEach(slot => _equipment.Add(slot, null));

        GD.Print("Loading item...");
        Equipable item = GD.Load<Equipable>("res://Data(Resources)/Items/Equipables/HelmetTest.tres");
        GD.Print("Item loaded... Starting to equip...");
        EquipItem(item);
        GD.Print("Item equipped...");
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
        GD.Print("~~Equipping item...");
        if(item != null)
        {
            if(item.Type == ItemType.Equipable)
            {
                if(_equipment[item.Slot] != null)
                {
                    removeCurrentStats(item.Slot);
                }
                _equipment[item.Slot] = item;
                addNewStats(item);
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
        GD.Print("~~Item equipped...");
    }

    private void removeCurrentStats(EquipmentSlot slot)
    {
        GD.Print("--Removing stats from current...");

        if (_equipment[slot] != null)
        {
            var entityAttributes = GetParent().GetNode<EntityAttributes>("EntityAttributes");
            if (entityAttributes != null)
            {
                entityAttributes.RemoveStatsFromCurrent(_equipment[slot].BonusStats);
            }
            else
            {
                GD.PrintErr("EntityAttributes node not found");
            }
        }

        GD.Print("--Stats removed from current...");
    }

    private void addNewStats(Equipable item)
    {

        GD.Print("++Adding new stats...");
        var entityAttributes = GetParent().GetNode<EntityAttributes>("EntityAttributes");
        if (entityAttributes != null)
        {
            entityAttributes.AddStatsToCurrent(item.BonusStats);
        }
        else
        {
            GD.PrintErr("EntityAttributes node not found");
        }
        GD.Print("++New stats added...");
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