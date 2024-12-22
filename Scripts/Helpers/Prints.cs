using System;
using Godot;

public static class Prints{
    public static void ResourceLoadSuccessfully(object context){
        GD.Print($"{context.GetType().Name} references loaded successfully...");
    }

    public static void RefsInitSuccessfully(object context){
        GD.Print($"{context.GetType().Name} resources loaded successfully...");
    }

    public static void Loading(object context){
        GD.Print($"Loading {context.GetType().Name}");
    }

    public static void Loaded(object context){
        GD.Print($"Loaded {context.GetType().Name}");
    }
}
