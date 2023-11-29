using Godot;
using System;

public partial class bullet : Sprite2D{
	[Export]
	public float speed = 10f;
	public override void _Process(double delta) {
		Vector2 forward = new Vector2(Mathf.Cos(Rotation)*speed, Mathf.Sin(Rotation)*speed);
		this.Position += forward;
	}

	private void _on_area_2d_body_entered(Node2D body) {
		GD.Print("Bullet hit something");
		
		if(body is Enemy) {
			Enemy enemy = body as Enemy;
			enemy.health -= 10;
			this.QueueFree();
		}
	}

	private void _on_timer_timeout() {
		this.QueueFree();
	}
}



