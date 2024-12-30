
using System;
using System.Linq;
using System.Reflection;
using Godot;

// Purpose: MainManager is the main manager of the game, it is responsible for loading all the other managers in the correct order.

public partial class MainManager : Node
{

    private static MainManager _instance;
    public static MainManager Instance {
        get { return _instance; }
    }

    public override void _EnterTree()
    {    
        Prints.Loading(this);

        // Obtain all manager types with ManagerOrder attribute
        var managerTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<ManagerOrderAttribute>() != null)
            .OrderBy(t => t.GetCustomAttribute<ManagerOrderAttribute>().Order);

        // Instantiate and add all managers to the scene
        foreach (var type in managerTypes)
        {
            var instance = (Node)Activator.CreateInstance(type);
            AddChild(instance); 
        }

        Prints.Loaded(this);
    }

}