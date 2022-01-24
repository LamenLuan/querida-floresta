using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Scene3Controller : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GoogleSheetsController sheetsController;
    [SerializeField] private CanvasS3Controller canvasController;
    [SerializeField] private AudioController audioController;
    [SerializeField] private NarratorS3Controller narratorController;
    [SerializeField] private Button option1Btn, option2Btn, option3Btn,
    word1Btn, word2Btn, repeatQuestionBtn;
    private byte index;
    private DateTime timeStarted;

    private void removeListenerFromButtons() // Invoked in Start()
    {
        Button[] buttons =
            {option1Btn, option2Btn, option3Btn, repeatQuestionBtn};

        foreach(Button btn in buttons) btn.onClick.RemoveAllListeners();
    }

    private void setRightAnswer(Button button)
    {
        canvasController.setAnswerEffect(button, true);
        audioController.hitSound();
        narratorController.playRightAnswerAudio();
        if(index < 3) index++;
    }

    private void setWrongAnswer(Button button)
    {
        canvasController.setAnswerEffect(button, false);
        audioController.missSound();
        narratorController.playWrongAnswerAudio();
        AplicationModel.Scene3Misses[index]++;
    }

    private void sendDataToReport()
    {
        ReportCreator.writeLine("\nAtividade 3");
        ReportCreator.writeMissesPerPhase(AplicationModel.Scene3Misses);
        ReportCreator.writeResponseTime(AplicationModel.PlayerResponseTime[2]);
    }

    void Start() // Start is called before the first frame update
    {
        timeStarted = DateTime.Now;
        AplicationModel.SceneAcesses[2]++;
        index = 0;

        repeatQuestionBtn.onClick.AddListener(
            () => narratorController.playQuestion1Audio()
        );

        AplicationModel.PlayerResponseTime[2] =
            narratorController.playIntroductionAudio();

        Action changeToQuestionThree = () => {
            removeListenerFromButtons(); 
            repeatQuestionBtn.onClick.AddListener(
                () => narratorController.playQuestion3Audio()
            );
            canvasController.Invoke("changeToQuestionThree", 2.5f);

            word1Btn.onClick.AddListener( () => {
                setRightAnswer(word1Btn);
                canvasController.Invoke("levelIsOver", 2.5f);
                narratorController.Invoke(
                    "playSceneCompletedAudio",
                    narratorController.RightAnswerAudio.clip.length + 1f
                );
                sceneLoader.Invoke(
                    (Player.Instance.ScenesCompleted[2])
                        ? "loadSceneSelection"
                        : "loadPlayersForest",
                    narratorController.SceneCompletedAudio.clip.length + 
                    narratorController.RightAnswerAudio.clip.length + 3f
                );
                if(!Player.Instance.ScenesCompleted[2]) {
                    sendDataToReport();
                    Player.Instance.ScenesCompleted[2] = true;
                    new Thread(sheetsController.SavePlayerProgress).Start();
                }
            });
            word2Btn.onClick.AddListener( () => setWrongAnswer(word2Btn) );
        };

        Action changeToQuestionTwo = () => {
            removeListenerFromButtons(); 
            repeatQuestionBtn.onClick.AddListener(
                () => narratorController.playQuestion2Audio()
            );
            canvasController.Invoke("changeToQuestionTwo", 2f);

            option1Btn.onClick.AddListener( () => setWrongAnswer(option1Btn) );
            option2Btn.onClick.AddListener( () => {
                setRightAnswer(option2Btn);
                narratorController.Invoke(
                    "playQuestion3Audio",
                    narratorController.RightAnswerAudio.clip.length + 1f
                );
                changeToQuestionThree();
            });
            option3Btn.onClick.AddListener( () => setWrongAnswer(option3Btn) );
        };

        option1Btn.onClick.AddListener( () => setWrongAnswer(option1Btn) );
        option2Btn.onClick.AddListener( () => setWrongAnswer(option2Btn) );
        option3Btn.onClick.AddListener( () => {
            setRightAnswer(option3Btn);
            narratorController.Invoke(
                "playQuestion2Audio",
                narratorController.RightAnswerAudio.clip.length + 1f
            );
            changeToQuestionTwo();
        });
        
        Button[] buttons = {option1Btn, option2Btn, option3Btn};

        void calculateTimePassed() {
            AplicationModel.PlayerResponseTime[2] =
                (DateTime.Now - timeStarted).Seconds -
                AplicationModel.PlayerResponseTime[2];

            foreach (var button in buttons)
                button.onClick.RemoveListener(calculateTimePassed);
        }

        if(!Player.Instance.ScenesCompleted[2])
            foreach (var button in buttons)
                button.onClick.AddListener(calculateTimePassed);
    }
}
