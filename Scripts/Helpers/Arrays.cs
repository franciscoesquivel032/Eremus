




using System;
using System.Collections.Generic;

public static class ArrayHelpers 
{
    public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
        foreach(T item in enumeration)
        {
            action(item);
        }
    }
}