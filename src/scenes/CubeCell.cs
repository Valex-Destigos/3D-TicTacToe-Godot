using Godot;
using System;

public partial class CubeCell : Node3D
{
	[Signal]
	public delegate void CellSelectedEventHandler(CubeCell cell);

	[Signal]
	public delegate void CellClearedEventHandler();

	[Export]
	CollisionObject3D cellCollider;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var collider = GetNode<CollisionObject3D>("CellBody");
		if (collider != null)
		{
			collider.MouseEntered += OnMouseEnter;
			collider.MouseExited += OnMouseExit;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnMouseEnter()
	{
		GD.Print($"hover on cell {Name}");
		EmitSignal(SignalName.CellSelected, this);
	}

	private void OnMouseExit()
	{
		GD.Print($"exit cell {Name}");
		EmitSignal(SignalName.CellCleared);
	}
}
