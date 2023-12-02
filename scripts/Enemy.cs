using Godot;
using System;

public partial class Enemy : CharacterBody2D {
	[Export] public int health = 100;
	[Export] public int damage = 10;
	[Export] public float speed = 40f;
	[Export] public float attackRange = 10f;
	[Export] public float attackRate = 2f;
	[Export] public bool ranged = false;
	public PackedScene projectile;
	private Vector2 direction, playerPosition;

	private bool canAttack = true;
	private float setSpeed;

	private AnimationPlayer anim;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		anim = GetChild<AnimationPlayer>(3);
		anim.Play("enemy_walk");
		setSpeed = speed;

		if(ranged)
			projectile = GD.Load("res://scenes/fire.tscn") as PackedScene;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		if(health <= 0) {
			Death();
		} else {
			float distance = Movement((float)delta);

			if(distance < attackRange) {
				speed = 0;

				if(canAttack) {
					if(ranged)
						RangedAttack();
					else
						Attack();
				}
			} else {
				speed = setSpeed;
			}
		}
	}

	public override void _PhysicsProcess(double delta) {
		Movement((float)delta);
	}

	public void _on_animation_player_animation_finished(String anim_name) {
		if(anim_name == "enemy_death") {
			this.QueueFree();
		} else {
			anim.Play("enemy_walk");
		}
	}

	public void TakeDamage(int damage) {
		anim.Play("enemy_take_damage");
		health -= damage;
	}

	public void Knockback(Vector2 direction, float force) {
		MoveAndCollide(direction * force);
	}

	private float Movement(float delta) {
		NavigationAgent2D nav = GetChild<NavigationAgent2D>(4);
		nav.TargetPosition = playerPosition;
		playerPosition = GetParent().GetNode<CharacterBody2D>("Player").Position;
		Vector2 targetPosition = ToLocal(nav.GetNextPathPosition());
		direction = playerPosition - this.Position;
		Vector2 movement = targetPosition.Normalized() * speed;

		MoveAndCollide(movement * delta);

		return MathF.Sqrt(MathF.Pow(direction.X, 2) + MathF.Pow(direction.Y, 2));
	}

	private void Attack() {
		anim.Play("enemy_attack");
		GD.Print("Enemy attacked");
		GetParent().GetNode<Player>("Player").TakeDamage(damage);
		GetChild<Timer>(0).Start(attackRate);
		canAttack = false;
	}

	private void RangedAttack() {
		anim.Play("enemy_attack");
		GD.Print("Enemy attacked");
		if(!canAttack)
			return;

		//Instantiate bullet
		Sprite2D bullet = projectile.Instantiate() as Sprite2D;
		//Add bullet to scene
		bullet.Rotation = this.Position.AngleToPoint(playerPosition);
		bullet.Position = new Vector2(this.Position.X + Mathf.Cos(bullet.Rotation) * 20f, 
		this.Position.Y + Mathf.Sin(bullet.Rotation) * 20f);
		GetParent().AddChild(bullet);
		GetChild<Timer>(0).Start(attackRate);
		canAttack = false;
	}

	private void Death() {
		anim.Play("enemy_death");
		GetChild<CollisionShape2D>(2).QueueFree();
		speed = 0;
		GetParent().GetNode<GameManager>("GameManager").increaseScore(10);
	}

	private void _on_attack_rate_timeout() {
		canAttack = true;
	}
}
