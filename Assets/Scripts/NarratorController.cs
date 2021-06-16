using UnityEngine;

public class NarratorController : MonoBehaviour
{
    private Animator narratorAnimator;
    private void stopSpeakingAnimation() // Invoked by playAudio(...)
    {
        narratorAnimator.SetBool("isSpeaking", false);
    }

    protected void playAudio(AudioSource audio, float secondsToStop)
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
}
