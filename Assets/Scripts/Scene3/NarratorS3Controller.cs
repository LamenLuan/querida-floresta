using UnityEngine;

public class NarratorS3Controller : NarratorController
{
    [SerializeField] private CanvasS3Controller canvasController;
    [SerializeField] private AudioSource introductionAudio, question1Audio,
    question2Audio, question3Audio, rightAnswerAudio, wrongAnswerAudio,
    sceneCompletedAudio;

    public AudioSource SceneCompletedAudio
    {
        get { return sceneCompletedAudio; }
    }

    public AudioSource RightAnswerAudio
    {
        get { return rightAnswerAudio; }
    }

    private void playNarratorAudio(AudioSource audio)
    {
        canvasController.activateBackgroundCover(audio.clip.length + 1f);
        playAudio(audio);
    }

    public void playQuestion1Audio() // Invoked by playIntroductionAudio()
    {
        playNarratorAudio(question1Audio);
    }
    public void playQuestion2Audio()
    {
        playNarratorAudio(question2Audio);
    }

    public void playQuestion3Audio()
    {
        playNarratorAudio(question3Audio);
    }

    public float playIntroductionAudio()
    {
        playNarratorAudio(introductionAudio);
        Invoke("playQuestion1Audio", introductionAudio.clip.length + 1f);
        return introductionAudio.clip.length + question1Audio.clip.length + 2f;
    }

    public void playRightAnswerAudio()
    {
        playNarratorAudio(rightAnswerAudio);
    }

    public void playWrongAnswerAudio()
    {
        playNarratorAudio(wrongAnswerAudio);
    }

    public void playSceneCompletedAudio()
    {
        playNarratorAudio(sceneCompletedAudio);
    }
}
