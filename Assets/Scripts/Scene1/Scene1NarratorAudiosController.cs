using System;
using UnityEngine;

public class Scene1NarratorAudiosController : MonoBehaviour
{
    [SerializeField] public AudioSource introduction1Audio, introduction2Audio,
    introduction3Audio, missClick1Audio, missClick2Audio, missClick3Audio,
    hitRightTimeAudio;

    private Animator narratorAnimator;
    
    private void stopSpeakingAnimation() // Invoked by playAudio(...)
    {
        narratorAnimator.SetBool("isSpeaking", false);
    }

    private void playAudio(AudioSource audio, float secondsToStop)
    {
        try
        {
            narratorAnimator.SetBool("isSpeaking", true);
        }
        catch (System.NullReferenceException)
        {
            narratorAnimator = GetComponent<Animator>();
            narratorAnimator.SetBool("isSpeaking", true);
        }
        audio.PlayDelayed(0.4f);
        Invoke("stopSpeakingAnimation", secondsToStop);
    }

    public void playIntroduction1Audio() // Invoked by Scene1Controller
    {
        playAudio(introduction1Audio, 9f);
    }

    public void playIntroduction2Audio() // Invoked by Scene1Controller
    {
        playAudio(introduction2Audio, 6f);
    }

    public void playIntroduction3Audio() // Invoked by Scene1Controller
    {
        playAudio(introduction3Audio, 9f);
    }

    public void playhitRightTimeAudio() // Invoked by Scene1Controller
    {
        playAudio(hitRightTimeAudio, 4f);
    }

    public void playMissClick1Audio() // Invoked by Scene1Controller
    {
        playAudio(missClick1Audio, 8f);
    }

    public void playMissClick2Audio() // Invoked by Scene1Controller
    {
        playAudio(missClick2Audio, 8f);
    }

    public void playMissClick3Audio() // Invoked by Scene1Controller
    {
        playAudio(missClick3Audio, 8f);
    }
}
