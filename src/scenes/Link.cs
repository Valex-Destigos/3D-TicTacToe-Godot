using Godot;
using System;
using System.Collections.Generic;

public partial class Link : Node3D
{
	private CubeCell[] _Cells;
	public CubeCell[] Cells
	{
		get { return _Cells; }
	}
	private Vector3 LastCellPosition;
	private int FillState;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_Cells = new CubeCell[3];
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void AddCell(CubeCell cell)
	{
		_Cells[FillState] = cell;
		FillState++;

		if (FillState == 2)
		{
			CalculateLastCellPosition();
		}
	}

	public bool IsSolved()
	{
		if (_Cells[0] == null || _Cells[1] == null || _Cells[2] == null)
		{
			return false;
		}
		return _Cells[0].MatchesPlayerFigure(_Cells[1].PlayerFigure) &&
			   _Cells[1].MatchesPlayerFigure(_Cells[2].PlayerFigure);
	}

	public Vector3 GetLastCellPosition()
	{
		return LastCellPosition;
	}

	private void CalculateLastCellPosition()
	{
		if (_Cells[0] != null && _Cells[1] != null)
		{
			List<Vector3> line = CalculateVectorLine(_Cells[0].GetPosition(), _Cells[1].GetPosition());
			foreach (Vector3 position in line)
			{
				if (!(_Cells[0].GetPosition().Equals(position) || _Cells[1].GetPosition().Equals(position)))
				{
					LastCellPosition = position;
				}
			}
		}
	}

	private static List<Vector3> CalculateVectorLine(Vector3 vec1, Vector3 vec2)
	{
		List<Vector3> line = new();
		Vector3 direction = (vec1 - vec2).Normalized();
		for (int r = -2; r <= 2; r++)
		{
			Vector3 lineVector = new Vector3(vec1.X + r * direction.X, vec1.Y + r * direction.Y,
					vec1.Z + r * direction.Z);
			if (IsInBounds(lineVector))
			{
				line.Add(lineVector);
			}
		}
		return line;
	}

	private static bool IsInBounds(Vector3 vec)
	{
		return vec.X >= -1 && vec.X <= 1 && vec.Y >= -1 && vec.Y <= 1 && vec.Z >= -1 && vec.Z <= 1;
	}
}
