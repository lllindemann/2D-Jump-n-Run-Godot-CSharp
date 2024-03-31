using Godot;
using System;

public partial class player_controller : CharacterBody2D
{
	// variables relevant for main player movement
	[Export]
	public int MAX_SPEED { get; set; } = 150;
	[Export]
	public int JUMP_SPEED { get; set; } = 400;
	[Export]
	public float FRICTION { get; set; } = 10;
	[Export]
	public float MOVE_SPEED = 150.0f;
	[Export]
	public float JUMP_VELOCITY = 400.0f;

	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();


	Vector2 velocity = new Vector2();
	bool jumping = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;
		getInput();

		// add gravity on player when he is in the air
		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float) delta;

		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void getInput()
	{

		velocity.X = 0;

		// We check for each move input and update the direction accordingly.
		if (Input.IsKeyPressed(Key.Right) || Input.IsKeyPressed(Key.D))
		{
			velocity.X += MOVE_SPEED;

		}
		 if (Input.IsKeyPressed(Key.Left) || Input.IsKeyPressed(Key.D))
		{
			velocity.X -= MOVE_SPEED;
		}
		 if ((Input.IsKeyPressed(Key.Space) || Input.IsKeyPressed(Key.Up)) && IsOnFloor())
		{
			jumping = true;
			velocity.Y = - JUMP_VELOCITY;
		}
	}
}
