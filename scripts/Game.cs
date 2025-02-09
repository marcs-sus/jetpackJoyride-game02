using Godot;
using System;

public partial class Game : Node2D
{
	private GameManager gameManager;

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("/root/GameManager");

		if (!gameManager.gameMusic.IsPlaying())
		{
			gameManager.gameMusic.Play();
			gameManager.menuMusic.Stop();
		}
	}
}
