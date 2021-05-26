using UnityEngine;
using UnityEngine.UI;

public class CanvasNarratorButtonsCotroller : MonoBehaviour
{
    [SerializeField] private GameObject backgroundCoverObj, toucanObj,
    toucanTryAgainObj, levelTxtObj, nextLevelBtnObj, quitBtnObj, helpBtnObj,
    startBtnObj, tryAgainBtnObj, levelSignObj;

    [SerializeField] private Text cloudQuantityTxt;

    public void updateLevelTxt(int level)
    {
        levelTxtObj.GetComponent<Text>().text = "Fase " + (level + 1).ToString();
    }

    public void resetInterface()
    {
        GameObject[] objects = {
            toucanObj, startBtnObj, quitBtnObj, helpBtnObj, backgroundCoverObj,
            levelSignObj, levelTxtObj
        };

        toucanTryAgainObj.SetActive(false);
        nextLevelBtnObj.SetActive(false);
        tryAgainBtnObj.SetActive(false);

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
        GameObject[] objects = {
            quitBtnObj, helpBtnObj, levelTxtObj, toucanObj
        };

        foreach (GameObject item in objects) item.SetActive(false);

        backgroundCoverObj.GetComponent<Image>().color =
            new Vector4(0f, 0f, 0f, 0.55f);

        backgroundCoverObj.SetActive(true);
        toucanTryAgainObj.SetActive(true);
        tryAgainBtnObj.SetActive(true);
    }
}
