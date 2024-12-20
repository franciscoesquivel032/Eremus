


using System;
using Godot;

public partial class TestManager : Manager<TestManager>
{

    

    public override void _EnterTree()
    {
        base._EnterTree();
        GD.Print("Holita");
    }


}