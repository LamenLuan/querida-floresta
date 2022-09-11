using UnityEngine;
using UnityEngine.UI;

public class AuthController : MonoBehaviour
{
	[SerializeField] private SceneLoader sceneLoader;
	[SerializeField] private GoogleSheetsController sheetsController;
	[SerializeField] private WebCamController webCamController;
	[SerializeField] private GameObject buttonsObj, noConnectionObj, webCamObj;
	[SerializeField] private Text quitButtonTxt;
	[SerializeField] private Toggle editorTg;
	private bool readQr;

	void Start()
	{

#if UNITY_EDITOR
		editorTg.gameObject.SetActive(true);
#endif

		try
		{
			sheetsController.StartSheets();
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

	public void ReadQrMode()
	{
		if (editorTg.gameObject.activeSelf && editorTg.isOn)
		{
			LoadEditorPlayer();
			return;
		}

		readQr = true;
		buttonsObj.SetActive(false);
		webCamObj.SetActive(true);
		editorTg.gameObject.SetActive(false);
		quitButtonTxt.text = "VOLTAR";
		webCamController.StartWebCam();
	}

	public void SetEditorMode(bool onValueChanged) // Called by Toggle (tgEditor)
	{
		AplicationModel.EditorMode = !AplicationModel.EditorMode;
	}

	public void ErrorMode(string msg)
	{
		buttonsObj.SetActive(false);
		webCamObj.SetActive(false);
		Text text = noConnectionObj.transform.GetComponentInChildren<Text>();
		text.text = msg;
		noConnectionObj.SetActive(true);
	}

	private bool LoadPlayerData(string id)
	{
		var data = sheetsController.FindEntry(id);
		if (data == null) return false;
		Player.Instance.LoadData(data);
		return true;
	}

	public void LoadEditorPlayer()
	{
		if (LoadPlayerData("1")) sceneLoader.loadMainMenu();
	}

	public bool LoadPlayer(string id)
	{
		var dataLoaded = LoadPlayerData(id);
		if (dataLoaded) sceneLoader.Invoke("loadMainMenu", 2.0f);
		return dataLoaded;
	}

	public void QuitButtonAction()
	{
		if (readQr)
		{
			webCamController.StopCam();
			sceneLoader.loadAuthMenu();
		}
		else sceneLoader.quitGame();
	}
}
