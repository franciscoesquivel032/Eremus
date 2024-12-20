using Godot;
using Godot.Collections;

public partial class ManagersRes : Resource
{
    [Export]
    public Array<CSharpScript> ManagerList { get ; set; }
} 