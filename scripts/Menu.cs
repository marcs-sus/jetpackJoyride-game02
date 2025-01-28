using Godot;
using System;

public partial class Menu : Control
{
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
