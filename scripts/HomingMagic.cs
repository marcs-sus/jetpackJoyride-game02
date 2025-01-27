using Godot;
using System;

public partial class HomingMagic : Area2D
{
	[Export] public float FLyVelocity = -10.0f;
	[Export] public float WarningTimer = 2.5f;

	private bool isWaningFinished = false;

	private Timer warning;
	private AnimatedSprite2D warningSprite;
	private CharacterBody2D player;
	private GameManager gameManager;

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("../../GameManager");
		player = GetNode<CharacterBody2D>("../../Player");

		warning = GetNode<Timer>("Warning");
		warningSprite = GetNode<AnimatedSprite2D>("WarningSprite");

		warning = new Timer();
		AddChild(warning);
		warning.WaitTime = WarningTimer;
		warning.Timeout += _OnWarningTimeout;
		warning.Start();
	}

	public override void _Process(double delta)
	{
		if (!isWaningFinished)
		{
			if (player != null)
				Position = new Vector2(Position.X, player.Position.Y);
		}
		else if (isWaningFinished)
		{
			Position += new Vector2(FLyVelocity, 0) * ((float)delta * 80.0f);
		}
	}

	private void _OnWarningTimeout()
	{
		warningSprite.Visible = false;
		isWaningFinished = true;
	}

	private void _OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			gameManager.StopScoreTracking();

			body.QueueFree();
		}
	}
}
