using Godot;
using System;

public partial class CsgBox3d : CsgBox3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello :) heehiho");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PrintMessage(String message)
	{
		GD.Print(message);
	}
}
