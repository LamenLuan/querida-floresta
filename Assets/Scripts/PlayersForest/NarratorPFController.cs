using UnityEngine;

public class NarratorPFController : NarratorController
{
    [SerializeField] private AudioSource unlockAudio;
    public AudioSource UnlockAudio { get { return unlockAudio; } }
    
    public void playUnlockAudio()
    {
        playAudio(unlockAudio);
    }
}
