using UnityEngine;
using System;

public class Scene1Controller : MonoBehaviour
{
    [SerializeField] private float timeGap;
    [SerializeField] private GameObject difficultiesObj, rainObjPrefabObj, 
    steamEffectsObj;
    [SerializeField] private CanvasNarratorButtonsCotroller canvasNBCotroller;
    [SerializeField] private AudioSource cloudAudio;
    [SerializeField] private  AudioController audioController;
    private bool gameOn;
    private int cloudCounter, cloudNumber, levelCounter;
    private float timeCounter, newHeight;
    private Transform cloudTransform;
    private SpriteRenderer cloudRenderer;
    private GameObject rainObj, difficultyObj;

    // Called by Button (btStart)
    public void startGame()
    {
        gameOn = true;
        cloudAudio.Play();
    }

    // Called by Button (btQuit)
    public void quitGame()
    {
        Application.Quit();
    }

    // Called by Button (btHelp)
    public void showHelp()
    {
        
    }

    private void moveToNextCloud()
    {
        cloudTransform = difficultyObj.transform.GetChild(cloudCounter).transform;
        newHeight = cloudTransform.position.y + 5f;

        cloudRenderer =
            difficultyObj.transform.GetChild(cloudCounter).GetComponent<SpriteRenderer>();
    }

    // Called by Button (ImgBackground)
    public void checkClickOnCenario()
    {
        if(gameOn)
        {
            // Player clicked on the right moment
            if(cloudCounter == cloudNumber)
            {
                audioController.hitSound();
                rainObj = Instantiate(rainObjPrefabObj);
                steamEffectsObj.SetActive(false);
                if(levelCounter < 2) canvasNBCotroller.showNextLevelButton();
            }
            else
            {
                audioController.missSound();
                cloudAudio.Stop();
                canvasNBCotroller.changeToTryAgainInterface();
            }
            gameOn = false;
        }
    }

    private void resetLevelData()
    {
        cloudCounter = 0;
        cloudNumber = difficultyObj.transform.childCount;
        timeCounter = 0f;
        gameOn = false;
    }

    private void instantiateDifficulty()
    {
        GameObject difficultyObj =
            difficultiesObj.transform.GetChild(levelCounter).gameObject;
        difficultyObj.SetActive(true);
        this.difficultyObj = GameObject.Instantiate(difficultyObj);
        difficultyObj.SetActive(false);
    }

    // Called by button (btTryAgain)
    public void resetLevel()
    {
        Destroy(this.difficultyObj);
        instantiateDifficulty();

        canvasNBCotroller.resetInterface();
        steamEffectsObj.SetActive(true);
        Destroy(rainObj);

        resetLevelData();
        moveToNextCloud();
    }

    // Called by button (btNextLevel)
    public void goNextLevel()
    {
        GameObject currentLevel =
            difficultiesObj.transform.GetChild(levelCounter).gameObject;

        Action<GameObject> destroyClouds = (gameObject) => {
            for (int i = gameObject.transform.childCount - 1; i >= 0; --i)
            {
                Destroy(currentLevel.transform.GetChild(i).gameObject);
            }
        };
        
        // First destroying the clouds of current level
        destroyClouds(currentLevel);
        // Then disabling it before going to the next
        currentLevel.SetActive(false);

        levelCounter++;
        timeGap -= 0.1f;

        resetLevel();
        canvasNBCotroller.updateLevelTxt(levelCounter);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        instantiateDifficulty();

        resetLevelData();
        levelCounter = 0;

        moveToNextCloud();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOn)
        {
            if(timeCounter < timeGap)
            {
                timeCounter += Time.deltaTime;

                if(cloudCounter < cloudNumber)
                {
                    cloudTransform.position = new Vector2(
                        cloudTransform.position.x,
                        Mathf.Lerp(-2.68f, newHeight, timeCounter / timeGap)
                    );
                    
                    cloudTransform.localScale = new Vector3(
                        Mathf.Lerp(0.066f, 0.2f, timeCounter / timeGap),
                        Mathf.Lerp(0.093f, 0.22f, timeCounter / timeGap),
                        1f
                    );

                    cloudRenderer.color = new Vector4(
                        255f, 255f, 255f,
                        Mathf.Lerp(0f, 1f, timeCounter / timeGap)
                    );
                }
            }
            else
            {
                canvasNBCotroller.changeQuantityTxt(++cloudCounter);

                if(cloudCounter < cloudNumber)
                {
                    moveToNextCloud();
                }
                else if(cloudCounter > cloudNumber)
                {
                    audioController.missSound();
                    canvasNBCotroller.changeToTryAgainInterface();
                    gameOn = false;
                }

                timeCounter = 0f;
                if(cloudCounter < cloudNumber) cloudAudio.Play();
            }
        }
    }
}
