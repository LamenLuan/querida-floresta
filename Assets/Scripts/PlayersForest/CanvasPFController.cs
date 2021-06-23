using UnityEngine;
using UnityEngine.UI;

public class CanvasPFController : MonoBehaviour
{
    [SerializeField] private GameObject reward1Obj, reward2Obj, reward3Obj,
    imgLevelSignObj;
    GameObject newReward;

    public void setRewardsActive()
    {
        GameObject[] rewards = {reward1Obj, reward2Obj, reward3Obj};

        for (int i = 0; i < AplicationModel.scenesCompleted; i++)
            rewards[i].SetActive(true);
    }

    public void setLockedEffect()
    {
        if(newReward != null)
        {
            for (int i = 0; i < newReward.transform.childCount; ++i)
            {
                newReward.transform.GetChild(i).GetComponent<Image>().color =
                    new Vector4(0f, 0f, 0f, 1f);
            }
        }
    }

    public void removeLockedEffect()
    {
        if(newReward != null)
        {
            for (int i = 0; i < newReward.transform.childCount; ++i)
            {
                newReward.transform.GetChild(i).GetComponent<Image>().color =
                    new Vector4(1f, 1f, 1f, 1f);
            }
        }
    }

    public Button addButton() // Invoked in Start()
    {
        Button canvasButton = gameObject.AddComponent<Button>();

        canvasButton.transition = Selectable.Transition.None;
        canvasButton.onClick.AddListener( () => 
            imgLevelSignObj.SetActive(false)
        );
        imgLevelSignObj.SetActive(true);

        return canvasButton;
    }

    void Start() // Start is called before the first frame update
    {
        if(AplicationModel.isForestInTemporaryMode)
        {
            switch (AplicationModel.scenesCompleted)
            {
                case 1: newReward = reward1Obj; break;
                case 2: newReward = reward2Obj; break;
                case 3: newReward = reward3Obj; break;
            }
        }
    }
}
