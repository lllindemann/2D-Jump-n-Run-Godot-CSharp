using Godot;
using System;
using System.Diagnostics;

public partial class GameManager : Node2D
{
	[Export]
	public Node2D FRUITS { get; set; } = null;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach(Area2D fruit in FRUITS.GetChildren()){

			Debug.WriteLine(fruit.Name);

		}
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
