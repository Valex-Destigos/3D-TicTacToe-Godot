using Godot;

public partial class Env : Node
{
	// Member variables here, example:
	private int _a = 2;
	private string _b = "textvar";

	public override void _Ready()
	{
		// Called every time the node is added to the scene.
		// Initialization here.
		
		CsgBox3d box = new CsgBox3d();
		
		GD.Print("Hello from C# to Godot :)");

		box.PrintMessage("Galigrü von Voriel Tsigôt!");
	}

	public override void _Process(double delta)
	{
		// Called every frame. Delta is time since the last frame.
		// Update game logic here.
	}
}
