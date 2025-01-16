using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float FlySpeed = -400.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		if (Input.IsActionPressed("fly"))
		{
			velocity.Y = FlySpeed;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
