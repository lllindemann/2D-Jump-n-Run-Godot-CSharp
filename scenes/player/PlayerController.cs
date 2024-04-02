using Godot;
using System;

/// <summary>
/// Inherit from this base class to create a singleton class which has access on monobehavior functions.
/// </summary>
public partial class PlayerController : CharacterBody2D
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

	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();


	private Vector2 _velocity = new Vector2();
	private bool _jumping = false;

	private AnimatedSprite2D _Sprite2d;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_Sprite2d = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		SetupAnimation();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ProcessAnimation();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		_velocity = Velocity;
		getInput();

		// add gravity on player when he is in the air
		if (!IsOnFloor())
		{
			_velocity.Y += Gravity * (float)delta;

		}

		Velocity = _velocity;
		MoveAndSlide();
	}


	public void getInput()
	{
		_velocity.X = 0;

		// We check for each move input and update the direction accordingly.
		if (Input.IsKeyPressed(Key.Right) || Input.IsKeyPressed(Key.D))
		{
			_velocity.X += MOVE_SPEED;

		}
		if (Input.IsKeyPressed(Key.Left) || Input.IsKeyPressed(Key.A))
		{
			_velocity.X -= MOVE_SPEED;
		}
		if ((Input.IsKeyPressed(Key.Space) || Input.IsKeyPressed(Key.Up) || Input.IsKeyPressed(Key.W)) && IsOnFloor())
		{
			_jumping = true;
			_velocity.Y = -JUMP_VELOCITY;
		}
	}
	public void SetupAnimation()
	{
		_Sprite2d.Animation = "default";
		_Sprite2d.Play();
		_Sprite2d.SpeedScale = 1;


	}

	public void ProcessAnimation()
	{

		if (Math.Abs(_velocity.X) >= 20)
		{
			_Sprite2d.FlipH = false;

			if (_velocity.X < 0)
			{
				_Sprite2d.FlipH = true;
			}
		}

		if (!IsOnFloor())
		{
			_Sprite2d.Play("fall", 1);
		}
		else if (Math.Abs(_velocity.X) >= 20)
		{
			_Sprite2d.Play("run", 3);
		}
		else
		{
			_Sprite2d.Play("default", 1);
		}


	}
}
