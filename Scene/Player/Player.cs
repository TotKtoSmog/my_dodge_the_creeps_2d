using Godot;
using System;
using System.Formats.Asn1;

public partial class Player : Area2D
{	
	[Export]
	public int Speed {get;set;} = 400;

	public Vector2 ScreenSize;
	public override void _Ready() => ScreenSize = GetViewportRect().Size;
	public override void _Process(double delta)
	{
		Move((float)delta);

	}

	public void Move(float delta)
	{
		var velocity = Vector2.Zero;

		if (Input.IsActionPressed("move_right"))
			velocity.X += 1;
		if (Input.IsActionPressed("move_left"))
			velocity.X -= 1;
		if (Input.IsActionPressed("move_down"))
			velocity.Y += 1;
		if (Input.IsActionPressed("move_up"))
			velocity.Y -= 1;
		PlayAnimation(ref velocity);
		Clamp(velocity, delta);

	}
	public void PlayAnimation(ref Vector2  velocity)
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}
	}
	public void Clamp(Vector2 velocity, float delta)
	{
		Position += velocity * delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
	}
}
