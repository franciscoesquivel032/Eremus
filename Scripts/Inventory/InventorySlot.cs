using Godot;
using System;

public partial class InventorySlot : Resource 
{
    [Export]
    public ItemData Item;

    [Export]
    public int Quantity;
}