using Godot;
using System;
using System.Linq;

public partial class ObstacleSpawner : Node2D
{
	[Export] public PackedScene[] ObstacleScene;
	[Export] public int[] ObstacleRarity = { 40, 35, 25 }; // Must contain same number and order as ObstacleScene
	[Export] public float SpawnInterval = 2.0f;

	private Timer timer;
	private Player player;
	private Random random = new Random();

	public override void _Ready()
	{
		player = GetNode<Player>("../Player");

		timer = new Timer();
		AddChild(timer);
		timer.WaitTime = SpawnInterval;
		timer.Timeout += _OnTimerTimeout;
		timer.Start();
	}

	private void _OnTimerTimeout()
	{
		SpawnObstacle();
	}

	private void SpawnObstacle()
	{
		if (player.isDead) return;

		if (ObstacleScene == null)
		{
			GD.PrintErr("No obstacle scene assigned");
			return;
		}

		int RandomizedObstacle = GetRandomizedObstacle();
		Area2D obstacleInstance = (Area2D)ObstacleScene[RandomizedObstacle].Instantiate();
		AddChild(obstacleInstance);

		// Ceiling -> -275	Floor -> 275
		const float MaxSpawnY = 275.0f;

		switch (RandomizedObstacle)
		{
			case 0: // Static Magic Rune
				SpawnMagicRune(obstacleInstance, MaxSpawnY);
				break;

			case 1: // Rotating Magic Rune
				SpawnRotatingMagicRune(obstacleInstance, MaxSpawnY);
				break;

			case 2: // Homing Magic Projectile
				SpawnHomingMagic(obstacleInstance, MaxSpawnY);
				break;

			default:
				GD.PrintErr("Error on spawning obstacle");
				break;
		}
	}

	private int GetRandomizedObstacle()
	{
		int randomWeight = random.Next(ObstacleRarity.Sum()); // Linq.Sum()

		int cumulativeWeight = 0;
		for (int i = 0; i < ObstacleRarity.Length; i++)
		{
			cumulativeWeight += ObstacleRarity[i];
			if (randomWeight < cumulativeWeight)
				return i;
		}

		return -1;
	}

	private void SpawnMagicRune(Area2D obstacle, float maxPosition)
	{
		float position = 0.0f;

		switch (obstacle.RotationDegrees)
		{
			case 0:
				position = (float)GD.RandRange(-maxPosition + 50.0f, maxPosition - 50.0f);
				GD.Print($"Spawned {obstacle.Name} with {obstacle.RotationDegrees}°");
				break;
			case 90:
				position = (float)GD.RandRange(-maxPosition + 125.0f, maxPosition - 125.0f);
				GD.Print($"Spawned {obstacle.Name} with {obstacle.RotationDegrees}°");
				break;
			case 45:
				position = (float)GD.RandRange(-maxPosition + 105.0f, maxPosition - 105.0f);
				GD.Print($"Spawned {obstacle.Name} with {obstacle.RotationDegrees}°");
				break;
			case -45:
				position = (float)GD.RandRange(-maxPosition + 105.0f, maxPosition - 105.0f);
				GD.Print($"Spawned {obstacle.Name} with {obstacle.RotationDegrees}°");
				break;
		}

		obstacle.Position = new Vector2(0, position);
	}

	private void SpawnRotatingMagicRune(Area2D obstacle, float maxPosition)
	{
		float position = (float)GD.RandRange(-maxPosition + 125.0f, maxPosition - 125.0f);
		GD.Print($"Spawned {obstacle.Name}");

		obstacle.Position = new Vector2(0, position);
	}

	private void SpawnHomingMagic(Area2D obstacle, float maxPosition)
	{
		float position = !player.isDead ? player.Position.Y : 0.0f;
		GD.Print($"Spawned {obstacle.Name}");

		obstacle.Position = new Vector2(0, position);
	}
}
