using UnityEngine;
using UnityEngine.UI;

public class CanvasS1Controller : MonoBehaviour
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
        Text levelTxt = levelTxtObj.GetComponent<Text>();

        levelTxt.text = "Fase " + (level + 1).ToString();
    }

    private void changeToucanRect(
        float left, float top, float right, float bottom, float minX,
        float minY, float maxX, float maxY)
    {
        RectTransform rect = toucanObj.GetComponent<Image>().rectTransform;

        RectTransformExtensions.SetLeft(rect, left);
        RectTransformExtensions.SetTop(rect, top);
        RectTransformExtensions.SetRight(rect, right);
        RectTransformExtensions.SetBottom(rect, bottom);

        rect.anchorMin = new Vector2(minX, minY);
        rect.anchorMax = new Vector2(maxX, maxY);
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
        
        changeToucanRect(
            -0.000289917f, 0.08061218f, 0.7796173f, -0.7994232f, 0f, 0.319f,
            0.2241268f, 0.61f
        );

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

        changeToucanRect(
            0.478302f, 0.594101f, 0.7182922f, 0.1781006f, 0.3382536f, 0.355f,
            0.6632535f, 0.776826f
        );

        backgroundCoverObj.SetActive(true);
        tryAgainBtnObj.SetActive(true);
    }
}
