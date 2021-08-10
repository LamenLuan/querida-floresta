using UnityEngine;
using UnityEngine.UI;

public class SceneSelectionController : MonoBehaviour
{
    [SerializeField] private Button scene1Btn, scene2Btn, scene3Btn;

    // Start is called before the first frame update
    void Start()
    {
        Button[] btns = {scene1Btn, scene2Btn, scene3Btn};
        byte scenesCompleted = AplicationModel.scenesCompleted;

        for (int i = 0; scenesCompleted >= i && i < btns.Length; i++)
            btns[i].interactable = true;
    }
}