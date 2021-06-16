using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1Controller : MonoBehaviour
{
    [SerializeField] private float timeGap;
    [SerializeField] private GameObject difficultiesObj, rainObjPrefabObj, 
    steamEffectsObj;
    [SerializeField] private CanvasNarratorButtonsCotroller canvasNBCotroller;
    [SerializeField] private Scene1NarratorController narratorController;
    [SerializeField] private AudioSource cloudAudio;
    [SerializeField] private AudioController audioController;
    private bool gameOn;
    private int cloudCounter, cloudNumber, levelCounter;
    private float timeCounter, newHeight;
    private Transform cloudTransform;
    private SpriteRenderer cloudRenderer;
    private GameObject rainObj, difficultyObj;

    private void loadPlayersForest() // Invoked in Start()
    {
        AplicationModel.scenesCompleted++;
        SceneManager.LoadScene("Players Forest");
    }

    public void startGame() // Called by Button (btStart)
    {
        gameOn = true;
        cloudAudio.Play();
        audioController.setMusicVolume(0.01f);
    }

    public void quitScene() // Called by Button (btQuit)
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void showHelp() // Called by Button (btHelp)
    {
        
    }

    private void moveToNextCloud()
    {
        cloudTransform =
            difficultyObj.transform.GetChild(cloudCounter).transform;
        newHeight = cloudTransform.position.y + 5f;

        Transform tranformAux = difficultyObj.transform.GetChild(cloudCounter);
        cloudRenderer = tranformAux.GetComponent<SpriteRenderer>();
    }

    private void playANarratorAudio(
        string audioToInvoke, string canvasFunction, float audioLength)
    {
        narratorController.Invoke(audioToInvoke, 1f);
        if(canvasFunction != null)
            canvasNBCotroller.Invoke(canvasFunction, audioLength);
    }

    private void playMissClickAudio()
    {
        switch (levelCounter)
        {
            case 0: playANarratorAudio(
                "playMissClick1Audio", "enableTryAgainButton", 9f
            ); break;
            case 1: playANarratorAudio(
                "playMissClick2Audio", "enableTryAgainButton", 9f
            ); break;
            case 2: playANarratorAudio(
                "playMissClick3Audio", "enableTryAgainButton", 9f
            ); break;   
        }
    }

    public void checkClickOnCenario() // Called by Button (ImgBackground)
    {
        if(gameOn)
        {
            // Player clicked on the right moment
            if(cloudCounter == cloudNumber)
            {
                audioController.hitSound();
                rainObj = Instantiate(rainObjPrefabObj);
                steamEffectsObj.SetActive(false);
                if(levelCounter < 2)
                {
                    playANarratorAudio(
                        "playRightClickAudio", "showNextLevelButton", 5f
                    );
                }
                // Here the narrator will congratulate the player
                else
                {
                    audioController.sceneCompletedSound();
                    Invoke("loadPlayersForest", 5f);
                }
            }
            // Miss click
            else
            {
                audioController.missSound();
                cloudAudio.Stop();
                canvasNBCotroller.changeToTryAgainInterface();
                playMissClickAudio();
            }
            audioController.setMusicVolume(0.1f);
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

    public void resetLevel() // Called by button (btTryAgain)
    {
        Destroy(this.difficultyObj);
        instantiateDifficulty();

        canvasNBCotroller.resetInterface();
        steamEffectsObj.SetActive(true);
        Destroy(rainObj);

        resetLevelData();
        moveToNextCloud();
    }

    private void playIntroductionAudio()
    {
        if(levelCounter == 1)
            playANarratorAudio("playIntroduction2Audio", "showButtons", 7f);
        else if(levelCounter == 2)
            playANarratorAudio("playIntroduction3Audio", "showButtons", 10f);
    }

    public void goNextLevel() // Called by button (btNextLevel)
    {
        GameObject currentLevel =
            difficultiesObj.transform.GetChild(levelCounter).gameObject;

        // First destroying the clouds of current level
        for (int i = currentLevel.transform.childCount - 1; i >= 0; --i)
            Destroy(currentLevel.transform.GetChild(i).gameObject);

        // Then disabling it before going to the next
        currentLevel.SetActive(false);

        levelCounter++;
        timeGap -= 0.1f;

        resetLevel();
        canvasNBCotroller.hideButtons();
        canvasNBCotroller.updateLevelTxt(levelCounter);
        playIntroductionAudio();
    }
    
    void Start() // Start is called before the first frame update
    {
        instantiateDifficulty();

        resetLevelData();
        levelCounter = 0;

        moveToNextCloud();
        playANarratorAudio("playIntroduction1Audio", "showButtons", 10f);
    }

    void Update() // Update is called once per frame
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
                    playANarratorAudio(
                        "playNotClickedAudio", "enableTryAgainButton", 7f
                    );
                }

                timeCounter = 0f;
                if(cloudCounter < cloudNumber) cloudAudio.Play();
            }
        }
    }
}
