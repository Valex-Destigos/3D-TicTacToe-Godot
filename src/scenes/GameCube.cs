using Godot;
using System;

public partial class GameCube : Node3D
{
	private CubeCell selectedCell;

	private bool isCrossTurn = false;

	public enum PlayerFigure{
		FREE, OMNI, CROSS, CIRCLE
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InstantiateCube();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton button)
		{
			if (button.Pressed && button.ButtonIndex == MouseButton.Left)
			{
				if (selectedCell != null)
				{
					if (isCrossTurn) 
				    {
						selectedCell.SetPlayerFigure(PlayerFigure.CROSS);
					}
					else
					{
						selectedCell.SetPlayerFigure(PlayerFigure.CIRCLE);
					}
				}
			}
		}
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
                    Vector3 cubePosition = new Vector3(x, y, z);
                    var instance = cubeCellScene.Instantiate();
                    AddChild(instance);

                    if (instance is Node3D node)
                    {
                        node.GlobalScale(Vector3.One * 0.95f);
                        node.GlobalPosition = GlobalPosition + cubePosition;
                    }
                    InitializeCubeCell(cubePosition, instance);
                }
            }
		}
	}

    private void SetSelectedCell(CubeCell cell)
	{
		selectedCell = cell;
	}

	private void ClearSelectedCell()
	{
		selectedCell = null;
	}

	private void SwitchPlayerTurn()
	{
		isCrossTurn = !isCrossTurn;
	}

	private void InitializeCubeCell(Vector3 vec, Node instance)
    {
        if (instance is CubeCell cell)
        {
            if (vec.X == 0 && vec.Y == 0 && vec.Z == 0)
            {
                cell.GetNode<MeshInstance3D>("MeshInstance3D").Visible = false;
                cell.GetNode<MeshInstance3D>("Omni").Visible = true;
            }
            cell.RelativePosition = vec;
            cell.CellSelected += SetSelectedCell;
            cell.CellCleared += ClearSelectedCell;
            cell.PlayerFigureSet += SwitchPlayerTurn;
        }
    }
}
