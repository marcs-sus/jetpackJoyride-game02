using Godot;
using System;

public partial class ObstaclesDestroyer : Area2D
{
	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{

	}

	private void _OnAreaEntered(Area2D area)
	{
		if (area.IsInGroup("Obstacle"))
		{
			area.QueueFree();
			GD.Print($"Destroyed {area.Name}");
		}
	}
}
