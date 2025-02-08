using Godot;
using System;

public partial class RotatingMagicRune : Area2D
{
	[Export] public float FLyVelocity = -5.0f;
	[Export] public float RotationSpeed = -10.0f;

	private GameManager gameManager;
	private Random random = new Random();

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("/root/GameManager");
	}

	public override void _Process(double delta)
	{
		Position += new Vector2(FLyVelocity, 0) * ((float)delta * 100.0f);

		int rotationSpeedCases = random.Next(0, 3);
		switch (rotationSpeedCases)
		{
			case 0:
				RotationSpeed = -5.0f;
				break;

			case 1:
				RotationSpeed = -10.0f;
				break;

			case 2:
				RotationSpeed = -15.0f;
				break;

			default:
				GD.PrintErr("Error on defining rotation speed");
				break;
		}
		RotationDegrees += RotationSpeed * ((float)delta * 20.0f);
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
