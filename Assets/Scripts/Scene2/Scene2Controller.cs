using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using DigitalRuby.RainMaker;

public class Scene2Controller : MonoBehaviour
{
    [SerializeField] private Button cowButton, garbageButton, treesButton;
    [SerializeField] private GameObject rainPrefab, cowObject, garbageObject, 
        treesObject;
    [SerializeField] private SpritesS2Controller spritesController;
    [SerializeField] private CanvasS2Controller canvasController;
    private RainScript2D rainScript;

    // Called by Button (btQuit)
    public void quitScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void loadScene3()
    {
        SceneManager.LoadScene("Scene 3");
    }

    // Invoked by treesClicked() delegate in Start()
    private void startRaining()
    {
        rainScript =
            ( Instantiate(rainPrefab) ).GetComponent<RainScript2D>();
        rainScript.RainHeightMultiplier = 0f;
        rainScript.RainWidthMultiplier = 1.12f;
        Destroy(rainScript.gameObject, 8f);
    }

    private void changingToTryAgainInterface(float time)
    {
        canvasController.Invoke("changeToTryAgainInterface", time);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Using lamba here to remove the buttons's listeners when one of them
        // is clicked
        Action cowClicked = () => changingToTryAgainInterface(3f),
        garbageClicked = () => changingToTryAgainInterface(3f),
        treesClicked = () => {
            spritesController.Invoke("showTreesRoots", 3f);
            Invoke("startRaining", 6f);
            spritesController.Invoke("turnSceneGreen", 11f);
            spritesController.Invoke("makeAnimalsHappy", 11f);
            Invoke("loadScene3", 16f);
        };

        Action<GameObject, Button> buttonClicked = (gameObject, button) => {
            cowButton.onClick.RemoveAllListeners();
            garbageButton.onClick.RemoveAllListeners();
            treesButton.onClick.RemoveAllListeners();

            Destroy(button.gameObject);
            gameObject.SetActive(true);
        };

        cowButton.onClick.AddListener( () => {
            buttonClicked(cowObject, cowButton);
            cowClicked();
        });

        garbageButton.onClick.AddListener( () => {
            buttonClicked(garbageObject, garbageButton);
            garbageClicked();
        });

        treesButton.onClick.AddListener( () => {
            buttonClicked(treesObject, treesButton);
            treesClicked();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
