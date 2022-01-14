using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GoogleSheetsController sheetsController;
    [SerializeField] private WebCamController webCamController;
    [SerializeField] private GameObject loginButtonObj, registerButtonObj,
        noConnectionObj, webCamObj;
    [SerializeField] private Text quitButtonTxt;
    private bool readQr;

    public bool ValitadeId(string id)
    {
        try { if(int.Parse(id) < 1) return false; }
        catch (FormatException) { return false; }
        return true;
    }

    private void DisableButtons()
    {
        loginButtonObj.SetActive(false);
        registerButtonObj.SetActive(false);
    }

    public void ReadQrMode()
    {
        readQr = true;
        DisableButtons();
        webCamObj.SetActive(true);
        quitButtonTxt.text = "VOLTAR";
        webCamController.StartWebCam();
    }

    public void ErrorMode(string msg)
    {
        DisableButtons();
        Text text = noConnectionObj.transform.GetComponentInChildren<Text>();
        text.text = msg;
        noConnectionObj.SetActive(true);
    }

    public void LoadPlayer(string id)
    {
        var data = sheetsController.FindEntry(id);
        if (data == null) return;
        Player.Instance.LoadData(data);
        sceneLoader.loadMainMenu();
    }

    public bool RegisterPlayer(string name)
    {
        Player.Instance.Name = name;
        IList<object> data = Player.Instance.ToObjectList();
        print(sheetsController.CreateEntry(data));
        return true;
    }

    public void SavePlayerData()
    {
        sheetsController.UpdateEntry(
            Player.Instance.Id, Player.Instance.ToObjectList()
        );
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
