using UnityEngine;

public class NarratorRegisterController : NarratorController
{
    [SerializeField] private AudioSource typeUserAudio, userRegisteredAudio,
        alreadyRegisteredAudio;
        
    public float TypeUserAudioLength
    {
        get { return typeUserAudio.clip.length; }
    }

    public float AlreadyRegAudioLength
    {
        get { return alreadyRegisteredAudio.clip.length; }
    }

    public float UserRegAudioLength
    {
        get { return userRegisteredAudio.clip.length; }
    }

    public void playTypeUserAudio()
    {
        playAudio(typeUserAudio);
    }

    public void playUserRegisteredAudio()
    {
        playAudio(userRegisteredAudio);
    }

    public void playAlreadyRegisteredAudio()
    {
        playAudio(alreadyRegisteredAudio);
    }

}
