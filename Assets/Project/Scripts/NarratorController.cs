using UnityEngine;

public class NarratorController : MonoBehaviour
{
    private Animator narratorAnimator;
    private void stopSpeakingAnimation() // Invoked by playAudio(...)
    {
        narratorAnimator.SetBool("isSpeaking", false);
    }

    protected void playAudio(AudioSource audio)
    {
        try {
            narratorAnimator.SetBool("isSpeaking", true);
        }
        catch(System.NullReferenceException) {
            narratorAnimator = GetComponent<Animator>();
            narratorAnimator.SetBool("isSpeaking", true);
        }
        audio.PlayDelayed(0.6f);
        Invoke("stopSpeakingAnimation", audio.clip.length);
    }
}
