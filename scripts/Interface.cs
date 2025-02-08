using Godot;
using System;

public partial class Interface : Control
{
	private Label gameOverLabel, scoreLabel, highScoreLabel, coinsLabel;
	private GameManager gameManager;
	private Player player;

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("/root/GameManager");
		player = GetNode<Player>("../Player");

		gameOverLabel = GetNode<Label>("GameOver");
		highScoreLabel = GetNode<Label>("HighScore");
		coinsLabel = GetNode<Label>("CoinsLabel");
		scoreLabel = GetNode<Label>("Score");

		gameOverLabel.Visible = false;
	}

	public override void _Process(double delta)
	{
		scoreLabel.Text = $"{gameManager.CurrentScore:D4}M";
		highScoreLabel.Text = $"Best: {gameManager.HighScore:D4}M";
		coinsLabel.Text = $"{gameManager.Coins:D3}";

		if (player.isDead)
		{
			gameOverLabel.Visible = true;
		}
	}
}
