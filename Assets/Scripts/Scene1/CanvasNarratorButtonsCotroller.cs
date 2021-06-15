using UnityEngine;
using UnityEngine.UI;

public class CanvasNarratorButtonsCotroller : MonoBehaviour
{
    [SerializeField] private GameObject backgroundCoverObj, toucanObj,
    levelTxtObj, nextLevelBtnObj, quitBtnObj, helpBtnObj, startBtnObj,
    tryAgainBtnObj, levelSignObj;

    [SerializeField] private Text cloudQuantityTxt;

    public void enableTryAgainButton()
    {
        tryAgainBtnObj.GetComponent<Button>().interactable = true;
    }

    public void showButtons() // Invoked in Scene1Controller
    {
        GameObject[] buttons = {startBtnObj, helpBtnObj, quitBtnObj};

        foreach (GameObject buttonObj in buttons) buttonObj.SetActive(true);
    }

    public void hideButtons()
    {
        GameObject[] buttons = {startBtnObj, helpBtnObj, quitBtnObj};

        foreach (GameObject buttonObj in buttons) buttonObj.SetActive(false);
    }

    public void updateLevelTxt(int level)
    {
        levelTxtObj.GetComponent<Text>().text = "Fase " + (level + 1).ToString();
    }

    public void resetInterface()
    {
        GameObject[] objects = {
            startBtnObj, quitBtnObj, helpBtnObj, backgroundCoverObj,
            levelSignObj, levelTxtObj
        };

        tryAgainBtnObj.GetComponent<Button>().interactable = false;
        tryAgainBtnObj.SetActive(false);
        nextLevelBtnObj.SetActive(false);
        
        RectTransform rect = toucanObj.GetComponent<Image>().rectTransform;
        RectTransformExtensions.SetLeft(rect, -0.000289917f);
        RectTransformExtensions.SetTop(rect, 0.08061218f);
        RectTransformExtensions.SetRight(rect, 0.7796173f);
        RectTransformExtensions.SetBottom(rect, -0.7994232f);
        rect.anchorMin = new Vector2(0f, 0.319f);
        rect.anchorMax = new Vector2(0.2241268f, 0.61f);

        backgroundCoverObj.GetComponent<Image>().color =
            new Vector4(1f, 1f, 1f, 0f);            
        cloudQuantityTxt.text = "0";

        foreach (GameObject item in objects) item.SetActive(true);
    }

    public void changeQuantityTxt(int quantity)
    {
        cloudQuantityTxt.GetComponent<Text>().text = quantity.ToString();
    }

    public void showNextLevelButton()
    {
        nextLevelBtnObj.SetActive(true);
    }

    // Called by Button (btStart)
    public void disableStartInterface()
    {
        GameObject[] objects = {
            startBtnObj, quitBtnObj, helpBtnObj, backgroundCoverObj, levelSignObj
        };

        foreach (GameObject item in objects) item.SetActive(false);
    }

    public void changeToTryAgainInterface()
    {
        GameObject[] objects = { quitBtnObj, helpBtnObj, levelTxtObj };

        foreach (GameObject item in objects) item.SetActive(false);

        backgroundCoverObj.GetComponent<Image>().color =
            new Vector4(0f, 0f, 0f, 0.55f);

        RectTransform rect = toucanObj.GetComponent<Image>().rectTransform;
        RectTransformExtensions.SetLeft(rect, 0.478302f);
        RectTransformExtensions.SetTop(rect, 0.594101f);
        RectTransformExtensions.SetRight(rect, 0.7182922f);
        RectTransformExtensions.SetBottom(rect, 0.1781006f);
        rect.anchorMin = new Vector2(0.3382536f, 0.355f);
        rect.anchorMax = new Vector2(0.6632535f, 0.776826f);

        backgroundCoverObj.SetActive(true);
        tryAgainBtnObj.SetActive(true);
    }
}
