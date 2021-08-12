using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        if(!AplicationModel.scenesCompleted[0]) {
            startButton.onClick.AddListener(
                () => sceneLoader.loadScene1()
            );
        }
        else {
            startButton.GetComponentInChildren<Text>().text = "ATIVIDADES";
            startButton.onClick.AddListener(
                () => sceneLoader.loadSceneSelection()
            );
        }
    }
}
