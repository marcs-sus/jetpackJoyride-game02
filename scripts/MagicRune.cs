using Godot;
using System;

public partial class MagicRune : Area2D
{
	[Export] public float FLyVelocity = -5.0f;

	private GameManager gameManager;
	private Random random = new Random();

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("/root/GameManager");

		int rotationCases = random.Next(0, 4);
		switch (rotationCases)
		{
			case 0:
				RotationDegrees = 0;
				break;
			case 1:
				RotationDegrees = 90;
				break;
			case 2:
				RotationDegrees = 45;
				break;
			case 3:
				RotationDegrees = -45;
				break;
			default:
				RotationDegrees = 0;
				GD.PrintErr("Error on defining rotation");
				break;
		}
	}

	public override void _Process(double delta)
	{
		Position += new Vector2(FLyVelocity, 0) * ((float)delta * 80.0f);
	}

	private void _OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			gameManager.StopScoreTracking();

			Player player = (Player)body;
			player.Die();
		}
	}
}
