using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Button startButton, speechButton;
    [SerializeField] private GameObject buttonsObj;
    [SerializeField] private NarratorMMController narratorController;
    private AudioClip speechClip;

    void Start() { // Start is called before the first frame update
        speechClip = narratorController.SpeechAudio.clip;

        if(!AplicationModel.isFirstTimeScene1) {
            speechButton.gameObject.SetActive(true);
        }

        if(!AplicationModel.scenesCompleted[0]) {
            startButton.onClick.AddListener(
                () => {
                    if(AplicationModel.isFirstTimeScene1) {
                        hideButtonsPlaySpeech();
                        sceneLoader.Invoke("loadScene1", speechClip.length + 1);
                        AplicationModel.isFirstTimeScene1 = false;
                    }
                    else sceneLoader.loadScene1();
                }
            );
        }
        else {
            startButton.GetComponentInChildren<Text>().text = "ATIVIDADES";
            startButton.onClick.AddListener(
                () => sceneLoader.loadSceneSelection()
            );
        }
    }

    private void showButtons() { // Invoked by hideButtonsPlaySpeech()
        buttonsObj.SetActive(true);
    }

    public void hideButtonsPlaySpeech() { // Called by button (BtSpeech)
        buttonsObj.SetActive(false);
        narratorController.playSpeechAudio();
        Invoke("showButtons", speechClip.length);
    }
}
