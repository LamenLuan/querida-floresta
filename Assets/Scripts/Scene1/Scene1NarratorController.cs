using UnityEngine;

public class Scene1NarratorController : NarratorController
{
    [SerializeField] public AudioSource introduction1Audio, introduction2Audio,
    introduction3Audio, missClick1Audio, missClick2Audio, missClick3Audio,
    rightClickAudio, notClickedAudio, helpAudio;
    
    /* All invoked by Scene1Controller */
    
    public void playIntroduction1Audio()
    {
        playAudio(introduction1Audio);
    }

    public void playIntroduction2Audio()
    {
        playAudio(introduction2Audio);
    }

    public void playIntroduction3Audio()
    {
        playAudio(introduction3Audio);
    }

    public void playRightClickAudio()
    {
        playAudio(rightClickAudio);
    }

    public void playMissClick1Audio()
    {
        playAudio(missClick1Audio);
    }

    public void playMissClick2Audio()
    {
        playAudio(missClick2Audio);
    }

    public void playMissClick3Audio()
    {
        playAudio(missClick3Audio);
    }

    public void playNotClickedAudio()
    {
        playAudio(notClickedAudio);
    }

    public void playHelpAudio()
    {
        playAudio(helpAudio);
    }
}
