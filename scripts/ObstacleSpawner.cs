using Godot;
using System;

public partial class ObstacleSpawner : Node2D
{
	[Export] public PackedScene[] ObstacleScene;
	[Export] public float[] ObstacleRarity = { 50.0f, 30.0f, 20.0f }; // TODO
	[Export] public float SpawnInterval = 2.0f;

	private Timer timer;
	private Random random = new Random();

	public override void _Ready()
	{
		timer = new Timer();
		AddChild(timer);
		timer.WaitTime = SpawnInterval;
		timer.Timeout += SpawnObstacle;
		timer.Start();
	}

	public override void _Process(double delta)
	{

	}

	private void SpawnObstacle()
	{
		if (ObstacleScene == null)
		{
			GD.Print("No obstacle scene assigned");
			return;
		}

		int RandomObstacle = random.Next(ObstacleScene.Length);
		Area2D obstacleInstance = (Area2D)ObstacleScene[RandomObstacle].Instantiate();
		AddChild(obstacleInstance);

		// Ceiling -> -275	Floor -> 275
		float MaxSpawnY = 275.0f, randomY = 0.0f;

		switch (RandomObstacle)
		{
			case 0: // Static Magic Rune
				SpawnMagicRune(obstacleInstance, randomY, MaxSpawnY);
				break;

			case 1: // Rotating Magic Rune
				SpawnRotatingMagicRune(obstacleInstance, randomY, MaxSpawnY);
				break;

			default:
				GD.Print("Error on spawning obstacle");
				break;
		}
	}

	private void SpawnMagicRune(Area2D obstacle, float position, float maxPosition)
	{
		switch (obstacle.RotationDegrees)
		{
			case 0:
				position = (float)GD.RandRange(-maxPosition + 50.0f, maxPosition - 50.0f);
				GD.Print($"Spawned {obstacle.Name} with {obstacle.RotationDegrees}°");
				break;
			case 90:
				position = (float)GD.RandRange(-maxPosition + 125, maxPosition - 125);
				GD.Print($"Spawned {obstacle.Name} with {obstacle.RotationDegrees}°");
				break;
			case 45:
				position = (float)GD.RandRange(-maxPosition + 105, maxPosition - 105);
				GD.Print($"Spawned {obstacle.Name} with {obstacle.RotationDegrees}°");
				break;
			case -45:
				position = (float)GD.RandRange(-maxPosition + 105, maxPosition - 105);
				GD.Print($"Spawned {obstacle.Name} with {obstacle.RotationDegrees}°");
				break;
		}

		obstacle.Position = new Vector2(0, position);
	}

	private void SpawnRotatingMagicRune(Area2D obstacle, float position, float maxPosition)
	{
		position = (float)GD.RandRange(-maxPosition + 125, maxPosition - 125);
		GD.Print($"Spawned {obstacle.Name}");

		obstacle.Position = new Vector2(0, position);
	}
}
