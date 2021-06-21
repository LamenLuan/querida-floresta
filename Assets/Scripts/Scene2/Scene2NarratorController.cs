using UnityEngine;

public class Scene2NarratorController : NarratorController
{
    [SerializeField] private AudioSource introductionAudio, trashSelectedAudio, 
    cowSelectedAudio;

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

}
