using UnityEngine;
using UnityEngine.UI;

public class WebCamController : MonoBehaviour
{
    [SerializeField] private MainMenuController mainMenuController;
    [SerializeField] private RawImage webCamRawImg;
    static WebCamTexture webCamTexture;

    void Start() // Start is called before the first frame update
    {
        if (webCamTexture == null) webCamTexture = new WebCamTexture();
        webCamRawImg.texture = webCamTexture;
        webCamRawImg.material.mainTexture = webCamTexture;
        if (!webCamTexture.isPlaying) webCamTexture.Play();
        if(!webCamTexture.isPlaying) {
            mainMenuController.ErrorMode(
                "Webcam indisponível ou inacessável. Conecte o dispositivo e/ou"
                + " de permissão ao jogo"
            );
        }
    }

    public void StopCam()
    {
        if(webCamTexture.isPlaying) webCamTexture.Stop();
    }

}
