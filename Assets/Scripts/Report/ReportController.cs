using UnityEngine;
using UnityEngine.UI;

public class ReportController : MonoBehaviour
{
    [SerializeField] private Text reportTxt;

    private string ErrorQuantity(string noun, byte[] sceneMisses)
    {
        string str = "\t\tQuantidade de erros\n";

        for (int i = 0; i < sceneMisses.Length; i++)
            str += $"\t\t\t{noun} {i}: {sceneMisses[i]}\n";
            
        return str;
    }

    private string ResponseTime(double resTime)
    {
        return $"\t\tTempo de resposta da atividade: {resTime.ToString("F2")}";
    }

    void Start() // Start is called before the first frame update
    {
        if( AplicationModel.LastSceneCompleted() ) {
            reportTxt.text = 
                "Atividade 1\n" +
                ErrorQuantity("Fase", AplicationModel.Scene1Misses) +
                ResponseTime(AplicationModel.PlayerResponseTime[0]) + "\n\n" +
                
                "Atividade 2\n" +
                $"\t\tQuantidade de erros: {AplicationModel.Scene2Misses}\n" +
                ResponseTime(AplicationModel.PlayerResponseTime[1]) + "\n\n" +

                "Atividade 3\n" +
                ErrorQuantity("Questão", AplicationModel.Scene3Misses) +
                ResponseTime(AplicationModel.PlayerResponseTime[2]);
        }
    }
}
