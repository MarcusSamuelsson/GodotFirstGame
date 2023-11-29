using Godot;
using System;

public partial class Player : CharacterBody2D {
	[Export]
	public float speed = 2f;
	public int health = 100;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		Movement();

		if(health <= 0) {
			Defeat();
		}
	}

	

	private void LookAtMouse() {
		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 direction = mousePosition - this.Position;
		this.Rotation = direction.Angle();
	}

	private void Movement() {
		LookAtMouse();

		if(Input.IsKeyPressed(Key.W)) {
			this.Position += new Vector2(0, -speed);
		}

		if(Input.IsKeyPressed(Key.S)) {
			this.Position += new Vector2(0, speed);
		}

		if(Input.IsKeyPressed(Key.A)) {
			this.Position += new Vector2(-speed, 0);
		}

		if(Input.IsKeyPressed(Key.D)) {
			this.Position += new Vector2(speed, 0);
		}
	}

	public override void _Input(InputEvent @event) {
		//call Shoot function if mouse button is pressed down
		if(@event is InputEventMouseButton eventMouseButton) {
			GetChild(0).Call("Shoot");
		}
	}

	private void Defeat() {
		GD.Print("You died");
	}
}
