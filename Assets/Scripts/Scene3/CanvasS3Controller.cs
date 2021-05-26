using UnityEngine;
using UnityEngine.UI;

public class CanvasS3Controller : MonoBehaviour
{
    [SerializeField] private GameObject icon1BtnObj, icon2BtnObj, icon3BtnObj,
    word1Obj, word2Obj, backgroundCoverObj;
    [SerializeField] private Sprite btn1Sprite, btn2Sprite, btn3Sprite;

    public void activateBackgroundCover()
    {
        backgroundCoverObj.SetActive(true);
    }

    public void deactivateBackgroundCover()
    {
        backgroundCoverObj.SetActive(false);
    }

    // Invoked by scene controller
    public void changeToQuestionTwo()
    {
        icon1BtnObj.GetComponent<Image>().sprite = btn1Sprite;
        icon2BtnObj.GetComponent<Image>().sprite = btn2Sprite;
        icon3BtnObj.GetComponent<Image>().sprite = btn3Sprite;

        backgroundCoverObj.SetActive(false);
    }

    // Invoked by scene controller
    public void changeToQuestionThree()
    {
        Destroy(icon1BtnObj);
        Destroy(icon2BtnObj);
        Destroy(icon3BtnObj);

        word1Obj.SetActive(true);
        word2Obj.SetActive(true);

        backgroundCoverObj.SetActive(false);
    }

    // Invoked by scene controller
    public void levelIsOver()
    {
        Destroy(word1Obj);
        Destroy(word2Obj);
        Destroy(backgroundCoverObj);
    }
}
