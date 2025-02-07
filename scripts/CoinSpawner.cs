using Godot;
using System;

public partial class CoinSpawner : Node2D
{
	[Export] public PackedScene CoinScene;
	[Export] public float SpawnInterval = 2.5f;

	private Timer timer;
	private Player player;

	public override void _Ready()
	{
		player = GetNode<Player>("../Player");

		timer = new Timer();
		AddChild(timer);
		timer.WaitTime += SpawnInterval;
		timer.Timeout += _OnTimerTimeout;
		timer.Start();
	}

	private void _OnTimerTimeout()
	{
		SpawnCoin();
	}

	private void SpawnCoin()
	{
		if (player.isDead) return;

		if (CoinScene == null)
		{
			GD.PrintErr("No obstacle scene assigned");
			return;
		}

		Area2D coinInstance = (Area2D)CoinScene.Instantiate();
		AddChild(coinInstance);

		// Ceiling -> -275	Floor -> 275
		const float MaxSpawnY = 275.0f;

		float position = (float)GD.RandRange(-MaxSpawnY + 25.0f, MaxSpawnY - 25.0f);
		GD.Print($"Spawned {coinInstance.Name}");

		coinInstance.Position = new Vector2(0, position);
	}
}
