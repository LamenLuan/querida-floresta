using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayersForestController : MonoBehaviour
{
    [SerializeField] private CanvasPFController canvasController;
    [SerializeField] private AudioController audioController;
    [SerializeField] private NarratorPFController narratorController;
    private string functionToInvoke;

    public void loadMainMenu() // Invoked in Start()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void loadScene2() // Invoked in Start()
    {
        SceneManager.LoadScene("Scene 2");
    }

    public void loadScene3() // Invoked in Start()
    {
        SceneManager.LoadScene("Scene 3");
    }

    private void showReward()
    {
        canvasController.removeLockedEffect();
        audioController.sceneCompletedSound();
        if(functionToInvoke != null) Invoke(functionToInvoke, 8f);
    }

    private void addButtonInCanvas() // Invoked in Start()
    {
        Button canvasButton = canvasController.addButton();
        
        canvasButton.onClick.AddListener( () => {
            showReward();
            canvasButton.onClick.RemoveAllListeners();
        });
    }

    void Start() // Start is called before the first frame update
    {
        narratorController.Invoke("playUnlockAudio", 1f);

        functionToInvoke = null;

        if(AplicationModel.isForestInTemporaryMode)
        {
            switch (AplicationModel.scenesCompleted)
            {
                case 1: functionToInvoke = "loadScene2"; break;
                case 2: functionToInvoke = "loadScene3"; break;
            }

            canvasController.setLockedEffect();
            Invoke(
                "addButtonInCanvas",
                narratorController.UnlockAudio.clip.length + 1f
            );
        }
        canvasController.setRewardsActive();
    }
}
