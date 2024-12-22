
using Godot;


// TODO: This is a test


[GlobalClass]
public sealed partial class Item(string id) : Resource
{
    public readonly string Id = id;
    public static readonly Item STONE = new("stone");
    
}
