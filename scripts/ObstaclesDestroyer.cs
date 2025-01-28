using Godot;
using System;

public partial class ObstaclesDestroyer : Area2D
{
	private void _OnAreaEntered(Area2D area)
	{
		if (area.IsInGroup("Obstacle"))
		{
			area.QueueFree();
			GD.Print($"Destroyed {area.Name}");
		}
	}
}
