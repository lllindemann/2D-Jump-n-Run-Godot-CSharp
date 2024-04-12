using Godot;
using System;
using System.Diagnostics;

public partial class FruitController : Area2D
{
	[Export]
	public SpriteFrames[] SPRITES { get; set; } = { };

	[Signal]
	public delegate void CollectedEventHandler();

	private AnimatedSprite2D _Sprite2d;
	private bool _active = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_Sprite2d = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var rand = new Random();
		_Sprite2d.SpriteFrames = SPRITES[rand.Next(0, SPRITES.Length - 1)];
		_Sprite2d.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{



	}

	private void _on_fruit_body_entered(Node2D body)
	{
		// Check if entrering object is player node
		if (body.Name == "Player")
		{
			_active = false;
			Hide();
			EmitSignal(SignalName.Collected);
		}
	}

	private void Reset()
	{
		_active = true;
		Show();
	}
}
