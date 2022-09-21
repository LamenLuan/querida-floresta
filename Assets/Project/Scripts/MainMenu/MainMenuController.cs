using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	[SerializeField] private SceneLoader sceneLoader;
	[SerializeField] private Button startButton;
	[SerializeField] private GameObject buttonsObj;
	[SerializeField] private NarratorMMController narratorController;
	private AudioClip speechClip;

	private void NewGameStarted()
	{
		sceneLoader.loadScene1();
	}

	void Start() // Start is called before the first frame update
	{
		speechClip = narratorController.SpeechAudio.clip;
		startButton.onClick.AddListener(NewGameStarted);
	}

	private void ShowButtons() // Invoked by hideButtonsPlaySpeech()
	{
		buttonsObj.SetActive(true);
	}

	public void HideButtonsPlaySpeech() // Called by button (BtSpeech)
	{
		buttonsObj.SetActive(false);
		narratorController.playSpeechAudio();
		Invoke("ShowButtons", speechClip.length + 1);
	}

	public void QuitButtonAction()
	{
		Player.Instance.ClearData();
		sceneLoader.loadAuthMenu();
	}
}
