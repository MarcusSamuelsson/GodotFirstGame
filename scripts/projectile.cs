using Godot;
using System;

public partial class projectile : Sprite2D
{
	[Export]
	public float speed = 10f;
	private Vector2 forward;
	public override void _Process(double delta) {
		forward = new Vector2(Mathf.Cos(Rotation)*speed, Mathf.Sin(Rotation)*speed);
		this.Position += forward;
	}

	private void _on_area_2d_body_entered(Node2D body) {
		GD.Print("Bullet hit something");
		
		if(body is Player) {
			Player player = body as Player;
			player.TakeDamage(10);
			player.Knockback(forward, 1f);
		}

		this.QueueFree();
	}

	private void _on_timer_timeout() {
		this.QueueFree();
	}
}
