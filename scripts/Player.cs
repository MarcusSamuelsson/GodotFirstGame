using Godot;
using System;

public partial class Player : CharacterBody2D {
	[Export]
	public float speed = 100f;
	public int health = 100;

	public PackedScene bulletScene;

	private bool secAvailable = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		GetParent().GetNode<CanvasLayer>("CanvasLayer").
		GetChild(0).GetChild(0).GetChild<Label>(0).Text = "Health: " + health;
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		if(health <= 0) {
			Defeat();
		}
	}

	public override void _PhysicsProcess(double delta) {
		Movement((float)delta);

		//GD.Print(GetGlobalMousePosition());
	}

	public Vector2 GetInput()
    {
        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        return inputDir * speed;
    }

	private void LookAtMouse() {
		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 direction = mousePosition - this.Position;
		this.Rotation = direction.Angle();
	}

	private void Movement(float delta) {
		LookAtMouse();

        MoveAndCollide(GetInput() * delta);
	}

	public override void _Input(InputEvent @event) {
		//call Shoot function if mouse button is pressed down
		if(Input.IsActionJustPressed("shoot")) {
			GetChild(0).Call("Shoot");
		}

		if(Input.IsActionJustPressed("secoundary_shoot")) {
			UseSecoundary();
		}
	}

	private void UseSecoundary() {
		if(!secAvailable)
			return;

		//Instantiate bullet
		PackedScene secondaryPD = GD.Load("res://scenes/grenade.tscn") as PackedScene;
		Sprite2D secondary = secondaryPD.Instantiate() as Sprite2D;
		//Add bullet to scene
		secondary.Position = new Vector2(GetChild<Node2D>(1).GlobalPosition.X + Mathf.Cos(GlobalRotation), 
		GetChild<Node2D>(1).GlobalPosition.Y + Mathf.Sin(GlobalRotation));
		secondary.Rotation = this.GlobalRotation-this.Rotation;
		GetParent().GetParent().AddChild(secondary);
		GetChild<Timer>(0).Start(1f);
		secAvailable = false;
	}

	public void TakeDamage(int damage) {
		health -= damage;
		GetParent().GetNode<CanvasLayer>("CanvasLayer").
		GetChild(0).GetChild(0).GetChild<Label>(0).Text = "Health: " + health;
	}

	public void Knockback(Vector2 direction, float force) {
		MoveAndCollide(direction * force);
	}

	private void Defeat() {
		GD.Print("You died");
	}
}
