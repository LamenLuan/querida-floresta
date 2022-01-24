using UnityEngine;
using UnityEngine.UI;

public class SceneSelectionController : MonoBehaviour
{
    [SerializeField] private Button scene1Btn, scene2Btn, scene3Btn;

    // Start is called before the first frame update
    void Start()
    {
        Button[] btns = {scene2Btn, scene3Btn};
        
        scene1Btn.interactable = true;
        for (int i = 0; i < btns.Length; i++)
            if(Player.Instance.ScenesCompleted[i]) btns[i].interactable = true;
    }
}