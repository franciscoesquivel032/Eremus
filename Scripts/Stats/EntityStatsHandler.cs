using Godot;

// TODO
public partial class EntityStatsHandler : Node 
{

    [Export] private StatsResource _baseStats;
    public StatsResource BaseStats => _baseStats;

    private StatsResource _currentStats;
    public StatsResource CurrentStats => _currentStats;

    public override void _Ready()
    {
        base._Ready();
        _baseStats = new StatsResource();
        _currentStats = new StatsResource(_baseStats);
    }

    public void AddStats(Equipable item)
    {
        if(item != null)
        {
            if(item.Type == ItemType.Equipable)
            {
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

    public void RemoveStats(Equipable item)
    {
        if(item != null)
        {
            if(item.Type == ItemType.Equipable)
            {
                _currentStats.MaxHealth -= item.BonusStats.MaxHealth;
                _currentStats.Mana -= item.BonusStats.Mana;
                _currentStats.Armor -= item.BonusStats.Armor;
                _currentStats.PhysicalDamage -= item.BonusStats.PhysicalDamage;
                _currentStats.MagicDamage -= item.BonusStats.MagicDamage;
                _currentStats.MovementSpeed -= item.BonusStats.MovementSpeed;
                _currentStats.AttackSpeed -= item.BonusStats.AttackSpeed;
                _currentStats.AttackRange -= item.BonusStats.AttackRange;
                _currentStats.Strength -= item.BonusStats.Strength;
                _currentStats.Dexterity -= item.BonusStats.Dexterity;
                _currentStats.Intelligence -= item.BonusStats.Intelligence;
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
