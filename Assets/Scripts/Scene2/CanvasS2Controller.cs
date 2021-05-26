using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasS2Controller : MonoBehaviour
{
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private GameObject backgroundCoverObj, toucanObj,
    tryAgainToucanObj;

    // Called by the scene controller
    public void changeToTryAgainInterface()
    {
        toucanObj.SetActive(false);
        backgroundCoverObj.SetActive(true);
        tryAgainToucanObj.SetActive(true);
        tryAgainButton.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        tryAgainButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Scene 2");
        });
    }
}
