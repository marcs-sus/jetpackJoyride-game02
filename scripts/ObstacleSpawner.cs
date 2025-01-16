using Godot;
using System;

public partial class ObstacleSpawner : Node2D
{
	[Export] public PackedScene[] ObstacleScene;
	[Export] public float SpawnInterval = 2.0f;
	[Export] public float MaxSpawnY = 400.0f;

	private Timer timer;

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

		var obstacleInstance = (Node2D)ObstacleScene[0].Instantiate();
		AddChild(obstacleInstance);

		float randomY = (float)GD.RandRange(-MaxSpawnY / 2, MaxSpawnY / 2);

		obstacleInstance.Position = new Vector2(0, randomY);
	}
}
