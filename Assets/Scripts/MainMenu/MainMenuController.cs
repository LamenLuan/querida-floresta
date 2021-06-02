using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Called by Button (btQuit)
    public void quitGame()
    {
        Application.Quit();
    }
    
    public void startGame()
    {
        SceneManager.LoadScene("Scene 1");
    }
}
