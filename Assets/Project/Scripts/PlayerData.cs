public class PlayerData
{
	private static short NumOfScenes = SceneLoader.NUM_OF_SCENES;
	public static bool[] CompletedScene = new bool[NumOfScenes];
	public static short NumOfTipsS1, NumOfTipsS2;
	public static int[] NumOfKboardInputs = new int[NumOfScenes];
	public static int[] NumOfQuits = new int[NumOfScenes];

	private static void ResetGameData()
	{
		for (int i = 0; i < NumOfScenes; i++)
		{
			CompletedScene[i] = false;
			NumOfQuits[i] = 0;
		}
	}

	private static void ResetCommonSceneData(short scenceIdx)
	{
		NumOfKboardInputs[scenceIdx] = 0;
	}

	public static void ResetScene1Data()
	{
		NumOfTipsS1 = 0;
		ResetCommonSceneData(0);
	}

	public static void ResetScene2Data()
	{
		NumOfTipsS2 = 0;
		ResetCommonSceneData(1);
	}

	public static void ResetScene3Data()
	{
		ResetCommonSceneData(2);
	}
}