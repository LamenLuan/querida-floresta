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

        for (int i = rewards.Length - 1; i >= 0; --i)
            if(Player.Instance.ScenesCompleted[i]) rewards[i].SetActive(true);
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
            GameObject[] rewards = {reward1Obj, reward2Obj, reward3Obj};
            int lastIndex = rewards.Length - 1;
           
            if(
                !Player.Instance.ScenesCompleted[lastIndex] &&
                Player.Instance.ScenesCompleted[0]
            ) {
                for (int i = 1; i < rewards.Length; i++) {
                    if(!Player.Instance.ScenesCompleted[i]) {
                        newReward = rewards[i - 1];
                        break;
                    }
                }
            }
            else if(newReward == null) newReward = rewards[lastIndex];
        }
    }
}
