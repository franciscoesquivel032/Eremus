using System;

public class ResourceLoadException : Exception
{
    public ResourceLoadException(){}
    public ResourceLoadException(string message) : base(message){}
    public ResourceLoadException(string message, Exception inner) : base(message, inner){}
}