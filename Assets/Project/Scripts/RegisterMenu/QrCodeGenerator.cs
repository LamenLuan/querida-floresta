using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QrCodeGenerator : MonoBehaviour
{
    private const int HEIGHT = 256;
    private const int WIDTH = 256;
    [SerializeField] private RawImage qrCodeImg;

    private BarcodeWriterPixelData GenerateBarcodeWriter()
    {
        BarcodeWriterPixelData barcodeWriter = new BarcodeWriterPixelData();
        barcodeWriter.Format = BarcodeFormat.QR_CODE;
        barcodeWriter.Options.Width = WIDTH;
        barcodeWriter.Options.Height = HEIGHT;

        return barcodeWriter;
    }

    public void GenerateQrCode(string id)
    {
        var barcodeWriter = GenerateBarcodeWriter();
        var qrCode = barcodeWriter.Write(id);

        Texture2D texture2D = new Texture2D(
            WIDTH, HEIGHT, TextureFormat.BGRA32, false, true
        );
        texture2D.LoadRawTextureData(qrCode.Pixels);
        texture2D.Apply();
        
        qrCodeImg.texture = texture2D;
    }
}
