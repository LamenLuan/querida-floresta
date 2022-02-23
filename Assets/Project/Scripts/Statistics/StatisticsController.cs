using UnityEngine;
using UnityEngine.UI;

public class StatisticsController : MonoBehaviour
{
    [SerializeField] private Text[] reportTxt;

    private string ErrorQuantity(byte sceneMisses)
    {
        return "\t\tQuantidade de erros: " + AplicationModel.Scene2Misses + 
            "\n";
    }

    private string ErrorQuantity(string noun, byte[] sceneMisses)
    {
        string str = "\t\tQuantidade de erros\n";

        for (int i = 0; i < sceneMisses.Length; i++)
            str += "\t\t\t" + noun + " " + i + ": " + sceneMisses[i] + "\n";
            
        return str;
    }

    private string ResponseTime(double resTime)
    {
        return "\t\tTempo de início: " + resTime.ToString("F2") +  " s";
    }

    void Start() // Start is called before the first frame update
    {
        const string notCompleted = "Atividade ainda não completa";
        
        string[] scenes = {
            "Atividade 1\n" +
                ErrorQuantity("Fase", AplicationModel.Scene1Misses) +
                ResponseTime(AplicationModel.PlayerResponseTime[0]),
            "Atividade 2\n" +
                ErrorQuantity(AplicationModel.Scene2Misses) +
                ResponseTime(AplicationModel.PlayerResponseTime[1]),
            "Atividade 3\n" +
                ErrorQuantity("Questão", AplicationModel.Scene3Misses) +
                ResponseTime(AplicationModel.PlayerResponseTime[2])
        };

        for (int i = 0; i < reportTxt.Length; i++) {
            bool sceneCompleted = Player.Instance.ScenesCompleted[i];
            if(sceneCompleted) reportTxt[i].text = scenes[i];
            reportTxt[i].text = sceneCompleted ? scenes[i] : notCompleted;
        }
    }
}
