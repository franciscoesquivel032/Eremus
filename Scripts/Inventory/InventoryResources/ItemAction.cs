using Godot;

/// <summary>
/// Command pattern allowes to easyly add new Actions.
/// Action logic is separated from the inventory, items and entities
/// ItemActions are modifiable on runtime
/// Same ItemAction can be asociated to different items
/// </summary>
public abstract partial class ItemAction : Resource
{
    public abstract void Execute(Node target);
}