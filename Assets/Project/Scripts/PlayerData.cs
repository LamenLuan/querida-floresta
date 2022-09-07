public class PlayerData
{
	private const short NUM_OF_LEVELS_S1 = 3;
	private const short NUM_OF_QUESTIONS_S3 = 3;
	private static short NumOfScenes = SceneLoader.NUM_OF_SCENES;
	public static bool[] SceneCompleted = new bool[NumOfScenes];

	// ALL SCENES
	public static int[] NumOfKboardInputs = new int[NumOfScenes];
	public static int[] NumOfQuits = new int[NumOfScenes];
	public static int[] NumOfClicks = new int[NumOfScenes];
	public static double[] PlayerResponseTime = new double[NumOfScenes];
	public static double[] PlayDurationPerScene = new double[NumOfScenes];

	// SCENE 1
	public static short[] NumOfTipsS1 = new short[NUM_OF_LEVELS_S1];
	public static int[] NumOfNoClickMissesS1 = new int[NUM_OF_LEVELS_S1];
	public static int[] NumOfNearClickMissesS1 = new int[NUM_OF_LEVELS_S1];
	public static int[] NumOfClickMissesS1 = new int[NUM_OF_LEVELS_S1];
	public static double[] PlayDurationPerLevelS1 = new double[NUM_OF_LEVELS_S1];

	// SCENE 2
	public static short NumOfTipsS2;
	public static short NumOfMissesS2;
	public static short NotFocusedActionsS2;
	public static bool ResponseTimedS2;

	// SCENE 3
	public static short[] NumOfRepsS3 = new short[NUM_OF_QUESTIONS_S3];
	public static short[] NumOfMissesS3 = new short[NUM_OF_QUESTIONS_S3];

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
		PlayDurationPerLevelS1.Clear();
		ResetCommonSceneData(0);
	}

	public static void ResetScene2Data()
	{
		NumOfTipsS2 = 0;
		NumOfMissesS2 = 0;
		ResponseTimedS2 = false;
		ResetCommonSceneData(1);
	}

	public static void ResetScene3Data()
	{
		NumOfRepsS3.Clear();
		NumOfMissesS3.Clear();
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