public class PlayerData
{
	private static short NumOfScenes = SceneLoader.NUM_OF_SCENES;
	public static bool[] CompletedScene = new bool[NumOfScenes];
	public static short[] NumOfTips = new short[NumOfScenes - 1];

	private static void ResetCommonSceneData(short scence)
	{
		NumOfTips[scence] = 0;
	}

	public static void ResetScene1Data()
	{
		ResetCommonSceneData(0);
	}

	public static void ResetScene2Data()
	{
		ResetCommonSceneData(1);
	}

	public static void ResetScene3Data()
	{
		ResetCommonSceneData(2);
	}
}