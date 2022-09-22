﻿using UnityEngine;
using UnityEngine.UI;

public class PlayersForestController : MonoBehaviour
{
	[SerializeField] private SceneLoader sceneLoader;
	[SerializeField] private CanvasPFController canvasController;
	[SerializeField] private AudioController audioController;
	[SerializeField] private NarratorPFController narratorController;
	private string functionToInvoke;

	private void showReward()
	{
		canvasController.removeLockedEffect();
		audioController.sceneCompletedSound();
		if (functionToInvoke != null) sceneLoader.Invoke(functionToInvoke, 8f);
	}

	private void addButtonInCanvas() // Invoked in Start()
	{
		Button canvasButton = canvasController.addButton();

		canvasButton.onClick.AddListener(() =>
		{
			showReward();
			canvasButton.onClick.RemoveAllListeners();
		});
	}

	void Start() // Start is called before the first frame update
	{
		bool gameFinished = false;

		if (AplicationModel.isForestInTemporaryMode)
		{
			narratorController.Invoke("playUnlockAudio", 1f);
			functionToInvoke = null;

			if (PlayerData.SceneCompleted[1])
				functionToInvoke = "loadScene3";
			else if (PlayerData.SceneCompleted[0])
				functionToInvoke = "loadScene2";
			else
			{
				functionToInvoke = "loadMainMenu";
				gameFinished = true;
			}

			canvasController.setLockedEffect();
			Invoke(
					"addButtonInCanvas",
					narratorController.UnlockAudio.clip.length + 1f
			);
		}
		canvasController.setRewardsActive(gameFinished);
	}
}
