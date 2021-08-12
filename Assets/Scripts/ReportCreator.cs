using UnityEngine;
using System.IO;

public class ReportCreator
{
    private static string path =
        Application.dataPath + "/estatisticas.txt";

    public static void resetReport()
    {
        StreamWriter writer = new StreamWriter(path, false);
        writer.Close();
    }
        
    public static void writeLine(string line)
    {
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(line);
        writer.Close();
    }

}
