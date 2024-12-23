using Godot;
using System;
using System.Collections.Generic;


/// <summary>
/// 
/// An inventory is a list of InventorySlot
/// which represents the ItemData and the amount of units stored
/// 
/// ------
/// 
/// Better aproach than storing a Dictionary<ItemData,int> 
/// due to its limitations when designing the UI since Dictionaries
/// don't have an order and this aproach is way more scalable
/// [...]
/// 
/// Example : in case we want to implement
/// more data to an inventory slot such as a 
/// frame colored based on the ItemType
/// 
/// [...] + 
/// Godot doesn't support Dictionaries to be visible from the inspector
/// 
/// ------
/// 
/// In the other hand, this model is less accessible.
/// We can overcome this limitation writing a function
/// in the inventory handler script that builds and returns a Dictionary
/// just in case we want to do a rapid search of an item stored.
/// 
/// </summary>
public partial class Inventory : Resource 
{
    public List<InventorySlot> Items { get; set; }

}