using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayersForestController : MonoBehaviour
{
    [SerializeField] private GameObject reward1, reward2, reward3, canvas;
    [SerializeField] private AudioController audioController;

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
        GameObject[] rewards = {reward1, reward2, reward3};

        for (int i = 0; i < AplicationModel.scenesCompleted; i++)
            rewards[i].SetActive(true);
    }

    private void showReward() // Invoked in Start()
    {
        for (int i = 0; i < newReward.transform.childCount; ++i)
        {
            newReward.transform.GetChild(i).GetComponent<Image>().color =
                new Vector4(1f, 1f, 1f, 1f);
        }
        audioController.sceneCompletedSound();
    }

    void Start() // Start is called before the first frame update
    {
        string functionToInvoke = null;
        
        if(AplicationModel.isForestInTemporaryMode)
        {
            switch (AplicationModel.scenesCompleted)
            {
                case 1:
                    newReward = reward1;
                    functionToInvoke = "loadScene2";
                    break;
                case 2:
                    newReward = reward2;
                    functionToInvoke = "loadScene3";
                    break;
                case 3:
                    newReward = reward3;
                    break;
            }

            for (int i = 0; i < newReward.transform.childCount; ++i)
            {
                newReward.transform.GetChild(i).GetComponent<Image>().color =
                    new Vector4(0f, 0f, 0f, 1f);
            }
        }
        setRewardsActive();
        Invoke("showReward", 3f);
        if(functionToInvoke != null) Invoke(functionToInvoke, 8f);
    }
}
