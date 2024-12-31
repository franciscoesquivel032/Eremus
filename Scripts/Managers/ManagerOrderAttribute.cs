using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ManagerOrderAttribute : System.Attribute
{
    public int Order { get; }

    public ManagerOrderAttribute(int order)
    {
        Order = order;
    }
}