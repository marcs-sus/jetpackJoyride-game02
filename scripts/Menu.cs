using Godot;
using System;

public partial class Menu : Control
{
	private GameManager gameManager;

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("/root/GameManager");

		if (!gameManager.menuMusic.IsPlaying())
		{
			gameManager.menuMusic.Play();
			gameManager.gameMusic.Stop();
		}
	}

	private void _OnPlayPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}

	private void _OnUnlocksPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/unlocks.tscn");
	}

	private void _OnOptionsPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/options.tscn");
	}

	private void _OnExitPressed()
	{
		GetTree().Quit();
	}
}
