using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float FlySpeed = -500.0f;
	[Export] public float Acceleration = 1000.0f;

	public bool isDead = false;
	private bool wasFlying = false;

	private GameManager gameManager;
	private GpuParticles2D projectileParticles;
	private AnimatedSprite2D animatedSprite;
	private AudioStreamPlayer2D dyingSFX, wandFiringSFX;

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("/root/GameManager");
		projectileParticles = GetNode<GpuParticles2D>("Projectiles");
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		dyingSFX = GetNode<AudioStreamPlayer2D>("DyingSFX");
		wandFiringSFX = GetNode<AudioStreamPlayer2D>("WandFiringSFX");

		isDead = false;
		wasFlying = false;

		animatedSprite.SpriteFrames = (SpriteFrames)GD.Load($"res://assets/sprites/spriteFrames/{gameManager.data["active_skin"]}.tres");
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

			if (Input.IsActionJustPressed("reset"))
			{
				GetTree().ChangeSceneToFile("res://scenes/menu.tscn");
			}

			return;
		}

		if (Input.IsActionPressed("fly"))
		{
			velocity.Y = Mathf.Lerp(velocity.Y, FlySpeed, Acceleration * (float)delta / Math.Abs(FlySpeed));
			projectileParticles.Emitting = true;
			wandFiringSFX.Play();

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
			wandFiringSFX.Stop();

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
		dyingSFX.Play();

		GD.Print("YOU DIED");
	}
}
