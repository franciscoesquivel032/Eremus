




using Godot;

public partial class MainManager : Node
{

    private static MainManager _instance;
    public static MainManager Instance {
        get { return _instance; }
    }

    public override void _EnterTree()
    {        
        AddChild(CameraManager.Instance);
        AddChild(UnitManager.Instance);
    }

}