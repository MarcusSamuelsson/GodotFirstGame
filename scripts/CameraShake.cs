using Godot;
using System;

public partial class CameraShake : Camera2D {

	float shake_power = 4;
  	float shake_time = 0.4f;
	public bool isShake = false;
	Vector2 curPos;
	float elapsedtime = 0;

	public override void _Ready() {
		curPos = this.Offset;
	}
		

	public override void _Process(double delta) {
		if(isShake)
			shake((float)delta); 
	}

	public void StartShake(float power, float time) {
		shake_power = power;
		shake_time = time;
		GD.Print("Shake");
		isShake = true;
	}

	public void shake(float delta) {
		if(elapsedtime < shake_time) {
			Offset =  new Vector2(GD.Randf(), GD.Randf()) * shake_power;
			elapsedtime += delta;
		} else {
			isShake = false;
			elapsedtime = 0;
			Offset = curPos; 
		}
			  
	}
}
