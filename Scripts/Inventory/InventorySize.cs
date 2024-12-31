using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class InventorySize : Resource
{
    [Export]
    public int MaxCapacity { get; set; }

    /// <summary>
    /// Calculated property ~
    /// 
    /// Returns the total weight of a given Dictionary<ItemData,int> by
    /// multiplying each item weight times units stored
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public int CurrentWeight (Dictionary<ItemData, int> items) => items.Sum(i => i.Key.Weight * i.Value);
}