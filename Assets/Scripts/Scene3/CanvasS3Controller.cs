using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CanvasS3Controller : MonoBehaviour
{
    [SerializeField] private GameObject icon1BtnObj, icon2BtnObj, icon3BtnObj,
    word1Obj, word2Obj, backgroundCoverObj, quitButtonObj, repeatQuestionBtnObj;
    [SerializeField] private Sprite btn1Sprite, btn2Sprite, btn3Sprite;

    IEnumerator ResetEffect(Component component, bool isOutline)
    {
        yield return new WaitForSeconds(2f);
        
        if(isOutline)
            ((Outline) component).effectColor = new Color(0.312f, 0.208f, 0f);
        else
            ((Image) component).color = new Color(1f, 1f, 1f);
    }

    public void setAnswerEffect(Button button, bool isRightAnswer)
    {
        Func<Color> getAnswerEffect = () => {
            return isRightAnswer
                ? new Color(0f, 1f, 0f)
                : new Color(1f, 0f, 0f)
            ;
        };

        Outline outline = button.gameObject.GetComponent<Outline>();

        if(outline != null)
        {
            outline.effectColor = getAnswerEffect();
            StartCoroutine( ResetEffect(outline, true) );
        }
        else {
            Image image = button.gameObject.GetComponent<Image>();
            image.color = getAnswerEffect();
            StartCoroutine( ResetEffect(image, false) );
        }
    }

    // Invoked by activateBackgroundCover()
    public void deactivateBackgroundCover() 
    {
        backgroundCoverObj.SetActive(false);
    }

    // Invoked by the option buttons
    public void activateBackgroundCover(float timeToDeactivate) 
    {
        backgroundCoverObj.SetActive(true);
        Invoke("deactivateBackgroundCover", timeToDeactivate);
    }

    public void changeToQuestionTwo() // Invoked by Scene3Controller
    {
        icon1BtnObj.GetComponent<Image>().sprite = btn1Sprite;
        icon2BtnObj.GetComponent<Image>().sprite = btn2Sprite;
        icon3BtnObj.GetComponent<Image>().sprite = btn3Sprite;
    }

    public void changeToQuestionThree() // Invoked by Scene3Controller
    {
        GameObject[] objects = {icon1BtnObj, icon2BtnObj, icon3BtnObj};
        foreach (var obj in objects) Destroy(obj);

        word1Obj.SetActive(true);
        word2Obj.SetActive(true);
    }

    public void levelIsOver() // Invoked by Scene3Controller
    {
        GameObject[] objects = {
            repeatQuestionBtnObj, quitButtonObj, word1Obj , word2Obj,
        };
        foreach (var obj in objects) Destroy(obj);
    }
}
