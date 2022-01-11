using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Button startButton, speechButton, statisticsButton;
    [SerializeField] private GameObject buttonsObj;
    [SerializeField] private NarratorMMController narratorController;
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
}
