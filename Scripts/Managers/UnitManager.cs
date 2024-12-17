using Godot;



public partial class UnitManager : Node
{
    public static UnitManager Singleton { get; private set; }

    private Godot.Collections.Array<Node3D> _units;

    public Godot.Collections.Array<Node3D> Units {
        get { return _units; }
        set { _units = value; }
    }

    public override void _Ready()
    {
        GD.Print("Loaded UnitManager!");
    }

    public override void _Process(double delta)
    {
        foreach(Node3D unit in _units)
        {
            GD.Print(unit);   
        }
    }

}