using Godot;
using System;

public partial class Destroyer : Area2D
{
	private void _OnAreaEntered(Area2D area)
	{
		if (area.IsInGroup("Obstacle") || area.IsInGroup("Coin"))
		{
			area.QueueFree();
			GD.Print($"Destroyed {area.Name}");
		}
	}
}
