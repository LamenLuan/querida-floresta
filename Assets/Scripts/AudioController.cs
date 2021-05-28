using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClickedAudio, hitAudio, missAudio;


    // Invoked in the other controllers
    public void destroyHitMissAudios()
    {
        Destroy(hitAudio.gameObject);
        Destroy(missAudio.gameObject);
    }

    // Called by buttons
    public void buttonClickedSound()
    {
        buttonClickedAudio.Play();
    }

    public void hitSound()
    {
        hitAudio.Play();
    }

    public void missSound()
    {
        missAudio.Play();
    }
}