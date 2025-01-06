using Godot;

public partial class EntityStatsHandler : Node 
{

    private EntityStats _baseStats;
    public EntityStats BaseStats => _baseStats;

    private EntityStats _currentStats;
    public EntityStats CurrentStats => _currentStats;

    public override void _Ready()
    {
        base._Ready();
        _baseStats = new EntityStats();
        _currentStats = new EntityStats(_baseStats);
    }

    public void EquipItem(Equipable item)
    {
        if(item != null)
        {
            if(item.Type == ItemType.Equipable)
            {
                _currentStats = new EntityStats(_baseStats);
                _currentStats.MaxHealth += item.BonusStats.MaxHealth;
                _currentStats.Mana += item.BonusStats.Mana;
                _currentStats.Armor += item.BonusStats.Armor;
                _currentStats.PhysicalDamage += item.BonusStats.PhysicalDamage;
                _currentStats.MagicDamage += item.BonusStats.MagicDamage;
                _currentStats.MovementSpeed += item.BonusStats.MovementSpeed;
                _currentStats.AttackSpeed += item.BonusStats.AttackSpeed;
                _currentStats.AttackRange += item.BonusStats.AttackRange;
                _currentStats.Strength += item.BonusStats.Strength;
                _currentStats.Dexterity += item.BonusStats.Dexterity;
                _currentStats.Intelligence += item.BonusStats.Intelligence;
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
    }

    public void UnequipItem(Equipable item)
    {
        if(item != null)
        {
            if(item.Type == ItemType.Equipable)
            {
                _currentStats = new EntityStats(_baseStats);
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
    }
}
