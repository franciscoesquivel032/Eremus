




using Godot;

public abstract partial class Manager<T> : Node where T: Manager<T>, new()
{
    private static T _instance = new();

    /// <summary>
    ///  Returns the instance of the singleton.
    /// </summary>
    public static T Instance => _instance ?? new();

    public override void _EnterTree()
    {
        SetClassnameAsName();
    }

    /// <summary>
    /// Sets the classname as name.
    /// </summary>
    private void SetClassnameAsName()
    {
        // Sets the name of the node as the class for the editor
        Name = GetType().Name;
    }

}