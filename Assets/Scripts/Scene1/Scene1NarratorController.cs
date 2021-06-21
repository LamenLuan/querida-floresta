using UnityEngine;

public class Scene1NarratorController : NarratorController
{
    [SerializeField] public AudioSource introduction1Audio, introduction2Audio,
    introduction3Audio, missClick1Audio, missClick2Audio, missClick3Audio,
    rightClickAudio, notClickedAudio;
    
    /* All invoked by Scene1Controller */
    
    public void playIntroduction1Audio()
    {
        playAudio(introduction1Audio, 9f);
    }

    public void playIntroduction2Audio()
    {
        playAudio(introduction2Audio, 6f);
    }

    public void playIntroduction3Audio()
    {
        playAudio(introduction3Audio, 9f);
    }

    public void playRightClickAudio()
    {
        playAudio(rightClickAudio, 4f);
    }

    public void playMissClick1Audio()
    {
        playAudio(missClick1Audio, 8f);
    }

    public void playMissClick2Audio()
    {
        playAudio(missClick2Audio, 8f);
    }

    public void playMissClick3Audio()
    {
        playAudio(missClick3Audio, 8f);
    }

    public void playNotClickedAudio()
    {
        playAudio(notClickedAudio, 6f);
    }
}
