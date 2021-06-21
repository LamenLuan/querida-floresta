using UnityEngine;

public class Scene2NarratorController : NarratorController
{
    [SerializeField] private AudioSource introductionAudio, trashSelectedAudio, 
    cowSelectedAudio, treesSelectedAudio, aboutRootsAudio,
    aboutEvaporationAudio, aboutRainAudio, sceneCompletedAudio;
    [SerializeField] private AudioController audioController;

    /* All invoked by Scene2Controller */
    public void playIntroductionAudio()
    {
        playAudio(introductionAudio, 26.66f);
    }

    public void playTrashSelectedAudio()
    {
        playAudio(trashSelectedAudio, 6.49f);
    }

    public void playCowSelectedAudio()
    {
        playAudio(cowSelectedAudio, 5.76f);
    }

    public void playTreesSelectedAudio()
    {
        playAudio(treesSelectedAudio, 6.74f);
    }

    public void playAboutRootsAudio()
    {
        playAudio(aboutRootsAudio, 7.89f);
    }

    public void playAboutEvaporationAudio()
    {
        playAudio(aboutEvaporationAudio, 14.56f);
    }

    public void playAboutRainAudio()
    {
        playAudio(aboutRainAudio, 6.96f);
    }

    public void playSceneCompletedAudio()
    {
        audioController.sceneCompletedSound();
        playAudio(sceneCompletedAudio, 4f);
    }
}
