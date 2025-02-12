using Godot;
using System;

public partial class GameCube : Node3D
{
	CubeCell selectedCell;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InstantiateCube();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void InstantiateCube()
	{
		var cubeCellScene = GD.Load<PackedScene>("res://src/scenes/cube_cell.tscn");
		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				for (int z = -1; z <= 1; z++)
				{
					var instance = cubeCellScene.Instantiate();
					AddChild(instance);
					if (instance is Node3D node)
					{
						node.GlobalScale(Vector3.One * 0.95f);
						node.GlobalPosition = GlobalPosition + new Vector3(x, y, z);
					}
					if (instance is CubeCell cell)
					{
						cell.CellSelected += SetSelectedCell;
						cell.CellCleared += ClearSelectedCell;
					}
				}
			}
		}
	}

	private void SetSelectedCell(CubeCell cell)
	{
		selectedCell = cell;
		GD.Print($"set cell {cell.Name}");
	}

	private void ClearSelectedCell()
	{
		selectedCell = null;
		GD.Print("set cell to null");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton button)
		{
			if (button.Pressed && button.ButtonIndex == MouseButton.Left)
			{
				if (selectedCell != null)
				{
					GD.Print($"click on cell {selectedCell.Name}");
				}
				else
				{
					GD.Print($"click on empty canvas");
				}

			}
		}
	}
}
