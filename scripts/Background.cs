using Godot;
using System;

public partial class Background : Parallax2D
{
	[Export] public float ScrollSpeed = -300.0f;

	private Player player;

	public override void _Ready()
	{
		player = GetNode<Player>("../Player");
	}

	public override void _Process(double delta)
	{
		if (!player.isDead)
		{
			ScrollOffset += new Vector2(ScrollSpeed * (float)delta, 0);
		}
	}
}
