using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasS3Controller : MonoBehaviour
{
    [SerializeField] private GameObject icon1BtnObj, icon2BtnObj, icon3BtnObj,
    word1Obj, word2Obj, backgroundCoverObj;
    [SerializeField] private Sprite btn1Sprite, btn2Sprite, btn3Sprite;

    IEnumerator ResetOutline(Outline outline)
    {
        yield return new WaitForSeconds(2f);
        try
        {
            outline.effectColor = new Color(0.312f, 0.208f, 0f);
        }
        catch (System.Exception) {}
    }

    public void setOutline(Button button, bool hit)
    {
        Outline outline = button.gameObject.GetComponent<Outline>();
        
        outline.effectColor = hit
            ? new Color(0f, 1f, 0f)
            : new Color(1f, 0f, 0f)
        ;
        
        StartCoroutine( ResetOutline(outline) );
    }

    // Invoked by activateBackgroundCover()
    public void deactivateBackgroundCover()
    {
        backgroundCoverObj.SetActive(false);
    }

    public void activateBackgroundCover()
    {
        backgroundCoverObj.SetActive(true);
        Invoke("deactivateBackgroundCover", 2f);
    } 

    // Invoked by scene controller
    public void changeToQuestionTwo()
    {
        icon1BtnObj.GetComponent<Image>().sprite = btn1Sprite;
        icon2BtnObj.GetComponent<Image>().sprite = btn2Sprite;
        icon3BtnObj.GetComponent<Image>().sprite = btn3Sprite;
    }

    // Invoked by scene controller
    public void changeToQuestionThree()
    {
        Destroy(icon1BtnObj);
        Destroy(icon2BtnObj);
        Destroy(icon3BtnObj);

        word1Obj.SetActive(true);
        word2Obj.SetActive(true);
    }

    // Invoked by scene controller
    public void levelIsOver()
    {
        Destroy(word1Obj);
        Destroy(word2Obj);
        Destroy(backgroundCoverObj);
    }
}
