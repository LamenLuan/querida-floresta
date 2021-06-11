using System;
using UnityEngine;

public class Scene1NarratorAudiosController : MonoBehaviour
{
    [SerializeField] public AudioSource hitRightTimeAudio;

    private Animator narratorAnimator;
    
    public void stophitRightTimeAudio()
    {
        narratorAnimator.SetBool("isSpeaking", false);
    }

    public void playhitRightTimeAudio()
    {
        narratorAnimator.SetBool("isSpeaking", true);
        hitRightTimeAudio.Play();
        Invoke("stophitRightTimeAudio", 4f);
    }

    public bool ishitRightTimeAudioPlaying()
    {
        return hitRightTimeAudio.isPlaying;
    }

    void Start() // Start is called before the first frame update
    {
        narratorAnimator = GetComponent<Animator>();
    }
}
