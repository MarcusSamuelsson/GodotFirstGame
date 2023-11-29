using Godot;
using System;

public partial class Enemy : CharacterBody2D {
	[Export] public int health = 100;
	[Export] public int damage = 10;
	[Export] public float speed = 1f;
	[Export] public float attackRange = 10f;
	[Export] public float attackRate = 2f;

	private bool canAttack = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		float distance = Movement();

		if(canAttack && distance < attackRange) {
			Attack();
		}
		
		if(health <= 0) {
			Death();
		}
	}

	private float Movement() {
		Vector2 playerPosition = GetParent().GetNode<CharacterBody2D>("Player").Position;
		Vector2 direction = playerPosition - this.Position;
		this.Position += direction.Normalized() * speed;
		this.Rotation = direction.Angle();

		return MathF.Sqrt(MathF.Pow(direction.X, 2) + MathF.Pow(direction.Y, 2));
	}

	private void Attack() {
		GD.Print("Enemy attacked");
		GetParent().GetNode<Player>("Player").health -= damage;
		GetChild<Timer>(0).Start(attackRate);
		canAttack = false;
	}

	private void Death() {
		GD.Print("Enemy died");
		GetParent().GetNode<GameManager>("GameManager").increaseScore(10);
		this.QueueFree();
	}

	private void _on_attack_rate_timeout() {
		canAttack = true;
	}
}
