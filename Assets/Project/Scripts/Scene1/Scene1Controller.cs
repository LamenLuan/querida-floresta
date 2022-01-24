using UnityEngine;
using System;
using System.Threading;

public class Scene1Controller : MonoBehaviour
{
    [SerializeField] private float timeGap, speedIncrement;
    [SerializeField] private GameObject difficultiesObj, rainObjPrefabObj, 
    steamEffectsObj;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GoogleSheetsController sheetsController;
    [SerializeField] private CanvasS1Controller canvasController;
    [SerializeField] private Scene1NarratorController narratorController;
    [SerializeField] private AudioSource cloudAudio;
    [SerializeField] private AudioController audioController;
    private bool gameOn, lessClouds;
    private int cloudCounter, cloudNumber, levelCounter;
    private const float introAudioLength = 10.67f;
    private float timeCounter, newHeight;
    private DateTime timeStarted, timeEnded;
    private Transform cloudTransform;
    private SpriteRenderer cloudRenderer;
    private GameObject rainObj, difficultyObj;
    private MusicPlayer musicPlayer;

    // Called by Button (btStart) every level start
    public void startGame() 
    {
        gameOn = true;
        cloudAudio.Play();
        musicPlayer.setMusicVolume(0.01f);
        
        if(levelCounter == 0 && AplicationModel.Scene1Misses[0] == 0) {
            timeEnded = DateTime.Now;
            AplicationModel.PlayerResponseTime[0] =
                (timeEnded - timeStarted).TotalSeconds - introAudioLength;
        }  
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
        ReportCreator.writeLine("Atividade 1");
        ReportCreator.writeMissesPerPhase(AplicationModel.Scene1Misses);
        ReportCreator.writeResponseTime(AplicationModel.PlayerResponseTime[0]);
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
                    narratorController.Invoke("playCongratsAudio", 0.5f);
                    sceneLoader.Invoke(
                        (Player.Instance.ScenesCompleted[0])
                            ? "loadSceneSelection"
                            : "loadPlayersForest",
                        9f
                    );
                    if(!Player.Instance.ScenesCompleted[0]) {
                        sendDataToReport();
                        Player.Instance.ScenesCompleted[0] = true;
                        new Thread(sheetsController.SavePlayerProgress).Start();
                    }
                }
            }
            // Miss click
            else
            {
                if(
                    ++AplicationModel.Scene1Misses[levelCounter] > 4 &&
                    !AplicationModel.lessClouds
                ) setLessClouds();
                audioController.missSound();
                cloudAudio.Stop();
                canvasController.changeToTryAgainInterface();
                playMissClickAudio();
            }
            musicPlayer.setMusicVolume(0.1f);
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
    
    private void setLessClouds()
    {
        // For the second and third cloud group
        for(int i = 1; i < 3; ++i)
        {
            // Selecting the group object
            GameObject currentLevel =
                difficultiesObj.transform.GetChild(i).gameObject;
            // Calculating the last index of cloud array
            int childQuant = currentLevel.transform.childCount - 1;
            // Removing 2 cloud from group 2, 3 clouds from group 3
            for (int j = 0; j < i + 1; ++j)
            {
                Destroy(
                    currentLevel.transform.GetChild(childQuant - j).gameObject
                );
            }
        }

        cloudNumber = difficultyObj.transform.childCount;
        AplicationModel.lessClouds = true;
        narratorController.changeToLessCloudsAudios();
    }

    void Start() // Start is called before the first frame update
    {
        timeStarted = DateTime.Now;
        AplicationModel.SceneAcesses[0]++;
        instantiateDifficulty();
        musicPlayer = GameObject.FindGameObjectWithTag(
            "Music"
        ).GetComponent<MusicPlayer>();

        resetLevelData();
        levelCounter = 0;

        moveToNextCloud();
        playANarratorAudio(
            "playIntroduction1Audio", "showButtons", introAudioLength
        );
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
                    AplicationModel.Scene1Misses[levelCounter]++;
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
