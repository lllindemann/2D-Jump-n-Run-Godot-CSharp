using Godot;
using System;

public partial class FruitController : Area2D
{
	[Export]
	public SpriteFrames[] SPRITES { get; set; } = { };

	private AnimatedSprite2D _Sprite2d;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_Sprite2d = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var rand = new Random();
		_Sprite2d.SpriteFrames = SPRITES[rand.Next(0, SPRITES.Length -1 )];
		_Sprite2d.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
