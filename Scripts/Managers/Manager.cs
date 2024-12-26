




using System;
using Godot;

public delegate void OnManagerReady();

public abstract partial class Manager<T> : Node where T: Manager<T>, new()
{
    private static T _instance = new();

    /// <summary>
    ///  Returns the instance of the singleton.
    /// </summary>
    public static T Instance => _instance ?? new();

    /// <summary>
    /// Fires an event when the manager is ready.
    /// </summary>
    public event OnManagerReady ManagerReady;

    public override void _EnterTree()
    {
        SetClassnameAsName();
        _instance = this as T;
    }

    /// <summary>
    /// Sets the classname as name.
    /// </summary>
    private void SetClassnameAsName()
    {
        // Sets the name of the node as the class for the editor
        Name = GetType().Name;
    }

    protected virtual void OnManagerReady()
    {
        ManagerReady?.Invoke();
    }   

}