using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float FlySpeed = -500.0f;
	[Export] public float Acceleration = 1000.0f;

	public bool isDead = false;
	private bool wasFlying = false;

	private GpuParticles2D projectileParticles;
	private AnimatedSprite2D animatedSprite;


	public override void _Ready()
	{
		projectileParticles = GetNode<GpuParticles2D>("Projectiles");
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (isDead)
		{
			if (!IsOnFloor())
			{
				velocity += GetGravity() * (float)delta;
			}
			Velocity = velocity;
			MoveAndSlide();

			return;
		}

		if (Input.IsActionPressed("fly"))
		{
			velocity.Y = Mathf.Lerp(velocity.Y, FlySpeed, Acceleration * (float)delta / Math.Abs(FlySpeed));
			projectileParticles.Emitting = true;

			if (!wasFlying)
			{
				animatedSprite.Play("flying");
				wasFlying = true;
			}
		}
		else if (!Input.IsActionPressed("fly"))
		{
			velocity += GetGravity() * (float)delta;
			projectileParticles.Emitting = false;

			if (wasFlying)
			{
				animatedSprite.PlayBackwards("flying");
				wasFlying = false;
			}

			if (!animatedSprite.IsPlaying())
			{
				animatedSprite.Play("walking");
			}
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void Die()
	{
		if (isDead) return;

		isDead = true;
		animatedSprite.Play("dying");
		projectileParticles.Emitting = false;

		ShaderMaterial bgShader = (ShaderMaterial)GetNode<TextureRect>("../Background").Material;
		bgShader.SetShaderParameter("speed", 0.0f);

		GD.Print("YOU DIED");
	}
}
