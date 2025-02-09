using Godot;
using System;

public partial class Coin : Area2D
{
	[Export] public float FLyVelocity = -5.0f;

	private GameManager gameManager;
	private AudioStreamPlayer2D pickupSFX;

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("/root/GameManager");
		pickupSFX = GetNode<AudioStreamPlayer2D>("PickupSFX");
	}

	public override void _Process(double delta)
	{
		Position += new Vector2(FLyVelocity, 0) * ((float)delta * 80.0f);
	}

	private void _OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			gameManager.Coins++;
			pickupSFX.Play();
			Visible = false;
		}
	}
}
