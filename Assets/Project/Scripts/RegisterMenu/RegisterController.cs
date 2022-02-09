using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{
    private const string QR_DIR = "usuarios";
    [SerializeField] private GoogleSheetsController sheetsController;
    [SerializeField] private QrCodeGenerator qrCodeGenerator;
    [SerializeField] private GameObject registerObj, qrCodeObj;
    [SerializeField] private AudioSource registerAudio;
    [SerializeField] private Text inputTxt, saveFileButtonTxt, errorTxt,
    placeHolderTxt;
    [SerializeField] private RawImage qrCodeImg;

    private string lastNameRegistred;

    void Start()
    {
        try {
            sheetsController.StartSheets();
        }
        catch(System.Net.Http.HttpRequestException) {
            ErrorMode("Erro de conexão com a rede");
        }
        catch(System.Exception) {
            ErrorMode("Um erro inesperado aconteceu");
        }
    }

    private void SetQrCodeMode()
    {
        registerAudio.Play();
        registerObj.SetActive(false);
        qrCodeObj.SetActive(true);
    }

    IEnumerator InsertNameEffect()
    {
        Color original = placeHolderTxt.color;
        placeHolderTxt.color = new Color(0.4f, 0f, 0f, 0.5f);
        yield return new WaitForSeconds(1f);
        placeHolderTxt.color = original;
    }

    private List<object> PrepareRegister(string name)
    {
        List<object> data = new List<object>{name, 0, 0, 0};
        return data;
    }

    public void RegisterPlayer()
    {
        string name = inputTxt.text.ToLower();

        if( name.Equals("") ) {
            StartCoroutine( InsertNameEffect() );
            return;
        }

        int id = sheetsController.FindUser(name) + 1;
        if(id == 0) {
            lastNameRegistred = name;
            IList<object> data = PrepareRegister(name);
            if( sheetsController.CreateEntry(data) ) {
                try {
                    int playerId = sheetsController.FindUser(name) + 1;
                    qrCodeGenerator.GenerateQrCode( playerId.ToString() );
                    SetQrCodeMode();
                }
                catch(System.Exception ex) { ErrorMode(ex.Message); }
            }
            print("Usuário cadastrado");
        }
        else {
            print("Usuário já cadastrado");
        }
    }

    IEnumerator FileSavedMsg()
    {
        string originalTxt = saveFileButtonTxt.text;
        Color originalColor = saveFileButtonTxt.color;

        saveFileButtonTxt.text = "Arquivo salvo";
        saveFileButtonTxt.color = new Color(0, 1, 0);
        yield return new WaitForSeconds(2f);
        saveFileButtonTxt.text = originalTxt;
        saveFileButtonTxt.color = originalColor;
    }

    public void SaveQrCode()
    {
        Texture2D texture2D = (Texture2D) qrCodeImg.texture;
        Directory.CreateDirectory(QR_DIR);
        File.WriteAllBytes(
            $"{QR_DIR}/{lastNameRegistred}.png", texture2D.EncodeToPNG()
        );
        StartCoroutine( FileSavedMsg() );
    }

    public void ErrorMode(string msg)
    {
        errorTxt.text = msg;
        errorTxt.transform.parent.gameObject.SetActive(true);

        Transform regTransform = registerObj.transform;
        for(int i = 0; i < regTransform.childCount - 1; i++)
            regTransform.GetChild(i).gameObject.SetActive(false);
    }
}
