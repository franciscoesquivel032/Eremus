using System;

public class CameraSystemNullReferenceException : NullReferenceException
{
    public CameraSystemNullReferenceException(){}
    public CameraSystemNullReferenceException(string message) : base(message){}
    public CameraSystemNullReferenceException(string message, Exception inner) : base(message, inner){}

}