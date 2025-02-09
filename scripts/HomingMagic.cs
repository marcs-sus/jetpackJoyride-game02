using Godot;
using System;

public partial class HomingMagic : Area2D
{
	[Export] public float FLyVelocity = -10.0f;
	[Export] public float WarningTimer = 2.5f;
	[Export] public float HomingSpeed = 1.0f;

	private bool isWaningFinished = false;

	private Timer warning;
	private Sprite2D warningSprite;
	private Player player;
	private GameManager gameManager;
	private AudioStreamPlayer2D warningSFX, homingMagicSFX;

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("/root/GameManager");
		player = GetNode<Player>("../../Player");

		warningSFX = GetNode<AudioStreamPlayer2D>("WarningSFX");
		homingMagicSFX = GetNode<AudioStreamPlayer2D>("HomingMagicSFX");

		warning = GetNode<Timer>("Warning");
		warningSprite = GetNode<Sprite2D>("WarningSprite");

		warning = new Timer();
		AddChild(warning);
		warningSFX.Play();
		warning.WaitTime = WarningTimer;
		warning.Timeout += _OnWarningTimeout;
		warning.Start();
	}

	public override void _Process(double delta)
	{
		if (isWaningFinished)
		{
			Position += new Vector2(FLyVelocity, 0) * ((float)delta * 80.0f);
		}
		else if (!isWaningFinished && !player.isDead)
		{
			Position = new Vector2(Position.X, Mathf.Lerp(Position.Y, player.Position.Y, HomingSpeed * (float)delta));
		}
	}

	private void _OnWarningTimeout()
	{
		warningSprite.Visible = false;
		isWaningFinished = true;

		homingMagicSFX.Play();
	}

	private void _OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			gameManager.StopScoreTracking();

			player.Die();
		}
	}
}
