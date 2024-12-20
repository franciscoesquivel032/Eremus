



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
        ManagersRes managerList =  GD.Load<ManagersRes>("res://Data(Resources)/ManagersRes.tres");

        GD.Print("Loaded: " + managerList);

        AddChild(CameraManager.Instance);
        AddChild(UnitManager.Instance);
    }

}