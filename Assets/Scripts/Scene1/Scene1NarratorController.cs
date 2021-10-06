using UnityEngine;

public class Scene1NarratorController : NarratorController
{
    [SerializeField] private AudioSource introduction1Audio, introduction2Audio,
    introduction3Audio, missClick1Audio, missClick2Audio, missClick3Audio,
    rightClickAudio, notClickedAudio, helpAudio, congratsAudio;
    
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

    public void playCongratsAudio()
    {
        playAudio(congratsAudio);
    }

    public void changeToLessCloudsAudios()
    {
        missClick2Audio.clip = Resources.Load<AudioClip>("cliqueErradoFase2");
        missClick3Audio.clip = Resources.Load<AudioClip>("cliqueErradoFase3");
        introduction2Audio.clip = Resources.Load<AudioClip>("introducaoFase2");
        introduction3Audio.clip = Resources.Load<AudioClip>("introducaoFase3");
    }
}
