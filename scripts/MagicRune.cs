using Godot;
using System;

public partial class MagicRune : Area2D
{
	[Export] public float FLyVelocity = -5.0f;

	private Random random = new Random();

	public override void _Ready()
	{
		Rotation = random.Next(0, 360);
	}

	public override void _Process(double delta)
	{
		Position += new Vector2(FLyVelocity, 0);
	}

	private void _OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			body.QueueFree();
		}
	}
}
