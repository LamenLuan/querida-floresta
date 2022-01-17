using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void loadAuthMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void loadRegisterMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void loadScene1()
    {
        SceneManager.LoadScene(3);
    }
    public void loadScene2()
    {
        SceneManager.LoadScene(4);
    }

    public void loadScene3()
    {
        SceneManager.LoadScene(5);
    }

    private void loadPlayersForest()
    {
        SceneManager.LoadScene(6);
    }

    public void loadSceneSelection()
    {
        SceneManager.LoadScene(7);
    }

    public void loadCredits()
    {
        SceneManager.LoadScene(8);
    }

    public void loadStatisticsScreen()
    {
        SceneManager.LoadScene(9);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
