using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayersForestController : MonoBehaviour
{
    [SerializeField] private GameObject reward1Obj, reward2Obj, reward3Obj,
    canvasObj, imgLevelSignObj;
    [SerializeField] private AudioController audioController;
    private string functionToInvoke;
    private GameObject newReward;

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

    private void setRewardsActive()
    {
        GameObject[] rewards = {reward1Obj, reward2Obj, reward3Obj};

        for (int i = 0; i < AplicationModel.scenesCompleted; i++)
            rewards[i].SetActive(true);
    }

    private void showReward()
    {
        // Removing the visual effect to show new reward
        for (int i = 0; i < newReward.transform.childCount; ++i)
        {
            newReward.transform.GetChild(i).GetComponent<Image>().color =
                new Vector4(1f, 1f, 1f, 1f);
        }
        audioController.sceneCompletedSound();
        if(functionToInvoke != null) Invoke(functionToInvoke, 8f);
    }

    private void addButtonInCanvas() // Invoked in Start()
    {
        Button canvasButton = canvasObj.AddComponent<Button>();
        canvasButton.transition = Selectable.Transition.None;
        canvasButton.onClick.AddListener( () => {
            showReward();
            imgLevelSignObj.SetActive(false);
            canvasButton.onClick.RemoveAllListeners();
        });
    }

    private void setLockedEffectOnNewReward()
    {
        for (int i = 0; i < newReward.transform.childCount; ++i)
        {
            newReward.transform.GetChild(i).GetComponent<Image>().color =
                new Vector4(0f, 0f, 0f, 1f);
        }
    }

    void Start() // Start is called before the first frame update
    {
        functionToInvoke = null;

        if(AplicationModel.isForestInTemporaryMode)
        {
            switch (AplicationModel.scenesCompleted)
            {
                case 1:
                    newReward = reward1Obj;
                    functionToInvoke = "loadScene2";
                    break;
                case 2:
                    newReward = reward2Obj;
                    functionToInvoke = "loadScene3";
                    break;
                case 3:
                    newReward = reward3Obj;
                    break;
            }

            setLockedEffectOnNewReward();
            imgLevelSignObj.SetActive(true);
            Invoke("addButtonInCanvas", 1f);
        }
        setRewardsActive();
    }
}
