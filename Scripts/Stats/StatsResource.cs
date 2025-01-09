using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// Entity stats resource model ~
/// </summary>

[GlobalClass]
public partial class StatsResource : Resource
{
    [Export] public float MaxHealth { get; set; }
    [Export] public float Mana { get; set; }
    [Export] public float Armor { get; set; }
    [Export] public float PhysicalDamage { get; set; }
    [Export] public float MagicDamage { get; set; }
    [Export] public float MovementSpeed { get; set; }
    [Export] public float AttackSpeed { get; set; }
    [Export] public float AttackRange { get; set; }
    [Export] public float Strength { get; set; }
    [Export] public float Dexterity { get; set; }
    [Export] public float Intelligence { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public StatsResource()
    {
        MaxHealth = 100;
        Mana = 100;
        Armor = 0;
        PhysicalDamage = 20;
        MagicDamage = 20;
        MovementSpeed = 20;
        AttackSpeed = 1;
        AttackRange = 1;
        Strength = 1;
        Dexterity = 1;
        Intelligence = 1;
    }

    /// <summary>
    /// Constructor with parameters
    /// </summary>
    /// <param name="stats"></param>
    public StatsResource(StatsResource stats)
    {
        MaxHealth = stats.MaxHealth;
        Mana = stats.Mana;
        Armor = stats.Armor;
        PhysicalDamage = stats.PhysicalDamage;
        MagicDamage = stats.MagicDamage;
        MovementSpeed = stats.MovementSpeed;
        AttackSpeed = stats.AttackSpeed;
        AttackRange = stats.AttackRange;
        Strength = stats.Strength;
        Dexterity = stats.Dexterity;
        Intelligence = stats.Intelligence;
    }

    /// <summary>
    /// Clone method
    /// </summary>
    /// <returns></returns>
    public StatsResource Clone()
    {
        return new StatsResource(this);
    }

    /// <summary>
    /// Load stats from a file
    /// </summary>
    /// <param name="path"></param>
    /// <exception cref="ArgumentException"></exception>
    public StatsResource(string path)
    {
        var loadedStats = (StatsResource)ResourceLoader.Load(path);
        if(loadedStats != null)
        {
            CopyFrom(loadedStats);
        }
        else
        {
            throw new ArgumentException("Stats could not be loaded...");
        }
    }

    /// <summary>
    /// Copy stats from another instance
    /// </summary>
    /// <param name="stats"></param>
    public void CopyFrom(StatsResource stats)
    {
        MaxHealth = stats.MaxHealth;
        Mana = stats.Mana;
        Armor = stats.Armor;
        PhysicalDamage = stats.PhysicalDamage;
        MagicDamage = stats.MagicDamage;
        MovementSpeed = stats.MovementSpeed;
        AttackSpeed = stats.AttackSpeed;
        AttackRange = stats.AttackRange;
        Strength = stats.Strength;
        Dexterity = stats.Dexterity;
        Intelligence = stats.Intelligence;
    }
}