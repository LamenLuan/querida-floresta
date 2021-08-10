using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void loadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void loadScene1()
    {
        SceneManager.LoadScene("Scene 1");
    }
    public void loadScene2()
    {
        SceneManager.LoadScene("Scene 2");
    }

    public void loadScene3()
    {
        SceneManager.LoadScene("Scene 3");
    }

    private void loadPlayersForest()
    {
        SceneManager.LoadScene("Players Forest");
    }

    public void loadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void loadSceneSelection()
    {
        SceneManager.LoadScene("Scene Selection");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
