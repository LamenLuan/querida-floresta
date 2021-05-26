using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClickedAudio;

    public void buttonClickedSound()
    {
        buttonClickedAudio.Play();
    }
}