using System;
using UnityEngine;

public class Scene1NarratorAudiosController : MonoBehaviour
{
    [SerializeField] public AudioSource hitRightTimeAudio;

    private Animator narratorAnimator;
    
    public void stophitRightTimeAudio() // Invoked by playhitRightTimeAudio()
    {
        narratorAnimator.SetBool("isSpeaking", false);
    }

    public void playhitRightTimeAudio() // Invoked by Scene1Controller
    {
        narratorAnimator.SetBool("isSpeaking", true);
        hitRightTimeAudio.Play();
        Invoke("stophitRightTimeAudio", 4f);
    }

    void Start() // Start is called before the first frame update
    {
        narratorAnimator = GetComponent<Animator>();
    }
}
