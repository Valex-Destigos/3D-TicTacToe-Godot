using Godot;
using System;
using System.Security.AccessControl;

public partial class TargeterCamera : Camera3D
{
	[Export]
	private Node3D Target;
	[Export]
	private float OrbitalRadius;
	[Export]
	private float MouseSensitivity;

	private float Yaw;
	private float Pitch;

	private float MinimalPitch = - Mathf.Pi/3f;
	private float MaximumPitch = Mathf.Pi/3f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(Target.IsInsideTree()){
			var direction = (GlobalTransform.Origin - Target.GlobalTransform.Origin).Normalized();
			Yaw = Mathf.Atan2(direction.X, direction.Z);
			Pitch = Mathf.Asin(direction.Y);
			UpdateCameraPosition();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventMouseMotion motion && Input.IsMouseButtonPressed(MouseButton.Right)){
			Yaw -= motion.Relative.X * MouseSensitivity;
			Pitch = Mathf.Clamp(Pitch - motion.Relative.Y * MouseSensitivity, MinimalPitch, MaximumPitch);
			UpdateCameraPosition();
		}
    }

    private void UpdateCameraPosition(){
		if(!Target.IsInsideTree()){
			return;
		}

		var X = OrbitalRadius * Mathf.Cos(Pitch) * Mathf.Sin(Yaw);
		var Y = OrbitalRadius * Mathf.Sin(Pitch);
		var Z = OrbitalRadius * Mathf.Cos(Pitch) * Mathf.Cos(Yaw);

		Transform3D transform  = GlobalTransform;
		transform.Origin = Target.GlobalTransform.Origin + new Vector3(X,Y,Z);
		GlobalTransform = transform;
		LookAt(Target.GlobalTransform.Origin);
	}
}
