using Godot;
using System;

public partial class CoinsLabel : Label
{
	private GameManager gameManager;

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("%GameManager");
	}

	public override void _Process(double delta)
	{
		this.Text = $"{gameManager.Coins.ToString("D3")}";
	}
}
