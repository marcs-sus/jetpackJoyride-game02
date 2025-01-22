using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float FlySpeed = -500.0f;
	[Export] public float Acceleration = 1000.0f;

	private CpuParticles2D projectileParticles;

	public override void _Ready()
	{
		projectileParticles = GetNode<CpuParticles2D>("Projectiles");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		/* if (!IsOnFloor())
			velocity += GetGravity() * (float)delta; */

		if (Input.IsActionPressed("fly"))
		{
			velocity.Y = Mathf.Lerp(velocity.Y, FlySpeed, Acceleration * (float)delta / Math.Abs(FlySpeed));
			projectileParticles.Emitting = true;
		}
		else if (!Input.IsActionPressed("fly"))
		{
			velocity += GetGravity() * (float)delta;
			projectileParticles.Emitting = false;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
