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
		request.Execute();
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

	public int FindUser(string user)
	{
		var range = $"data!B:B";
		var request = service.Spreadsheets.Values.Get(Credentials.ID, range);

		var response = request.Execute();
		var rows = response.Values;
		if (rows == null) return -1;

		for (int i = 0; i < rows.Count; i++)
		{
			if (rows[i][0].Equals(user)) return i;
		}

		return -1;
	}

	public bool CreateEntry(IList<object> playerData)
	{
		var range = $"data!A:{FINAL_COL}";
		var valueRange = new ValueRange();

		var data = new List<object>() { "=LIN()" };
		data.AddRange(playerData);
		valueRange.Values = new List<IList<object>> { data };

		var append = service.Spreadsheets.Values.Append(
				valueRange, Credentials.ID, range
		);

		append.ValueInputOption = SpreadsheetsResource.ValuesResource.
				AppendRequest.ValueInputOptionEnum.USERENTERED;

		try { append.Execute(); }
		catch (System.Exception) { return false; }

		return true;
	}

	public void SendPlayData()
	{
		var range = $"play-data!A:";
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

	public void UpdateEntry(string id, IList<object> data)
	{
		if (!ValitadeId(id)) return;

		var valueRange = new ValueRange();
		var range = $"data!B{id}:{FINAL_COL}{id}";

		valueRange.Values = new List<IList<object>> { data };

		var update = service.Spreadsheets.Values.Update(
				valueRange, Credentials.ID, range
		);

		update.ValueInputOption = SpreadsheetsResource.ValuesResource.
				UpdateRequest.ValueInputOptionEnum.USERENTERED;

		update.Execute();
	}

	public void SavePlayerProgress()
	{
		int id = FindUser(Player.Instance.Name) + 1;
		if (id != 0) UpdateEntry(id.ToString(), Player.Instance.ToObjectList());
	}
}