using Godot;

public partial class HealAction : ItemAction
{
    [Export]
    public int HealAmount {get; set;} = 30;

    public override void Execute(Node target)
    {
        // TODO Stats feature not implemented
        GD.Print($"{target.Name} healed for {HealAmount} pts...");
    }
}