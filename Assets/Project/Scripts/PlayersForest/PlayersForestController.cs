using UnityEngine;
using UnityEngine.UI;

public class PlayersForestController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private CanvasPFController canvasController;
    [SerializeField] private AudioController audioController;
    [SerializeField] private NarratorPFController narratorController;
    private string functionToInvoke;

    private void showReward()
    {
        canvasController.removeLockedEffect();
        audioController.sceneCompletedSound();
        if(functionToInvoke != null) sceneLoader.Invoke(functionToInvoke, 8f);
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
        if(
            AplicationModel.isForestInTemporaryMode &&
            Player.Instance.ScenesCompleted[0]
        ) {
            narratorController.Invoke("playUnlockAudio", 1f);
            functionToInvoke = null;

            if(Player.Instance.ScenesCompleted[2])
                functionToInvoke = "loadSceneSelection";
            else if(Player.Instance.ScenesCompleted[1])
                functionToInvoke = "loadScene3";
            else if(Player.Instance.ScenesCompleted[0])
                functionToInvoke = "loadScene2";

            canvasController.setLockedEffect();
            Invoke(
                "addButtonInCanvas",
                narratorController.UnlockAudio.clip.length + 1f
            );
        }
        canvasController.setRewardsActive();
    }
}
