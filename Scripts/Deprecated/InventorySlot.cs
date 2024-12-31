using Godot;

public partial class InventorySlot : Node
{

    private ItemData _item;
    public ItemData Item
    {
        get { return _item; } 
        set { _item = value; } 
    }

    private int _quantity;
    public int Quantity 
    { 
        get { return _quantity; } 
        set { _quantity = value; } 
    }

    public InventorySlot()
    {
        this._item = null;
        this._quantity = 0;
    }

    public InventorySlot(ItemData data, int quantity)
    {
        this._item = data;
        this._quantity = quantity;
    }

    public InventorySlot(ItemData data)
    {
        this._item = data;
        this._quantity = 1;
    }


}