using Godot;
using System;
using System.Collections.Generic;

public partial class CubeCell : Node3D
{
	[Signal]
	public delegate void CellSelectedEventHandler(CubeCell cell);

	[Signal]
	public delegate void PlayerFigureSetEventHandler();

	[Signal]
	public delegate void CellClearedEventHandler();

	[Export]
	private CollisionObject3D cellCollider;

	public Node3D SelectionIdentifier { get; set; }

	private Vector3 _relativePosition;

	public Vector3 RelativePosition
	{
		get { return _relativePosition; }
		set { _relativePosition = value; }
	}

	private List<CubeCell> ValidNeighbors;
	private List<CubeCell> ValidOpposites;
	private List<CubeCell> ValidCells;

	private List<CubeCell> ActiveCells;

	private Node3D PlayerFigureNode;

	public GameCube.PlayerFigure PlayerFigure { get; set; } = GameCube.PlayerFigure.FREE;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ActiveCells = new List<CubeCell>();
		ValidNeighbors = new List<CubeCell>();
		ValidOpposites = new List<CubeCell>();
		ValidCells = new List<CubeCell>();

		var collider = GetNode<CollisionObject3D>("CellBody");
		if (collider != null)
		{
			collider.MouseEntered += OnMouseEnter;
			collider.MouseExited += OnMouseExit;
		}

		SelectionIdentifier = GetNode<Node3D>("SelectionIdentifier");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnMouseEnter()
	{
		EmitSignal(SignalName.CellSelected, this);
	}

	private void OnMouseExit()
	{
		EmitSignal(SignalName.CellCleared);
	}

	public void AddValidNeighbor(CubeCell neighbor)
	{
		ValidNeighbors.Add(neighbor);
	}

	public void AddValidOpposite(CubeCell opposite)
	{
		ValidOpposites.Add(opposite);
	}

	public void AddActiveCell(CubeCell position)
	{
		ActiveCells.Add(position);
	}

	public bool HasPlayerFigure()
	{
		return PlayerFigureNode != null;
	}

	public bool MatchesPlayerFigure(GameCube.PlayerFigure player)
	{
		return PlayerFigure == player || GameCube.PlayerFigure.OMNI == player;
	}

	public void SetPlayerFigure(GameCube.PlayerFigure figure)
	{
		if (PlayerFigureNode != null)
		{
			return;
		}

		switch (figure)
		{
			case GameCube.PlayerFigure.FREE:
				break;
			case GameCube.PlayerFigure.OMNI:
				PlayerFigure = figure;
				break;
			case GameCube.PlayerFigure.CROSS:
				PlayerFigureNode = GD.Load<PackedScene>("res://src/scenes/cross.tscn").Instantiate() as Node3D;
				PlayerFigure = figure;
				AddChild(PlayerFigureNode);
				break;
			case GameCube.PlayerFigure.CIRCLE:
				PlayerFigureNode = GD.Load<PackedScene>("res://src/scenes/circle.tscn").Instantiate() as Node3D;
				PlayerFigure = figure;
				AddChild(PlayerFigureNode);
				break;
			default:
				GD.Print("unlucky");
				break;
		}

		EmitSignal(SignalName.PlayerFigureSet);
	}

	private void CombineValidCells()
	{
		ValidCells.AddRange(ValidNeighbors);
		ValidCells.AddRange(ValidOpposites);
	}
}
