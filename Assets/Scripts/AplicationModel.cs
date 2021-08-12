public class AplicationModel
{
    public static bool isForestInTemporaryMode = true;
    public static bool isFirstTimeScene2 = true;
    public static bool[] scenesCompleted = {false, false, false};
    public static byte scene2Misses = 0;
    public static byte[] sceneAcesses = new byte[3];
}
