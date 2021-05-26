using System;
using UnityEngine;
using UnityEngine.UI;

public class Scene3Controller : MonoBehaviour
{
    private byte[] tries = {0, 0, 0};
    [SerializeField] private CanvasS3Controller canvasS3Controller;
    [SerializeField] private Button option1Btn, option2Btn, option3Btn,
    word1Btn, word2Btn;
    [SerializeField] private AudioSource hitAudio, missAudio;

    private void destroyHitMissAudios()
    {
        Destroy(hitAudio.gameObject);
        Destroy(missAudio.gameObject);
    }

    private void removeListenerFromButtons()
    {
        Button[] buttons = {option1Btn, option2Btn, option3Btn};
        foreach(Button btn in buttons) btn.onClick.RemoveAllListeners();
    }

    // Start is called before the first frame update
    void Start()
    {
        Action changeToQuestionThree = () => {
            canvasS3Controller.activateBackgroundCover();
            canvasS3Controller.Invoke("changeToQuestionThree", 2f);

            word1Btn.onClick.AddListener( () => {
                hitAudio.Play();
                canvasS3Controller.activateBackgroundCover();
                canvasS3Controller.Invoke("levelIsOver", 2f);
                Invoke("destroyHitMissAudios", 2f);
            });
            word2Btn.onClick.AddListener( () => {
                missAudio.Play();
                tries[2]++;
            });
        };

        Action changeToQuestionTwo = () => {
            canvasS3Controller.activateBackgroundCover();
            removeListenerFromButtons(); 
            canvasS3Controller.Invoke("changeToQuestionTwo", 2f);

            option1Btn.onClick.AddListener( () => {
                missAudio.Play();
                tries[1]++;
            });
            option2Btn.onClick.AddListener( () => {
                hitAudio.Play();
                changeToQuestionThree();
            });
            option3Btn.onClick.AddListener( () => {
                missAudio.Play();
                tries[1]++;
            });
        };

        option1Btn.onClick.AddListener( () => {
            missAudio.Play();
            tries[0]++;
        });
        option2Btn.onClick.AddListener( () => {
            missAudio.Play();
            tries[0]++;
        });
        option3Btn.onClick.AddListener( () => {
            hitAudio.Play();
            changeToQuestionTwo();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
