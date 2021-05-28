﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class Scene3Controller : MonoBehaviour
{
    private byte[] tries = {0, 0, 0};
    [SerializeField] private CanvasS3Controller canvasS3Controller;
    [SerializeField] private AudioController audioController;
    [SerializeField] private Button option1Btn, option2Btn, option3Btn,
    word1Btn, word2Btn;

    private void removeListenerFromButtons()
    {
        Button[] buttons = {option1Btn, option2Btn, option3Btn};
        foreach(Button btn in buttons) btn.onClick.RemoveAllListeners();
    }

    private void makeHit(Button button)
    {
        audioController.hitSound();
        canvasS3Controller.setOutline(button, true);
    }

    private void makeMiss(Button button)
    {
        audioController.missSound();
        canvasS3Controller.setOutline(button, false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Action changeToQuestionThree = () => {
            canvasS3Controller.Invoke("changeToQuestionThree", 2f);

            word1Btn.onClick.AddListener( () => {
                audioController.hitSound();
                canvasS3Controller.Invoke("levelIsOver", 2f);
                audioController.Invoke("destroyHitMissAudios", 2f);
            });
            word2Btn.onClick.AddListener( () => {
                audioController.missSound();
                tries[2]++;
            });
        };

        Action changeToQuestionTwo = () => {
            removeListenerFromButtons(); 
            canvasS3Controller.Invoke("changeToQuestionTwo", 2f);

            option1Btn.onClick.AddListener( () => {
                makeMiss(option1Btn);
                tries[1]++;
            });
            option2Btn.onClick.AddListener( () => {
                makeHit(option2Btn);
                changeToQuestionThree();
            });
            option3Btn.onClick.AddListener( () => {
                makeMiss(option3Btn);
                tries[1]++;
            });
        };

        option1Btn.onClick.AddListener( () => {
            makeMiss(option1Btn);
            tries[0]++;
        });
        option2Btn.onClick.AddListener( () => {
            makeMiss(option2Btn);
            tries[0]++;
        });
        option3Btn.onClick.AddListener( () => {
            makeHit(option3Btn);
            changeToQuestionTwo();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
