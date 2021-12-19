using System;
using UnityEngine;
using UnityEngine.UI;

using DigitalRuby.RainMaker;

public class Scene2Controller : MonoBehaviour
{
    [SerializeField] private Button cowButton, garbageButton, treesButton;
    [SerializeField] private GameObject rainPrefab, cowObject, garbageObject, 
        treesObject;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private SpritesS2Controller spritesController;
    [SerializeField] private CanvasS2Controller canvasController;
    [SerializeField] private Scene2NarratorController narratorController;
    private static float introAudioLength = 27.66f;
    private DateTime timeStarted;
    private RainScript2D rainScript;
    private enum Tcontroller { CANVAS, SPRITE, SELF, SCENE_LOADER }

    public void setFirstTimeInScene() // Called by Button (btQuit)
    {
        AplicationModel.isFirstTimeScene2 = true;
    }

    public void showHelp()
    {
        playANarratorAudio("playHelpAudio", "leaveHelpInterface", 9f);
        canvasController.changeToHelpInterface();
    }

    private void startRaining() // Invoked by in Start()
    {
        rainScript =
            ( Instantiate(rainPrefab) ).GetComponent<RainScript2D>();
        rainScript.RainHeightMultiplier = 0f;
        rainScript.RainWidthMultiplier = 1.12f;
        Destroy(rainScript.gameObject, 8f);
    }

    private void playANarratorAudio(
        string functionToInvoke, string sceneFunction, float length,
        Tcontroller controller = Tcontroller.CANVAS, float awaitTime = 0f
    )
    {
        narratorController.Invoke(functionToInvoke, awaitTime + 1f);
        if(sceneFunction != null)
        {
            length += awaitTime;
            switch (controller)
            {
                case Tcontroller.CANVAS:
                    canvasController.Invoke(sceneFunction, length); break;
                case Tcontroller.SPRITE:
                    spritesController.Invoke(sceneFunction, length); break;
                case Tcontroller.SCENE_LOADER:
                    sceneLoader.Invoke(sceneFunction, length); break;
                
                default: Invoke(sceneFunction, length); break;
            }
        }
    }

    public void sceneMiss(string audioToInvoke, float audioLength)
    {
        AplicationModel.Scene2Misses++;
        playANarratorAudio(
            audioToInvoke, "changeToTryAgainInterface", audioLength
        );
    }

    private void sendDataToReport()
    {
        ReportCreator.writeLine("\nAtividade 2");
        ReportCreator.writeLine(
            $"Quantidade de erros da fase: {AplicationModel.Scene2Misses}"
        );
        ReportCreator.writeResponseTime(AplicationModel.PlayerResponseTime[1]);
    }

    void Start() // Start is called before the first frame update
    {
        timeStarted = DateTime.Now;
        AplicationModel.SceneAcesses[1]++;

        if(AplicationModel.isFirstTimeScene2)
        {
            AplicationModel.isFirstTimeScene2 = false;
            canvasController.showBackgroundCover();
            playANarratorAudio(
                "playIntroductionAudio", "hideBackgroundCover", introAudioLength
            );
        }
        else introAudioLength = 0f;

        Action treesClicked = () => {
            setFirstTimeInScene();
            
            playANarratorAudio(
                "playTreesSelectedAudio", "showTreesRoots", 8.74f,
                Tcontroller.SPRITE
            );
            playANarratorAudio(
                "playAboutRootsAudio", null, 9.89f,
                Tcontroller.SELF, 8.74f
            );
            playANarratorAudio(
                "playAboutEvaporationAudio", "startRaining", 16.56f,
                Tcontroller.SELF, 18.63f
            );
            playANarratorAudio(
                "playAboutRainAudio", "turnSceneGreenAndAnimalsHappy", 8.96f,
                Tcontroller.SPRITE, 35.19f
            );
            playANarratorAudio(
                "playSceneCompletedAudio",
                (AplicationModel.scenesCompleted[1])
                    ? "loadSceneSelection"
                    : "loadPlayersForest",
                9f,
                Tcontroller.SCENE_LOADER, 44.15f
            );

            if(!AplicationModel.scenesCompleted[1]) {
                sendDataToReport();
                AplicationModel.scenesCompleted[1] = true;
            }
        };

        Action<GameObject, Button> buttonClicked = (gameObject, button) => {
            Button[] buttons = {cowButton, treesButton, garbageButton};

            if(
                !AplicationModel.scenesCompleted[1] &&
                AplicationModel.PlayerResponseTime[1] == 0.00000f
            ) {
                AplicationModel.PlayerResponseTime[1] =
                    (DateTime.Now - timeStarted).Seconds - introAudioLength;
            }

            canvasController.showBackgroundCover();
            foreach (Button btn in buttons) btn.onClick.RemoveAllListeners();
            Destroy(button.gameObject);
            gameObject.SetActive(true);
        };

        cowButton.onClick.AddListener( () => {
            buttonClicked(cowObject, cowButton);
            sceneMiss("playCowSelectedAudio", 7.76f);
        });

        garbageButton.onClick.AddListener( () => {
            buttonClicked(garbageObject, garbageButton);
            sceneMiss("playTrashSelectedAudio", 8.49f);
        });

        treesButton.onClick.AddListener( () => {
            buttonClicked(treesObject, treesButton);
            treesClicked();
        });
    }
}
