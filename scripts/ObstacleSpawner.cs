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
		timer.Timeout += _SpawnObstacle;
		timer.Start();
	}

	public override void _Process(double delta)
	{

	}

	private void _SpawnObstacle()
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
				switch (obstacleInstance.RotationDegrees)
				{
					case 0:
						randomY = (float)GD.RandRange(-MaxSpawnY + 50.0f, MaxSpawnY - 50.0f);
						GD.Print($"Spawned {obstacleInstance.Name} with {obstacleInstance.RotationDegrees}°");
						break;
					case 90:
						randomY = (float)GD.RandRange(-MaxSpawnY + 125, MaxSpawnY - 125);
						GD.Print($"Spawned {obstacleInstance.Name} with {obstacleInstance.RotationDegrees}°");
						break;
					case 45:
						randomY = (float)GD.RandRange(-MaxSpawnY + 105, MaxSpawnY - 105);
						GD.Print($"Spawned {obstacleInstance.Name} with {obstacleInstance.RotationDegrees}°");
						break;
					case -45:
						randomY = (float)GD.RandRange(-MaxSpawnY + 105, MaxSpawnY - 105);
						GD.Print($"Spawned {obstacleInstance.Name} with {obstacleInstance.RotationDegrees}°");
						break;
				}

				obstacleInstance.Position = new Vector2(0, randomY);
				break;

			case 1: // Rotating Magic Rune
				randomY = (float)GD.RandRange(-MaxSpawnY + 125, MaxSpawnY - 125);
				GD.Print($"Spawned {obstacleInstance.Name}");

				obstacleInstance.Position = new Vector2(0, randomY);
				break;

			default:
				GD.Print("Error on spawning obstacle");
				break;
		}
	}
}
