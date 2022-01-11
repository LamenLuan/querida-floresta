using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorMMController : NarratorController
{
    [SerializeField] private AudioSource speechAudio;
    
    public AudioSource SpeechAudio { get { return speechAudio; } }

    public void playSpeechAudio() {
        playAudio(speechAudio);
    }
}
