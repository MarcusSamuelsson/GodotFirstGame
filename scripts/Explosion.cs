using Godot;
using System;

public partial class Explosion : Sprite2D {
	[Export] public float growthSpeed = 0.1f;
	[Export] public int[] pixle_size = {48, 48};
	[Export] public float shake_time = 1f;

	public int damage = 100;
	public float force = 100f;
	public float radius = 100f;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		GetParent().GetChild(0).GetChild(0).GetChild(1).GetChild<CameraShake>(0).StartShake(force, shake_time);
	}

	public override void _PhysicsProcess(double delta) {
		if(this.Scale.X * pixle_size[0] >= radius || 
		this.Scale.Y * pixle_size[1] >= radius) {
			this.QueueFree();
		}

		this.Scale += new Vector2(growthSpeed, growthSpeed) * (float)delta;
	}

	public void _on_area_2d_body_entered(Node body)
	{
		if(body is Player) {
			Player player = body as Player;
			float distance = this.GlobalPosition.DistanceTo(player.GlobalPosition);
			player.TakeDamage(100-(int)distance);
			player.Knockback(new Vector2
			(Mathf.Cos(this.Position.AngleToPoint(player.Position)),
			Mathf.Sin(this.Position.AngleToPoint(player.Position))), force);
		} else if(body is Enemy) {
			Enemy enemy = body as Enemy;
			float distance = this.GlobalPosition.DistanceTo(enemy.GlobalPosition);
			enemy.TakeDamage(100-(int)distance);
			enemy.Knockback(-this.Position, force);
		}
	}
}
