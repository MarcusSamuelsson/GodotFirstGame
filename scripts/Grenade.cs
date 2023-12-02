using Godot;
using System;

public partial class Grenade : Sprite2D {
	[Export] public float fuse_time = 5f;
	[Export] public float explosion_radius = 100f;
	[Export] public float explosion_force = 100f;
	[Export] public int explosion_damage = 100;

	private PackedScene explosion_scene;

	public override void _Ready() {
		explosion_scene = GD.Load("res://scenes/explosion.tscn") as PackedScene;
		GetChild<Timer>(0).Start(fuse_time);
	}

	private void _on_time_until_boom_timeout() {
		Explode();
	}

	private void Explode() {
		//Instantiate explosion
		Explosion explosion = explosion_scene.Instantiate() as Explosion;
		//Add explosion to scene
		explosion.Position = this.Position;
		explosion.radius = explosion_radius;
		explosion.force = explosion_force;
		explosion.damage = explosion_damage;
		GetParent().AddChild(explosion);
		this.QueueFree();
	}
	
}
