using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Called by Button (btQuit)
    public void quitGame()
    {
        Application.Quit();
    }
    
    public void loadScene1()
    {
        AplicationModel.isFirstTimeScene2 = true;
        SceneManager.LoadScene("Scene 1");
    }
}
