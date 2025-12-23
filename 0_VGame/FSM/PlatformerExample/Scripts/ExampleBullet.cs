using Godot;

namespace FSM.Bullet;

public partial class ExampleBullet : Node2D {

	private Vector2 _velocity = Vector2.Zero;
	[Export] private float speed = 300f;
	[Export] private float lifeTime = 5f;

	private bool _isInitialized = false;

	public async override void _Ready() {
		// Wait for Init
		while (!_isInitialized) {
			await ToSignal(GetTree(), "process_frame");
		}
		// Delete the bullet after its lifetime expires
		await ToSignal(GetTree().CreateTimer(lifeTime), "timeout");
		QueueFree();
	}

	public void Init(FacingDirections facingDirection) {
		if (facingDirection == FacingDirections.left) {
			_velocity = new Vector2(-speed, 0);
		} else {
			_velocity = new Vector2(speed, 0);
		}
		_isInitialized = true;
	}

	public override void _Process(double delta) {
		// Wait for Init
		if (!_isInitialized) { return; }
		// move the bullet each frame
		Position += _velocity * (float)delta;
	}
}
