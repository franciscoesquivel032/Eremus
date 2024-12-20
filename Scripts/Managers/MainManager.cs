



using System;
using System.Linq;
using Godot;

public partial class MainManager : Node
{

    private static MainManager _instance;
    public static MainManager Instance {
        get { return _instance; }
    }


    public override void _EnterTree()
    {    
        Prints.Loading(this);

        AddChild(CameraManager.Instance);
        AddChild(UnitManager.Instance);

        Prints.Loaded(this);
    }

}