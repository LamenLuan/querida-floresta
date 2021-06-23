using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene3Controller : MonoBehaviour
{
    [SerializeField] private CanvasS3Controller canvasController;
    [SerializeField] private AudioController audioController;
    [SerializeField] private NarratorS3Controller narratorController;
    [SerializeField] private Button option1Btn, option2Btn, option3Btn,
    word1Btn, word2Btn;
    private byte[] tries;
    private byte index;

    private void loadPlayersForest() // Invoked in Start()
    {
        AplicationModel.scenesCompleted++;
        SceneManager.LoadScene("Players Forest");
    }

    public void quitScene() // Called by Button (btQuit)
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void removeListenerFromButtons() // Invoked in Start()
    {
        Button[] buttons = {option1Btn, option2Btn, option3Btn};
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
        tries[index]++;
    }

    void Start() // Start is called before the first frame update
    {
        tries = new Byte[3];
        index = 0;

        narratorController.playIntroductionAudio();

        Action changeToQuestionThree = () => {
            canvasController.Invoke("changeToQuestionThree", 2.5f);

            word1Btn.onClick.AddListener( () => {
                setRightAnswer(word1Btn);
                canvasController.Invoke("levelIsOver", 2.5f);
                narratorController.playSceneCompletedAudio();
                Invoke(
                    "loadPlayersForest",
                    narratorController.SceneCompletedAudio.clip.length + 3f
                );
            });
            word2Btn.onClick.AddListener( () => setWrongAnswer(word2Btn) );
        };

        Action changeToQuestionTwo = () => {
            removeListenerFromButtons(); 
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
    }
}
