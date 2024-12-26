



using System;
using System.Linq;
using System.Reflection;
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

        // Obtener todos los tipos de managers con el atributo ManagerOrder
        var managerTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<ManagerOrderAttribute>() != null)
            .OrderBy(t => t.GetCustomAttribute<ManagerOrderAttribute>().Order);

        // Instanciar y añadir los managers en orden
        foreach (var type in managerTypes)
        {
            var instance = (Node)Activator.CreateInstance(type);
            AddChild(instance); 
        }

        Prints.Loaded(this);
    }

}