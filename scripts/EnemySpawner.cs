using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class EnemySpawner : Sprite2D {

	private PackedScene[] enemies = 
	{GD.Load("res://scenes/slime_red.tscn") as PackedScene, 
	GD.Load("res://scenes/fire_enemy.tscn") as PackedScene,
	GD.Load("res://scenes/slime_green.tscn") as PackedScene,};
	private AnimationPlayer anim;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		anim = GetChild<AnimationPlayer>(1);
		anim.Play("enemySpawner_idle");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		//Rotate spawner
		this.Rotation -= 0.01f;


	}

	public void _on_animation_player_animation_finished(String anim_name) {
		anim.Play("enemySpawner_idle");
	}

	private void _on_timer_timeout() {
		GD.Print("Spawn enemy");

		//Spawn enemy
		anim.Play("enemySpawner_spawn");
		PackedScene enemyPS = enemies[GD.Randi() % enemies.Length];
		CharacterBody2D enemy = enemyPS.Instantiate() as CharacterBody2D;
		GetParent().AddChild(enemy);
		enemy.Position = this.Position;
	}
}
