


using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

[ManagerOrder(0)]
public partial class AssetManager : Manager<AssetManager>
{

	private Mesh _mainHalo, _secondaryHalo;

	private Dictionary<string, Mesh> _meshes;

	public override void _EnterTree()
	{
		base._EnterTree();

		Prints.Loading(this);

		_meshes = new();

		GetAllDirectories();

		OnManagerReady();
	}


	private void LoadAllAssets()
	{
		
	}

	private List<string> GetAllDirectories(string basePath = "res://")
	{
		List<string> directories = new List<string>();
		
		
		ProcessDir(basePath);


		return directories;
	}

	private void ProcessDir(string path)
	{
		DirAccess dir = DirAccess.Open(path);

		if(dir != null)
		{
			dir.GetDirectories().ForEach(dir => {
				ProcessDir(path + "/" +  dir);
			});

			dir.GetFiles()
				.ToList()
				.FindAll(file => file.EndsWith(".tres"))
				.ForEach(file => LoadResource(path, file));
		}
	}

	private void LoadResource(string path, string name)
	{
		Resource res = GD.Load(path + "/" + name);
		GD.Print(res);
		if (res is Mesh mesh)
		{
			GD.Print("Loading mesh: " + name);
			_meshes.Add(name, mesh);
		}
	}

    public Mesh GetMesh(string name)
    {
        return _meshes[name];
    }
}
