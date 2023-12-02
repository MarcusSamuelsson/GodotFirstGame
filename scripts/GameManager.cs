using Godot;
using System;

public partial class GameManager : Node2D
{
	public int score = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void increaseScore(int amount) {
		score += amount;
		GetParent().GetNode<CanvasLayer>("CanvasLayer").
		GetChild(0).GetChild(0).GetChild<Label>(1).Text = "Score: " + score;
	}
}
