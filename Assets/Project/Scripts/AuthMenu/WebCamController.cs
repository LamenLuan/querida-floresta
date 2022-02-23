using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class WebCamController : MonoBehaviour
{
    [SerializeField] private AuthController authController;
    [SerializeField] private AudioController audioController;
    [SerializeField] private RawImage webCamRawImg;
    [SerializeField] private Text qrCodeTxt;
    static WebCamTexture webCamTexture;
    private BarcodeReaderGeneric barcodeReader;

    private static byte[] Color32ArrayToByteArray(Color32[] colors)
    {
        if (colors == null || colors.Length == 0)
            return null;

        int lengthOfColor32 = Marshal.SizeOf(typeof(Color32));
        int length = lengthOfColor32 * colors.Length;
        byte[] bytes = new byte[length];

        GCHandle handle = default(GCHandle);
        try {
            handle = GCHandle.Alloc(colors, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();
            Marshal.Copy(ptr, bytes, 0, length);
        }
        finally {
            if (handle != default(GCHandle)) handle.Free();
        }
        
        return bytes;
    }

    void Update()
    {
        if (barcodeReader != null) {
            Result result = barcodeReader.Decode(
                Color32ArrayToByteArray(webCamTexture.GetPixels32()),
                webCamTexture.width,
                webCamTexture.height,
                RGBLuminanceSource.BitmapFormat.RGB32
            );

            if(result != null) {
                if( GoogleSheetsController.ValitadeId(result.Text) ) {
                    if( authController.LoadPlayer(result.Text) ) {
                        StopCam();
                        StartCoroutine(AcessEffect());
                        audioController.hitSound();
                        qrCodeTxt.text = $"BEM VINDO {Player.Instance.Name}!!";
                    }
                }
            }
        }
    }

    public void StartWebCam()
    {
        if(webCamTexture == null) webCamTexture = new WebCamTexture();
        webCamRawImg.texture = webCamTexture;
        webCamRawImg.material.mainTexture = webCamTexture;
        if(!webCamTexture.isPlaying) webCamTexture.Play();
        if(!webCamTexture.isPlaying) {
            authController.ErrorMode(
                "Webcam indisponível ou inacessável. Conecte o dispositivo"
                + " e/ou de permissão ao jogo"
            );
            return;
        }

        barcodeReader = new BarcodeReaderGeneric { AutoRotate = false };
    }

    IEnumerator AcessEffect()
    {
        Color originalColor = qrCodeTxt.color;
        qrCodeTxt.color = new Color(0f, 1f, 0f, 1f);
        yield return new WaitForSeconds(1.0f);
        qrCodeTxt.color = originalColor;
    }

    public void StopCam()
    {
        if(webCamTexture.isPlaying) {
            barcodeReader = null;
            webCamTexture.Stop();
        }
    }
}
