



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

        // Iterates the collection of managers and instantiates a new Node in the hierarchy containing Singleton script  
          managerList.ManagerList.ToList<CSharpScript>().ForEach(m =>AddChild( (Node) m.New() ));

    }

}