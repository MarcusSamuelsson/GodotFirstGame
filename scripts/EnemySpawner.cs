using Godot;
using System;

public partial class EnemySpawner : Sprite2D {

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		//Rotate spawner
		this.Rotation += 0.01f;


	}

	private void _on_timer_timeout() {
		GD.Print("Spawn enemy");

		//Spawn enemy
		PackedScene enemyPS = GD.Load("res://scenes/enemy.tscn") as PackedScene;
		CharacterBody2D enemy = enemyPS.Instantiate() as CharacterBody2D;
		GetParent().AddChild(enemy);
		enemy.Position = this.Position;
	}
}
