using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasS2Controller : MonoBehaviour
{
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private GameObject backgroundCoverObj, toucanObj,
    tryAgainToucanObj, helpBtnObj, quitBtnObj, helpSignObj;

    public void hideBackgroundCover() // Invoked by Scene2Controller
    {
        backgroundCoverObj.SetActive(false);
    }

    public void showBackgroundCover() // Invoked by Scene2Controller
    {
        backgroundCoverObj.SetActive(true);
    }

    public void changeToTryAgainInterface() // Called by Scene2Controller
    {
        toucanObj.SetActive(false);
        backgroundCoverObj.GetComponent<Image>().color =
            new Vector4(0f, 0f, 0f, 0.5f);

        showBackgroundCover();
        tryAgainToucanObj.SetActive(true);
        tryAgainButton.gameObject.SetActive(true);
    }

    private void setBtnsAndSignActive(bool isHelpInterfaceOn)
    {
        helpBtnObj.SetActive(!isHelpInterfaceOn);
        quitBtnObj.SetActive(!isHelpInterfaceOn);
        helpSignObj.SetActive(isHelpInterfaceOn);
    }

    public void changeToHelpInterface()
    {
        showBackgroundCover();
        setBtnsAndSignActive(true);
    }

    public void leaveHelpInterface()
    {
        setBtnsAndSignActive(false);
        hideBackgroundCover();
    }

    void Start() // Start is called before the first frame update
    {
        tryAgainButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Scene 2");
        });
    }
}
