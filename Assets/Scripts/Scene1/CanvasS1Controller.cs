using UnityEngine;
using UnityEngine.UI;

public class CanvasS1Controller : MonoBehaviour
{
    [SerializeField] private GameObject backgroundCoverObj, toucanObj,
    levelTxtObj, nextLevelBtnObj, quitBtnObj, helpBtnObj, startBtnObj,
    tryAgainBtnObj, levelSignObj, mouseObj;
    [SerializeField] private Text cloudQuantityTxt;

    public void enableTryAgainButton() // Invoked by Scene1Controller
    {
        tryAgainBtnObj.GetComponent<Button>().interactable = true;
    }

    public void showNextLevelButton() // Invoked by Scene1Controller
    {
        nextLevelBtnObj.SetActive(true);
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

    public void disableStartInterface() // Called by Button (btStart)
    {
        hideButtons();
        levelSignObj.SetActive(false);
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
    
    public void changeQuantityTxt(int quantity)
    {
        cloudQuantityTxt.text = quantity.ToString();
    }

    public void resetInterface()
    {
        GameObject[] objectsToActivate = {levelSignObj, levelTxtObj};

        GameObject[] objectsToDeactivate = 
            {backgroundCoverObj, tryAgainBtnObj, nextLevelBtnObj, mouseObj};

        tryAgainBtnObj.GetComponent<Button>().interactable = false;
        foreach (GameObject item in objectsToDeactivate) item.SetActive(false);
        
        changeToucanRect(
            -0.000289917f, 0.08061218f, 0.7796173f, -0.7994232f, 0f, 0.319f,
            0.2241268f, 0.61f
        );
              
        changeQuantityTxt(0);
        showButtons();
        foreach (GameObject item in objectsToActivate) item.SetActive(true);
    }

    public void changeToTryAgainInterface()
    {
        GameObject[] objects = { quitBtnObj, helpBtnObj, levelTxtObj };

        foreach (GameObject item in objects) item.SetActive(false);

        changeToucanRect(
            0.478302f, 0.594101f, 0.7182922f, 0.1781006f, 0.3382536f, 0.355f,
            0.6632535f, 0.776826f
        );

        backgroundCoverObj.SetActive(true);
        tryAgainBtnObj.SetActive(true);
    }

    public void changeToHelpInterface()
    {
        hideButtons();
        levelSignObj.SetActive(false);
        mouseObj.SetActive(true);
    }
}
