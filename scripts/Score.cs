using Godot;
using System;

public partial class Score : Label
{
	private GameManager gameManager;

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("/root/GameManager");
	}

	public override void _Process(double delta)
	{
		this.Text = $"{gameManager.CurrentScore.ToString("D4")}M";
	}
}
