using Godot;
using System;

public partial class GameManager : Node
{
	[Export] public int ScoreIncreaseRate = 2;

	public int CurrentScore  = 0;
	public int HighScore = 0;

	private bool isTrackingScore = false;
	private float floatScore = 0.0f;

	public override void _Ready()
	{
		CurrentScore  = 0;
		floatScore = 0.0f;

		StartScoreTracking();
	}

	public override void _Process(double delta)
	{
		if (isTrackingScore)
		{
			CurrentScore += (int)(floatScore += (float)delta * ScoreIncreaseRate);
			floatScore %= 1.0f;
		}
	}

	public void StartScoreTracking()
	{
		isTrackingScore = true;
	}

	public void StopScoreTracking()
	{
		isTrackingScore = false;

		if (CurrentScore > HighScore)
		{
			HighScore = CurrentScore;
		}
	}
}
