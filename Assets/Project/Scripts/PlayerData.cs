public class PlayerData
{
	private const short NUM_OF_LEVELS_S1 = 3;
	private static short NumOfScenes = SceneLoader.NUM_OF_SCENES;
	public static bool[] SceneCompleted = new bool[NumOfScenes];

	// ALL SCENES
	public static int[] NumOfKboardInputs = new int[NumOfScenes];
	public static int[] NumOfQuits = new int[NumOfScenes];
	public static int[] NumOfClicks = new int[NumOfScenes];
	public static double[] PlayerResponseTime = new double[NumOfScenes];

	// MAKE THIS DATA POR LEVEL IN SCENE 1
	public static double[] PlayDurationPerScene = new double[NumOfScenes];

	// SCENE 1
	public static short[] NumOfTipsS1;
	public static int[] NumOfNoClickMissesS1;
	public static int[] NumOfNearClickMissesS1;
	public static int[] NumOfClickMissesS1;
	// SCENE 2
	public static short NumOfTipsS2;
	// SCENE 3
	public static bool ResponseTimedS2;

	private static void ResetCommonSceneData(short scenceIdx)
	{
		NumOfKboardInputs[scenceIdx] = 0;
		NumOfClicks[scenceIdx] = 0;
		PlayerResponseTime[scenceIdx] = 0;
		PlayDurationPerScene[scenceIdx] = 0;
	}

	public static void ResetScene1Data()
	{
		NumOfTipsS1.Clear();
		NumOfNoClickMissesS1.Clear();
		NumOfNearClickMissesS1.Clear();
		NumOfClickMissesS1.Clear();
		ResetCommonSceneData(0);
	}

	public static void ResetScene2Data()
	{
		NumOfTipsS2 = 0;
		ResponseTimedS2 = false;
		ResetCommonSceneData(1);
	}

	public static void ResetScene3Data()
	{
		ResetCommonSceneData(2);
	}

	private static void ResetGameData()
	{
		for (int i = 0; i < NumOfScenes; i++)
		{
			SceneCompleted[i] = false;
			NumOfQuits[i] = 0;
		}
	}
}