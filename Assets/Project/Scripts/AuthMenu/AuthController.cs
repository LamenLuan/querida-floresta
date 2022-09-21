using UnityEngine;
using UnityEngine.UI;

public class AuthController : MonoBehaviour
{
	[SerializeField] private SceneLoader sceneLoader;
	[SerializeField] private GoogleSheetsController sheetsController;
	[SerializeField] private GameObject buttonsObj, noConnectionObj;
	private bool readQr;

	void Start()
	{
		PlayerData.ResetGameData();

		try
		{
			sheetsController.StartSheets();
			sceneLoader.loadMainMenu();
		}
		catch (System.Net.Http.HttpRequestException)
		{
			ErrorMode("Erro de conexão com a rede");
		}
		catch (System.Exception)
		{
			ErrorMode("Um erro inesperado aconteceu");
		}
	}

	public void ErrorMode(string msg)
	{
		buttonsObj.SetActive(false);
		Text text = noConnectionObj.transform.GetComponentInChildren<Text>();
		text.text = msg;
		noConnectionObj.SetActive(true);
	}

	public void QuitButtonAction() // Called by button
	{
		if (readQr)
		{
			sceneLoader.loadAuthMenu();
		}
		else sceneLoader.quitGame();
	}
}
