using Godot;
using System;

/// <summary>
/// Controller Class binded to the main Player Object (CharacterBody2D)
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

	[Export]
	public Vector2 SpawnPosition = new Vector2();

	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();


	private Vector2 _velocity = new Vector2();
	private bool _jumping = false;

	private AnimatedSprite2D _Sprite2d;

	#region GODOT_LIFECYCLE_METHODS

	///	<summary>
	///	Called when the node enters the scene tree for the first time.
	///	</summary>
	public override void _Ready()
	{
		_Sprite2d = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		SetupAnimation();
		SpawnPosition = Position;
	}


	/// <summary>
	/// Called every frame
	/// Idle processing
	/// </summary>
	/// <param name="delta">elapsed time since the previous frame</param>
	public override void _Process(double delta)
	{
		ProcessAnimation();
	}

	/// <summary>
	/// Called every fixed time interval
	/// Physics processing: calculations that must happen before each physics step
	/// </summary>
	/// <param name="delta">elapsed time since the previous frame</param>
	public override void _PhysicsProcess(double delta)
	{
		if(Position.Y > 1000){
			Respawn();
		}

		_velocity = Velocity;
		GetInput();

		// add gravity on player when he is in the air
		if (!IsOnFloor())
		{
			_velocity.Y += Gravity * (float)delta;
		}

		Velocity = _velocity;
		MoveAndSlide();
	}
	#endregion

	#region PUBLIC_METHODS

	/// <summary>
	/// Key Control Handling
	/// </summary>
	public void GetInput()
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

	/// <summary>
	/// Initialize the idle default animation of the player sprite
	/// </summary>
	public void SetupAnimation()
	{
		_Sprite2d.Animation = "default";
		_Sprite2d.Play();
		_Sprite2d.SpeedScale = 1;


	}

	/// <summary>
	/// Update the animation of the player sprite via movement direction
	/// </summary>
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
	#endregion

	#region PRIVATE_METHODS
	private void Respawn()
	{
		Position = SpawnPosition;
		_velocity.X = 0;
		_velocity.Y = 0;
	}
	#endregion
}
