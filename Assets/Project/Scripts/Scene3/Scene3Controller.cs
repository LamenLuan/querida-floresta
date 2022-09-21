using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Scene3Controller : MonoBehaviour
{
	private const short SCECE_IDX = 2;
	[SerializeField] private SceneLoader sceneLoader;
	[SerializeField] private GoogleSheetsController sheetsController;
	[SerializeField] private CanvasS3Controller canvasController;
	[SerializeField] private AudioController audioController;
	[SerializeField] private NarratorS3Controller narratorController;
	[SerializeField]
	private Button option1Btn, option2Btn, option3Btn,
	word1Btn, word2Btn, repeatQuestionBtn;
	private byte levelIndex;
	private DateTime timeStarted;
	private ref bool SceneCompleted => ref PlayerData.SceneCompleted[SCECE_IDX];

	public void mouseBtnClicked() // Called by all buttons
	{
		if (!SceneCompleted) PlayerData.NumOfClicks[SCECE_IDX]--;
	}

	public void RepeatBtnClicked()
	{
		if (!SceneCompleted) PlayerData.NumOfRepsS3[levelIndex]++;
	}

	private void removeListenerFromButtons() // Invoked in Start()
	{
		Button[] buttons = { option1Btn, option2Btn, option3Btn, repeatQuestionBtn };
		foreach (Button btn in buttons) btn.onClick.RemoveAllListeners();
	}

	public void quitScene() // Called by Button (btQuit)
	{
		if (!SceneCompleted) PlayerData.NumOfQuits[SCECE_IDX]++;
		sceneLoader.loadMainMenu();
	}

	private void setRightAnswer(Button button)
	{
		canvasController.setAnswerEffect(button, true);
		audioController.hitSound();
		narratorController.playRightAnswerAudio();
		if (levelIndex < 3) levelIndex++;
	}

	private void setWrongAnswer(Button button)
	{
		canvasController.setAnswerEffect(button, false);
		audioController.missSound();
		narratorController.playWrongAnswerAudio();
		AplicationModel.Scene3Misses[levelIndex]++;
		PlayerData.NumOfMissesS3[levelIndex]++;
	}

	private void sendDataToReport()
	{
		ReportCreator.writeLine("\nAtividade 3");
		ReportCreator.writeMissesPerPhase(AplicationModel.Scene3Misses);
		ReportCreator.writeResponseTime(PlayerData.PlayerResponseTime[SCECE_IDX]);
	}

	void Start() // Start is called before the first frame update
	{
		PlayerData.ResetScene3Data();

		timeStarted = DateTime.Now;
		AplicationModel.SceneAcesses[2]++;
		levelIndex = 0;

		repeatQuestionBtn.onClick.AddListener(
				() => narratorController.playQuestion1Audio()
		);

		PlayerData.PlayerResponseTime[SCECE_IDX] =
				narratorController.playIntroductionAudio();

		Action changeToQuestionThree = () =>
		{
			removeListenerFromButtons();
			repeatQuestionBtn.onClick.AddListener(
					() => narratorController.playQuestion3Audio()
			);
			canvasController.Invoke("changeToQuestionThree", 2.5f);

			word1Btn.onClick.AddListener(() =>
			{
				if (!SceneCompleted) PlayerData.PlayDurationPerScene[SCECE_IDX] =
					timeStarted.SecondsPassed();

				SceneCompleted = true;
				setRightAnswer(word1Btn);
				canvasController.Invoke("levelIsOver", 2.5f);
				narratorController.Invoke(
						"playSceneCompletedAudio",
						narratorController.RightAnswerAudio.clip.length + 1f
				);

				sceneLoader.Invoke(
						"loadPlayersForest",
						narratorController.SceneCompletedAudio.clip.length +
						narratorController.RightAnswerAudio.clip.length + 3f
				);

				if (PlayerData.AllScenesCompleted())
					new Thread(sheetsController.SendPlayData).Start();
			});
			word2Btn.onClick.AddListener(() => setWrongAnswer(word2Btn));
		};

		Action changeToQuestionTwo = () =>
		{
			removeListenerFromButtons();
			repeatQuestionBtn.onClick.AddListener(
					() => narratorController.playQuestion2Audio()
			);
			canvasController.Invoke("changeToQuestionTwo", 2f);

			option1Btn.onClick.AddListener(() => setWrongAnswer(option1Btn));
			option2Btn.onClick.AddListener(() =>
			{
				setRightAnswer(option2Btn);
				narratorController.Invoke(
						"playQuestion3Audio",
						narratorController.RightAnswerAudio.clip.length + 1f
				);
				changeToQuestionThree();
			});
			option3Btn.onClick.AddListener(() => setWrongAnswer(option3Btn));
		};

		option1Btn.onClick.AddListener(() => setWrongAnswer(option1Btn));
		option2Btn.onClick.AddListener(() => setWrongAnswer(option2Btn));
		option3Btn.onClick.AddListener(() =>
		{
			setRightAnswer(option3Btn);
			narratorController.Invoke(
					"playQuestion2Audio",
					narratorController.RightAnswerAudio.clip.length + 1f
			);
			changeToQuestionTwo();
		});

		Button[] buttons = { option1Btn, option2Btn, option3Btn };

		void calculateTimePassed()
		{
			PlayerData.PlayerResponseTime[SCECE_IDX] =
				timeStarted.SecondsPassed() - PlayerData.PlayerResponseTime[SCECE_IDX];

			foreach (var button in buttons) button.onClick.RemoveListener(calculateTimePassed);
		}

		if (!SceneCompleted)
			foreach (var button in buttons) button.onClick.AddListener(calculateTimePassed);
	}

	void Update()
	{
		if (!SceneCompleted)
		{
			if (Extensions.KeyboardDown()) PlayerData.NumOfKboardInputs[SCECE_IDX]++;
			if (Input.GetMouseButtonDown(0)) PlayerData.NumOfClicks[SCECE_IDX]++;
		}
	}
}
