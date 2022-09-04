using System;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.RainMaker;
using System.Threading;
using static Extensions;

public class Scene2Controller : MonoBehaviour
{
	private const short SCENE_IDX = 1;
	[SerializeField] private Button cowButton, garbageButton, treesButton;
	[SerializeField] private GameObject rainPrefab, cowObject, garbageObject, treesObject;
	[SerializeField] private SceneLoader sceneLoader;
	[SerializeField] private GoogleSheetsController sheetsController;
	[SerializeField] private SpritesS2Controller spritesController;
	[SerializeField] private CanvasS2Controller canvasController;
	[SerializeField] private Scene2NarratorController narratorController;
	private const float INTRO_LENGTH = 26.697f;
	private static DateTime timeStarted;
	private RainScript2D rainScript;
	private enum Tcontroller { CANVAS, SPRITE, SELF, SCENE_LOADER }
	private ref bool SceneCompleted => ref PlayerData.SceneCompleted[SCENE_IDX];

	public void mouseBtnClicked() // Called by all buttons
	{
		if (!SceneCompleted) PlayerData.NumOfClicks[SCENE_IDX]--;
	}

	public void quitScene() // Called by Button (btQuit)
	{
		if (!SceneCompleted)
		{
			PlayerData.ResetScene2Data();
			PlayerData.NumOfQuits[SCENE_IDX]++;
		}
		AplicationModel.isFirstTimeScene2 = true;
		sceneLoader.loadMainMenu();
	}

	public void showHelp()
	{
		if (!SceneCompleted) PlayerData.NumOfTipsS2++;
		playANarratorAudio("playHelpAudio", "leaveHelpInterface", 9f);
		canvasController.changeToHelpInterface();
	}

	private void startRaining() // Invoked by in Start()
	{
		rainScript = (Instantiate(rainPrefab)).GetComponent<RainScript2D>();
		rainScript.RainHeightMultiplier = 0f;
		rainScript.RainWidthMultiplier = 1.12f;
		Destroy(rainScript.gameObject, 8f);
	}

	private void playANarratorAudio(
			string functionToInvoke, string sceneFunction, float length,
			Tcontroller controller = Tcontroller.CANVAS, float awaitTime = 0f
	)
	{
		narratorController.Invoke(functionToInvoke, awaitTime + 1f);
		if (sceneFunction != null)
		{
			length += awaitTime;
			switch (controller)
			{
				case Tcontroller.CANVAS:
					canvasController.Invoke(sceneFunction, length); break;
				case Tcontroller.SPRITE:
					spritesController.Invoke(sceneFunction, length); break;
				case Tcontroller.SCENE_LOADER:
					sceneLoader.Invoke(sceneFunction, length); break;

				default: Invoke(sceneFunction, length); break;
			}
		}
	}

	public void sceneMiss(string audioToInvoke, float audioLength)
	{
		AplicationModel.Scene2Misses++;
		playANarratorAudio(audioToInvoke, "changeToTryAgainInterface", audioLength);
	}

	private void sendDataToReport()
	{
		ReportCreator.writeLine("\nAtividade 2");
		ReportCreator.writeLine(
			$"Quantidade de erros da fase: {AplicationModel.Scene2Misses}"
		);
		ReportCreator.writeResponseTime(PlayerData.PlayerResponseTime[SCENE_IDX]);
	}

	void Start() // Start is called before the first frame update
	{
		AplicationModel.SceneAcesses[1]++;

		if (AplicationModel.isFirstTimeScene2)
		{
			if (!SceneCompleted) timeStarted = DateTime.Now;
			AplicationModel.isFirstTimeScene2 = false;
			canvasController.showBackgroundCover();
			playANarratorAudio(
				"playIntroductionAudio", "hideBackgroundCover", INTRO_LENGTH
			);
		}

		Action treesClicked = () =>
		{
			AplicationModel.isFirstTimeScene2 = true;

			playANarratorAudio(
					"playTreesSelectedAudio", "showTreesRoots", 8.74f,
					Tcontroller.SPRITE
			);
			playANarratorAudio(
					"playAboutRootsAudio", null, 9.89f,
					Tcontroller.SELF, 8.74f
			);
			playANarratorAudio(
					"playAboutEvaporationAudio", "startRaining", 16.56f,
					Tcontroller.SELF, 18.63f
			);
			playANarratorAudio(
					"playAboutRainAudio", "turnSceneGreenAndAnimalsHappy", 8.96f,
					Tcontroller.SPRITE, 35.19f
			);
			playANarratorAudio(
					"playSceneCompletedAudio",
					(Player.Instance.ScenesCompleted[1])
							? "loadSceneSelection"
							: "loadPlayersForest",
					9f,
					Tcontroller.SCENE_LOADER, 44.15f
			);

			if (!Player.Instance.ScenesCompleted[1])
			{
				sendDataToReport();
				new Thread(sheetsController.SavePlayerProgress).Start();
				Player.Instance.ScenesCompleted[1] = true;
			}
		};

		Action<GameObject, Button> buttonClicked = (gameObject, button) =>
		{
			Button[] buttons = { cowButton, treesButton, garbageButton };

			if (!SceneCompleted && !PlayerData.ResponseTimedS2)
			{
				PlayerData.ResponseTimedS2 = true;
				PlayerData.PlayerResponseTime[SCENE_IDX] =
					(DateTime.Now - timeStarted).TotalSeconds - INTRO_LENGTH;
			}

			canvasController.showBackgroundCover();
			foreach (Button btn in buttons) btn.onClick.RemoveAllListeners();
			Destroy(button.gameObject);
			gameObject.SetActive(true);
		};

		cowButton.onClick.AddListener(() =>
		{
			buttonClicked(cowObject, cowButton);
			sceneMiss("playCowSelectedAudio", 7.76f);
		});

		garbageButton.onClick.AddListener(() =>
		{
			buttonClicked(garbageObject, garbageButton);
			sceneMiss("playTrashSelectedAudio", 8.49f);
		});

		treesButton.onClick.AddListener(() =>
		{
			if (!SceneCompleted) PlayerData.PlayDurationPerScene[SCENE_IDX] =
				(DateTime.Now - timeStarted).TotalSeconds;

			buttonClicked(treesObject, treesButton);
			treesClicked();
			SceneCompleted = true;
		});
	}

	void Update()
	{
		if (!SceneCompleted)
		{
			if (Extensions.KeyboardDown()) PlayerData.NumOfKboardInputs[SCENE_IDX]++;
			if (Input.GetMouseButtonDown(0)) PlayerData.NumOfClicks[SCENE_IDX]++;
		}
	}
}
