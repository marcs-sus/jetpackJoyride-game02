using Godot;
using System;
using System.Collections;
using System.IO;

public partial class Unlocks : Control
{
	[Export] public int[] SkinCost = { 10, 20, 30 };

	private CheckButton[] skinCheckButtons = new CheckButton[3];
	private GameManager gameManager;
	private Label coinsLabel;

	private readonly string filePath = ProjectSettings.GlobalizePath("user://saves/");
	private readonly string fileName = "save_data.json";

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("/root/GameManager");

		if (!gameManager.menuMusic.IsPlaying())
		{
			gameManager.menuMusic.Play();
			gameManager.gameMusic.Stop();
		}


		coinsLabel = GetNode<Label>("CoinsLabel");

		for (int i = 0; i < skinCheckButtons.Length; i++)
		{
			skinCheckButtons[i] = GetNode<CheckButton>($"VBoxContainer/GridContainer/Skin{i + 2}");
			//GD.Print($"{skinCheckButtons[i].Name} assigned");
		}

		switch ((string)gameManager.data["active_skin"])
		{
			case "skin_2":
				skinCheckButtons[0].ButtonPressed = true;
				break;

			case "skin_3":
				skinCheckButtons[1].ButtonPressed = true;
				break;

			case "skin_4":
				skinCheckButtons[2].ButtonPressed = true;
				break;
		}

		for (int i = 0; i < skinCheckButtons.Length; i++)
		{
			if ((bool)gameManager.data[$"skin_{i + 2}"])
				skinCheckButtons[i].Text = $"Skin {i + 2}";
		}
	}

	public override void _Process(double delta)
	{
		coinsLabel.Text = $"{gameManager.Coins:D3}";
	}

	private void _OnBackPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/menu.tscn");
	}

	private void UpdateUnlockables(string skinKey, int idx)
	{
		try
		{
			if (!(bool)gameManager.data[skinKey] && gameManager.Coins < SkinCost[idx])
			{
				skinCheckButtons[idx].ButtonPressed = false;
				GetNode<CheckButton>("VBoxContainer/GridContainer/Skin1").ButtonPressed = true;
			}
			else if (!(bool)gameManager.data[skinKey] && gameManager.Coins >= SkinCost[idx])
			{
				gameManager.Coins -= SkinCost[idx];
				skinCheckButtons[idx].Text = $"Skin {idx + 2}";

				gameManager.SaveDataOnFile(skinKey, true);
				gameManager.SaveDataOnFile("coin_count", gameManager.Coins);
				gameManager.SaveDataOnFile("active_skin", skinKey);
			}
			else if ((bool)gameManager.data[skinKey])
			{
				gameManager.SaveDataOnFile("active_skin", skinKey);
			}
		}
		catch (Exception ex)
		{
			GD.PrintErr($"Error: {ex}");
		}
	}

	private void _OnSkin1Toggled(bool skinToggle)
	{
		gameManager.SaveDataOnFile("active_skin", "default");
	}

	private void _OnSkin2Toggled(bool skinToggled)
	{
		UpdateUnlockables("skin_2", 0);
	}

	private void _OnSkin3Toggled(bool skinToggled)
	{
		UpdateUnlockables("skin_3", 1);
	}

	private void _OnSkin4Toggled(bool skinToggled)
	{
		UpdateUnlockables("skin_4", 2);
	}
}
