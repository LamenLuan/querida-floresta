using System.Collections.Generic;

class Player // Singleton
{
    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null) _instance = new Player();
            return _instance;
        }
    }

    public string Id { get; set; } 
    public string Name { get; set; }
    public bool[] ScenesCompleted { get; } = new bool[3];

    private Player() { }

    public void LoadData(string[] data)
    {
        Id = data[0];
        Name = data[1];
        for(int i = 0; i < ScenesCompleted.Length; i++)
            ScenesCompleted[i] = !data[i + 2].Equals("0");
    }

    public IList<object> ToObjectList()
    {
        List<object> list = new List<object>(){Name};
        foreach(bool scene in ScenesCompleted) list.Add(scene ? 1 : 0);

        return list;
    }

    public void ClearData()
    {
        _instance = new Player();
    }
}