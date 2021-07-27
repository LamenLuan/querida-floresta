using UnityEngine;

public class Scene1Controller : MonoBehaviour
{
    [SerializeField] private float timeGap, speedIncrement;
    [SerializeField] private GameObject difficultiesObj, rainObjPrefabObj, 
    steamEffectsObj;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private CanvasS1Controller canvasController;
    [SerializeField] private Scene1NarratorController narratorController;
    [SerializeField] private AudioSource cloudAudio;
    [SerializeField] private AudioController audioController;
    private bool gameOn;
    private int cloudCounter, cloudNumber, levelCounter;
    private byte[] misses;
    private float timeCounter, newHeight;
    private Transform cloudTransform;
    private SpriteRenderer cloudRenderer;
    private GameObject rainObj, difficultyObj;

    // Called by Button (btStart) every level start
    public void startGame() 
    {
        gameOn = true;
        cloudAudio.Play();
        audioController.setMusicVolume(0.01f);
    }

    public void showHelp() // Called by Button (btHelp)
    {
        playANarratorAudio("playHelpAudio", "resetInterface", 13f);
        canvasController.changeToHelpInterface();
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
            canvasController.Invoke(canvasFunction, audioLength);
    }

    private void playMissClickAudio()
    {
        string missClickAudio =
            "playMissClick" + (levelCounter + 1) + "Audio";

        playANarratorAudio(missClickAudio, "enableTryAgainButton", 9f);
    }

    private void sendDataToReport()
    {
        ReportCreator.resetReport();
        ReportCreator.writeLine("Quantidade de erros:");
        ReportCreator.writeLine("Atividade 1");
        for (int i = 0; i < 3; ++i) ReportCreator.writeLine(
            "Questao " + (i + 1) +  ": " + misses[i]
        );
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
                    sendDataToReport();
                    AplicationModel.scenesCompleted++;
                    audioController.sceneCompletedSound();
                    narratorController.Invoke("playCongratsAudio", 0.5f);
                    sceneLoader.Invoke("loadPlayersForest", 9f);
                }
            }
            // Miss click
            else
            {
                misses[levelCounter]++;
                audioController.missSound();
                cloudAudio.Stop();
                canvasController.changeToTryAgainInterface();
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

        canvasController.resetInterface();
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
        timeGap -= speedIncrement;

        resetLevel();
        canvasController.hideButtons();
        canvasController.updateLevelTxt(levelCounter);
        playIntroductionAudio();
    }
    
    void Start() // Start is called before the first frame update
    {
        misses = new byte[3];
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
                canvasController.changeQuantityTxt(++cloudCounter);

                if(cloudCounter < cloudNumber)
                {
                    moveToNextCloud();
                }
                else if(cloudCounter > cloudNumber)
                {
                    audioController.missSound();
                    misses[levelCounter]++;
                    canvasController.changeToTryAgainInterface();
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
