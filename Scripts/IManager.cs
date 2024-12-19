


public interface IManager<T>
{

    static T Instance { get; }

    abstract static void Load();

}