using Godot;
using System;

public partial class HighScore : Label
{
	private GameManager gameManager;

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("/root/GameManager");
	}

	public override void _Process(double delta)
	{
		this.Text = $"Best: {gameManager.HighScore.ToString("D4")}M";
	}
}
