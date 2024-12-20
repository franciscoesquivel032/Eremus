using Godot;
using Godot.Collections;

public partial class ManagersRes : Resource
{
    [Export]
    public Array<CSharpScript> ManagerList { get ; set; }


    public void Cosa(){
        foreach (CSharpScript item in ManagerList)
        {
            item.New();
        }
    }
} 