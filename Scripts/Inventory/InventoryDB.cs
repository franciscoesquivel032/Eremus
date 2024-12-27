using System.Collections.Generic;
using Godot;
using LinqToDB.Mapping;



using LinqToDB.Mapping;


public class InventoryDB
{
    [PrimaryKey, Identity]
    public int Id { get; set; }
    
}
