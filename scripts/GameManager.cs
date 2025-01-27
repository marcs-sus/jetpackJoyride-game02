using Godot;
using Godot.Collections;
using System;
using System.IO;

public partial class GameManager : Node
{
	[Export] public int ScoreIncreaseRate = 2;

	public int CurrentScore = 0;
	public int HighScore = 0;

	private bool isTrackingScore = false;
	private float floatScore = 0.0f;

	Dictionary data = new Dictionary();
	Json jsonLoader = new Json();
	private readonly string filePath = ProjectSettings.GlobalizePath("user://saves/");
	private readonly string fileName = "save_data.json";


	public override void _Ready()
	{
		LoadDataFromFile();

		CurrentScore = 0;
		floatScore = 0.0f;

		StartScoreTracking();
	}

	public override void _Process(double delta)
	{
		if (isTrackingScore)
		{
			CurrentScore += (int)(floatScore += (float)delta * ScoreIncreaseRate);
			floatScore %= 1.0f;

			if (CurrentScore > HighScore)
			{
				HighScore = CurrentScore;
			}
		}
	}

	public void StartScoreTracking()
	{
		isTrackingScore = true;
	}

	public void StopScoreTracking()
	{
		if (CurrentScore >= HighScore)
		{
			HighScore = CurrentScore;

			SaveDataOnFile("high_score", HighScore);
		}

		isTrackingScore = false;
	}

	private void SaveDataOnFile(string dataName, int dataToSave)
	{
		string path = Path.Join(filePath, fileName);

		data[dataName] = dataToSave;

		string jsonData = Json.Stringify(data);
		File.WriteAllText(path, jsonData);

		GD.Print($"Saved data on {path}\n\t --> {dataName} : {data[dataName]}");
	}

	private void LoadDataFromFile()
	{
		string path = Path.Join(filePath, fileName);

		if (File.Exists(path))
		{
			string jsonData = File.ReadAllText(path);
			Error error = jsonLoader.Parse(jsonData);

			if (error == Error.Ok)
			{
				data = (Dictionary)jsonLoader.Data;

				HighScore = (int)data["high_score"];
				//Coins = (int)data["coin_count"];

				GD.Print($"Loaded data from {path}\n\t--> {data}");
			}
			else if (error != Error.Ok)
			{
				GD.PrintErr($"Error loading save data: {error}");
			}
		}
		else if (!File.Exists(path))
		{
			GD.PrintErr($"Save file on {path} does not exist");
		}
	}
}
