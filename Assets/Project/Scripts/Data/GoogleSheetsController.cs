using UnityEngine;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;

public class GoogleSheetsController : MonoBehaviour
{
    [SerializeField] private MainMenuController _mainMenuController;
    const string SHEET_ID = "1_HznfnrQQ2iUVnMVr-4rdwz8iDWR71Zfzk15rc9pDmE";
    const string FINAL_COL = "B";
    static SheetsService service;

    void Start()
    {
        GoogleCredential googleCredential;

        try {
            using (var stream = new FileStream(
                "credentials.json", FileMode.Open, FileAccess.Read
            )) {
                string[] scopes = { SheetsService.Scope.Spreadsheets };

                googleCredential =
                    GoogleCredential.FromStream(stream).CreateScoped(scopes);
            }

            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = googleCredential,
                ApplicationName = "Querida Floresta"
            });

            print(service.Spreadsheets.Values.Get(SHEET_ID, $"data!A:A"));
        }
        catch (System.IO.FileNotFoundException) {
            _mainMenuController.ErrorMode("Arquivo faltante, reinstale o jogo");
            return;
        }
    }

    private bool ValitadeId(string id)
    {
        try { if(int.Parse(id) < 1) return false; }
        catch (FormatException) { return false; }
        return true;
    }

    public string[] FindEntry(string id)
    {
        if( !ValitadeId(id) ) return null;

        var range = $"data!A{id}:{FINAL_COL}{id}";
        var request = service.Spreadsheets.Values.Get(SHEET_ID, range);

        var response = request.Execute();
        var rows = response.Values;
        if(rows == null) return null;

        string[] data = new string[rows[0].Count];
        for (int i = 0; i < data.Length; i++) data[i] = rows[0][i].ToString();

        return data;
    }

    public bool CreateEntry(IList<object> playerData)
    {
        var range = $"data!A:{FINAL_COL}";
        var valueRange = new ValueRange();

        var data = new List<object>() {"=LIN()"};
        data.AddRange(playerData);
        valueRange.Values = new List<IList<object>> {data};

        var append = service.Spreadsheets.Values.Append(
            valueRange, SHEET_ID, range
        );

        append.ValueInputOption = SpreadsheetsResource.ValuesResource.
            AppendRequest.ValueInputOptionEnum.USERENTERED;

        try { append.Execute(); }
        catch (System.Exception) { return false; }
        
        return true;
    }

    public void UpdateEntry(string id, IList<object> data)
    {
        if( !ValitadeId(id) ) return;

        var valueRange = new ValueRange();
        var range = $"data!B{id}:{FINAL_COL}{id}";

        valueRange.Values = new List<IList<object>> {data};

        var update = service.Spreadsheets.Values.Update(
            valueRange, SHEET_ID, range
        );

        update.ValueInputOption = SpreadsheetsResource.ValuesResource.
            UpdateRequest.ValueInputOptionEnum.USERENTERED;

        update.Execute();
    }
}