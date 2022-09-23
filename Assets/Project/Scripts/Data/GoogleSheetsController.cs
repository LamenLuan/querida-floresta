using UnityEngine;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public class GoogleSheetsController : MonoBehaviour
{
	const string FINAL_COL = "E";
	static SheetsService service;

	public void StartSheets()
	{
		GoogleCredential googleCredential;
		object obj = JsonConvert.DeserializeObject(Credentials.CREDENTIALS);

		string[] scopes = { SheetsService.Scope.Spreadsheets };
		googleCredential = GoogleCredential.FromJson(
				Credentials.CREDENTIALS
		).CreateScoped(scopes);

		service = new SheetsService(new BaseClientService.Initializer()
		{
			HttpClientInitializer = googleCredential,
			ApplicationName = "Querida Floresta"
		});

		TestConnection();
	}

	private void TestConnection()
	{
		var range = $"data!A1:A1";
		var request = service.Spreadsheets.Values.Get(Credentials.ID, range);
		var response = request.Execute();
	}

	public static bool ValitadeId(string id)
	{
		try { return int.Parse(id) > 0; }
		catch (FormatException) { return false; }
	}

	public string[] FindEntry(string id)
	{
		if (!ValitadeId(id)) return null;

		var range = $"data!A{id}:{FINAL_COL}{id}";
		var request = service.Spreadsheets.Values.Get(Credentials.ID, range);

		var response = request.Execute();
		var rows = response.Values;
		if (rows == null) return null;

		string[] data = new string[rows[0].Count];
		for (int i = 0; i < data.Length; i++) data[i] = rows[0][i].ToString();

		return data;
	}

	public void SendPlayData()
	{
		var range = $"play-data!A:A";
		var valueRange = new ValueRange();

		var playerData = PlayerData.ToObjectList();
		valueRange.Values = new List<IList<object>> { playerData };

		var append = service.Spreadsheets.Values.Append(
			valueRange, Credentials.ID, range
		);

		append.ValueInputOption = SpreadsheetsResource.ValuesResource.
			AppendRequest.ValueInputOptionEnum.USERENTERED;

		append.Execute();
		PlayerData.ResetGameData();
	}
}