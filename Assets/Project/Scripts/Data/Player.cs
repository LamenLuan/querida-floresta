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

    private Player() { }

    public void LoadData(string[] data)
    {
        Id = data[0];
        Name = data[1];
    }

    public IList<object> ToObjectList()
    {
        return new List<object>() {Name};
    }
}