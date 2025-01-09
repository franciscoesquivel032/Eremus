using Godot;

[GlobalClass]
public partial class Equipable : ItemData
{
    /// <summary>
    /// Equipment slot
    /// </summary>
    [Export] 
    public EquipmentSlot Slot { get; set; }
    
    /// <summary>
    /// Bonus stats
    /// </summary>
    [Export]
    public StatsResource BonusStats { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public Equipable()
    {
        Slot = EquipmentSlot.Head;
        BonusStats = new StatsResource();
    }

    /// <summary>
    /// Constructor with parameters
    /// </summary>
    /// <param name="equipable"></param>
    public Equipable(Equipable equipable)
    {
        Slot = equipable.Slot;
        BonusStats = new StatsResource(equipable.BonusStats);
    }

}