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

	public int Coins = 0;

	public AudioStreamPlayer2D gameMusic, menuMusic;

	public Dictionary data = new Dictionary();
	private Json jsonLoader = new Json();
	private readonly string filePath = ProjectSettings.GlobalizePath("user://saves/");
	private readonly string fileName = "save_data.json";


	public override void _Ready()
	{
		gameMusic = GetNode<AudioStreamPlayer2D>("GameMusic");
		menuMusic = GetNode<AudioStreamPlayer2D>("MenuMusic");

		if (!Directory.Exists(filePath))
		{
			Directory.CreateDirectory(filePath);
		}

		LoadDataFromFile();

		CurrentScore = 0;
		floatScore = 0.0f;

		StartScoreTracking();
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("quit"))
		{
			GetTree().ChangeSceneToFile("res://scenes/menu.tscn");
		}

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

		SaveDataOnFile("coin_count", Coins);

		isTrackingScore = false;
	}

	public void SaveDataOnFile(string dataName, int dataToSave)
	{
		string path = Path.Join(filePath, fileName);

		data[dataName] = dataToSave;

		string jsonData = Json.Stringify(data);
		File.WriteAllText(path, jsonData);

		GD.Print($"Saved data on {path}\n\t --> {dataName} : {data[dataName]}");
	}

	public void SaveDataOnFile(string dataName, bool dataToSave)
	{
		string path = Path.Join(filePath, fileName);

		data[dataName] = dataToSave;

		string jsonData = Json.Stringify(data);
		File.WriteAllText(path, jsonData);

		GD.Print($"Saved data on {path}\n\t --> {dataName} : {data[dataName]}");
	}

	public void SaveDataOnFile(string dataName, string dataToSave)
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

			data = (Dictionary)jsonLoader.Data;

			if (!data.ContainsKey("high_score"))
				data.Add("high_score", 0);
			if (!data.ContainsKey("coin_count"))
				data.Add("coin_count", 0);
			if (!data.ContainsKey("skin_2"))
				data.Add("skin_2", false);
			if (!data.ContainsKey("skin_3"))
				data.Add("skin_3", false);
			if (!data.ContainsKey("skin_4"))
				data.Add("skin_4", false);
			if (!data.ContainsKey("active_skin"))
				data.Add("active_skin", "default");

			jsonData = Json.Stringify(data);
			File.WriteAllText(path, jsonData);

			if (error != Error.Ok)
			{
				GD.PrintErr($"Error loading save data: {error}");
			}
		}
		else if (!File.Exists(path))
		{
			GD.Print($"Save file on {path} does not exist, creating one...");

			data = new Dictionary
			{
				{ "high_score", 0 },
				{ "coin_count", 0 },
				{ "skin_2", false },
				{ "skin_3", false },
				{ "skin_4", false },
				{ "active_skin", "default" },
			};

			string jsonData = Json.Stringify(data);
			File.WriteAllText(path, jsonData);
		}

		HighScore = (int)data["high_score"];
		Coins = (int)data["coin_count"];

		GD.Print($"Loaded data from {path}\n\t--> {data}");
	}
}
