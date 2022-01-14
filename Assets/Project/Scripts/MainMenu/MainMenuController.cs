using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Button startButton, speechButton, statisticsButton;
    [SerializeField] private GameObject buttonsObj, noConnectionObj, webCamObj;
    [SerializeField] private NarratorMMController narratorController;
    [SerializeField] private GoogleSheetsController sheetsController;
    [SerializeField] private WebCamController webCamController;
    private AudioClip speechClip;

    private void NewGameStarted()
    {
        if (AplicationModel.isFirstTimeScene1) {
            HideButtonsPlaySpeech();
            sceneLoader.Invoke("loadScene1", speechClip.length + 1);
            AplicationModel.isFirstTimeScene1 = false;
        }
        else sceneLoader.loadScene1();
    }

    void Start() // Start is called before the first frame update
    {
        speechClip = narratorController.SpeechAudio.clip;
        speechButton.gameObject.SetActive(!AplicationModel.isFirstTimeScene1);
        statisticsButton.interactable = AplicationModel.scenesCompleted[0];

        if(!AplicationModel.scenesCompleted[0]) {
            startButton.onClick.AddListener(NewGameStarted);
        }
        else {
            startButton.GetComponentInChildren<Text>().text = "ATIVIDADES";
            startButton.onClick.AddListener(sceneLoader.loadSceneSelection);
        }
    }

    private void ShowButtons() // Invoked by hideButtonsPlaySpeech()
    {
        buttonsObj.SetActive(true);
    }

    public void HideButtonsPlaySpeech() // Called by button (BtSpeech)
    {
        buttonsObj.SetActive(false);
        narratorController.playSpeechAudio();
        Invoke("ShowButtons", speechClip.length + 1);
    }

    public void ErrorMode(string msg)
    {
        buttonsObj.SetActive(true);
        for(int i = 0; i < buttonsObj.transform.childCount - 1; i++)
            buttonsObj.transform.GetChild(i).gameObject.SetActive(false);
        webCamController.StopCam();
        webCamObj.SetActive(false);
        
        Text text = noConnectionObj.transform.GetComponentInChildren<Text>();
        text.text = msg;

        noConnectionObj.SetActive(true);
    }

    public bool LoadPlayer(string id)
    {
        var data = sheetsController.FindEntry(id);
        if (data == null) return false;
        Player.Instance.LoadData(data);

        return true;
    }

    public bool RegisterPlayer(string name)
    {
        Player.Instance.Name = name;
        IList<object> data = Player.Instance.ToObjectList();
        print( sheetsController.CreateEntry(data) );

        return true;
    }

    public void SavePlayerData()
    {
        sheetsController.UpdateEntry(
            Player.Instance.Id, Player.Instance.ToObjectList()
        );
    }

}
