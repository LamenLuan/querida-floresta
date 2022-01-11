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

    public static void writeResponseTime(double tempoResposta) {
        ReportCreator.writeLine(
            $"Tempo de resposta da atividade: {tempoResposta.ToString("F2")}"  
        );
    }

    public static void writeMissesPerPhase(byte[] misses) {
        for (int i = 0; i < misses.Length; i++)
        {
            ReportCreator.writeLine(
                $"Quantidade de erros da questao {i + 1}: {misses[i]}"
            );
        }
    }
}
