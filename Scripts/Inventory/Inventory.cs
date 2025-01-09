using Godot;
using System;
using System.Collections.Generic;

//TODO
/// <summary>
/// Inventory is a Dictionary where
///     Key => ItemData
///     Value => Amount of units stored of its key ItemData
/// Inventory has a capacity represented by a Resource named InventorySize
/// </summary>
public partial class Inventory : Node
{

    // InventorySize resource contains an int "Size"
    [Export]
    public InventorySize Capacity;

    private Dictionary<ItemData, int> _items;
    public Dictionary<ItemData, int> Items => _items;

    public override void _Ready()
    {
        Prints.Loading("Initializing inventory items...");
        
        base._Ready();
        _items = new Dictionary<ItemData, int>();

        Prints.Loaded("Inventory items initialized.");

        InitEquipment();
    }
   
   /// <summary>
   /// Adds an item with a given quantity to the dictionary of items
   /// </summary>
   /// <param name="item"></param>
   /// <param name="quantity"></param>
   /// <returns></returns>
    public bool AddItem(ItemData item, int quantity)
    {
        bool success = true; // return var
        int currentWeight = GetCurrentWeight(); // stores the current weight
        int addedWeight = item.Weight * quantity; // stores the weight of the items to be addded

        // Checks if there is enough room to add the item
        if(currentWeight + addedWeight > Capacity.MaxCapacity)
        {
            success = false;
        }   

        // If the added item is already stored adds the quantity introduced to its current
        // otherwise it adds a new Entry to de Dictionary
        _items[item] = Items.ContainsKey(item) ?
        _items[item] + quantity :
        quantity; 

        return success;
    }

    /// <summary>
    /// Substracts the indicated quantity from the given Key
    /// If the resulting quantity is less or equal to zero, remove the indicated item from the Dictionary
    /// </summary>
    /// <param name="item"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void RemoveItem(ItemData item, int quantity)
    {
        // Checks if the item is stores in the Dictionary
        if(!IsItem(item)) 
            throw new ArgumentException("Item introduced is not in the inventory...");

        // Checks if the quantity introduced is below quantity stored
        if(_items[item] < quantity) 
            throw new InvalidOperationException("Quantity introduced surpases the current units of the item...");

        // Substracts the amount of items indicated
        _items[item] -= quantity;

        // If the resulting amount of the item stored is less or equal to zero remove the item from the Dictionary
        if(IsQuantBelowZero(item))
            _items.Remove(item);

    }

    /// <summary>
    /// Edits the quantity of a given item
    /// </summary>
    /// <param name="item"></param>
    /// <param name="newQuantity"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void EditItem (ItemData item, int newQuantity)
    {
        int currentWeight; 
        int previousWeight;
        int newWeight;

        // Checks if the item is stored in the Dictionary
        if(!IsItem(item)) 
            throw new ArgumentException("Item introduced is not in the inventory...");

        currentWeight = GetCurrentWeight();
        previousWeight = item.Weight * _items[item];
        newWeight = item.Weight * newQuantity;

        // If we substract the former weight of an item to add its new weight and it exceedes the MaxCapacity of the inventory
        if (currentWeight - previousWeight + newWeight > Capacity.MaxCapacity)
            throw new InvalidOperationException("Weight exceeded...");

        // Edit quantity
        _items[item] = newQuantity;

        // If the resulting amount of the item stored is less or equal to zero remove the item from the Dictionary
        if(IsQuantBelowZero(item))
            _items.Remove(item);

    }

    // Returns rather or not an item quantity is below 0
    public bool IsQuantBelowZero(ItemData item) => _items[item] <= 0;

    // Returns the current total weight
    public int GetCurrentWeight() => Capacity.CurrentWeight(_items);

    // Returns rather or not an item is stored in the Dictionary
    public bool IsItem(ItemData item) => _items.ContainsKey(item);

}