using Godot;
using System;

public partial class gun : Sprite2D {
	[Export]
	public float fireRate = 0.5f;

	public PackedScene bulletScene;

	private bool canShoot = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		bulletScene = GD.Load("res://scenes/bullet.tscn") as PackedScene;
	}

	private void Shoot() {
		if(!canShoot)
			return;

		//Instantiate bullet
		Sprite2D bullet = bulletScene.Instantiate() as Sprite2D;
		//Add bullet to scene
		bullet.Position = new Vector2(GetChild<Node2D>(1).GlobalPosition.X + Mathf.Cos(GlobalRotation), 
		GetChild<Node2D>(1).GlobalPosition.Y + Mathf.Sin(GlobalRotation));
		bullet.Rotation = this.GlobalRotation-this.Rotation;
		GetParent().GetParent().AddChild(bullet);
		GetChild<Timer>(0).Start(fireRate);
		//GetParent().GetChild(1).GetChild<CameraShake>(0).StartShake(1f, 0.1f);
		canShoot = false;
	}

	private void _on_timer_timeout() {
		canShoot = true;
	}
}
