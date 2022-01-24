public class AplicationModel
{
    private static AplicationModel _instace;

    public static AplicationModel Instance {
        get {
            if (_instace == null) _instace = new AplicationModel();
            return _instace;
        }
    }

    public static bool isForestInTemporaryMode = true, lessClouds;
    public static bool isFirstTimeScene1 = true, isFirstTimeScene2 = true;

    // Report data
    public static byte[] SceneAcesses { get; } = new byte[3];
    public static byte[] Scene1Misses { get; } = new byte[3];
    public static byte Scene2Misses { get; set; }
    public static byte[] Scene3Misses { get; } = new byte[3];
    public static double[] PlayerResponseTime { get; } = new double[3];

    private AplicationModel() {}
}