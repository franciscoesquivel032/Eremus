
using System;

public class IllegalEquipableStateException : Exception
{
    public IllegalEquipableStateException(){}
    public IllegalEquipableStateException(string message) : base(message){}
    public IllegalEquipableStateException(string message, Exception inner) : base(message, inner){}
}