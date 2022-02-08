using UnityEngine;
using UnityEngine.UI;
using QRCoder;

public class QrCodeGenerator : MonoBehaviour
{
    private const int SIZE = 256;
    private const int PIXELS_PER_MODULE = 20;
    private const QRCodeGenerator.ECCLevel ECC = QRCodeGenerator.ECCLevel.Q;
    [SerializeField] private RawImage qrCodeImg;

    private byte[] GenerateBarcodeWriter(string id)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(id, ECC);
        PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

        return qrCode.GetGraphic(PIXELS_PER_MODULE);
    }

    public void GenerateQrCode(string id)
    {
        Texture2D texture2D = new Texture2D(
            SIZE, SIZE, TextureFormat.BGRA32, false, true
        );
        texture2D.LoadImage( GenerateBarcodeWriter(id) );
        texture2D.Apply();
        qrCodeImg.texture = texture2D;
    }
}
