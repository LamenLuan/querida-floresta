using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{
    private const string QR_DIR = "usuarios";
    [SerializeField] private GoogleSheetsController sheetsController;
    [SerializeField] private NarratorRegisterController narratorController;
    [SerializeField] private QrCodeGenerator qrCodeGenerator;
    [SerializeField] private GameObject registerObj, qrCodeObj, btConcludeObj;
    [SerializeField] private AudioSource registerAudio;
    [SerializeField] private Text inputTxt, saveFileButtonTxt, errorTxt,
    placeHolderTxt;
    [SerializeField] private RawImage qrCodeImg;
    private bool canRegister = true;
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

    IEnumerator SetQrCodeMode()
    {
        registerAudio.Play();
        registerObj.SetActive(false);
        qrCodeObj.SetActive(true);
        yield return new WaitForSeconds(narratorController.UserRegAudioLength);
        btConcludeObj.SetActive(true);
    }

    IEnumerator InsertNameEffect()
    {
        Color original = placeHolderTxt.color;
        placeHolderTxt.color = new Color(0.4f, 0f, 0f, 0.5f);
        yield return new WaitForSeconds(narratorController.TypeUserAudioLength);
        placeHolderTxt.color = original;
        canRegister = true;
    }

    private List<object> PrepareRegister(string name)
    {
        List<object> data = new List<object>{name, 0, 0, 0};
        return data;
    }

    private void SetCanRegister()
    {
        canRegister = true;
    }

    private void RegisterPlayer()
    {
        string name = inputTxt.text.ToLower();

        lastNameRegistred = name;
        IList<object> data = PrepareRegister(name);
        if ( sheetsController.CreateEntry(data) ) {
            try {
                int playerId = sheetsController.FindUser(name) + 1;
                qrCodeGenerator.GenerateQrCode(playerId.ToString());
                StartCoroutine( SetQrCodeMode() );
            }
            catch (System.Exception ex) { ErrorMode(ex.Message); }
        }
        narratorController.playUserRegisteredAudio();
    }

    private void WarnAlreadyRegistered()
    {
        canRegister = false;
        narratorController.playAlreadyRegisteredAudio();
        Invoke("SetCanRegister", narratorController.AlreadyRegAudioLength);
    }

    public void TryRegisterPlayer()
    {
        if(!canRegister) return;
        string name = inputTxt.text.ToLower();

        if( name.Equals("") ) {
            canRegister = false;
            StartCoroutine( InsertNameEffect() );
            narratorController.playTypeUserAudio();
            return;
        }

        int id = sheetsController.FindUser(name) + 1;
        if(id == 0) RegisterPlayer();
        else WarnAlreadyRegistered();
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
