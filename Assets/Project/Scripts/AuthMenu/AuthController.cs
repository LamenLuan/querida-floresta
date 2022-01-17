using UnityEngine;
using UnityEngine.UI;

public class AuthController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GoogleSheetsController sheetsController;
    [SerializeField] private WebCamController webCamController;
    [SerializeField] private GameObject buttonsObj, noConnectionObj, webCamObj;
    [SerializeField] private Text quitButtonTxt;
    private bool readQr;

    void Start()
    {
        try {
            sheetsController.StartSheets();
        }
        catch (System.Net.Http.HttpRequestException) {
            ErrorMode("Erro de conexão com a rede");
        }
        catch (System.Exception) {
            ErrorMode("Um erro inesperado aconteceu");
        }
    }
    
    public void ReadQrMode()
    {
        readQr = true;
        buttonsObj.SetActive(false);
        webCamObj.SetActive(true);
        quitButtonTxt.text = "VOLTAR";
        webCamController.StartWebCam();
    }

    public void ErrorMode(string msg)
    {
        buttonsObj.SetActive(false);
        Text text = noConnectionObj.transform.GetComponentInChildren<Text>();
        text.text = msg;
        noConnectionObj.SetActive(true);
    }

    public void LoadPlayer(string id)
    {
        var data = sheetsController.FindEntry(id);
        if (data == null) return;
        Player.Instance.LoadData(data);
        
        sceneLoader.Invoke("loadMainMenu", 2.0f);
    }

    public void QuitButtonAction()
    {
        if(readQr) {
            webCamController.StopCam();
            sceneLoader.loadAuthMenu();
        }
        else sceneLoader.quitGame();
    }
}
