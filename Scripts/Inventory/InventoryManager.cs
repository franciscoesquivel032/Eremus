




using System.Linq;
using Godot;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SQLite;
using Microsoft.Data.Sqlite;

public partial class InventoryManager : Manager<InventoryManager>
{

    public override void _EnterTree()
    {
        base._EnterTree();

		GD.Print("Loading Inventory Manager");
        
        
        // SQLiteTools.CreateDatabase("test.sqlite"); // Creates the database file

        var dbOptions = new DataOptions()
            .UseSQLiteMicrosoft("Data Source=test.sqlite");

        using var db = new DataConnection(dbOptions);

        

        ITable<InventoryDB> table;

        try
        {
            table = db.GetTable<InventoryDB>();
        }
        catch (System.Exception)
        {
            table = db.CreateTable<InventoryDB>();
        }

        // Insert sample data
        // db.Insert(new InventoryDB());
        // db.Insert(new InventoryDB());

        // Query data
        var items = table.ToList();
        foreach (var item in items)
        {
            GD.Print($"Id: {item.Id}");
        }




		GD.Print("Loading Inventory Manager");
    }

}