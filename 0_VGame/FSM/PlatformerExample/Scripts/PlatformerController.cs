using Godot;

namespace FSM.PlatformerExample;
/// <summary>
/// Example FSM Controller demonstrating how to set up a simple FSM using the base FSM classes.
/// Steps 1 to 5 should be reviewed and changed for your FSM.
/// </summary>
public partial class PlatformerController : StateMachineController<PlatformerController> {

	// 1) Nodes used by this state machine controller.
	[Export] private AnimationPlayer _animPlayer;
	[Export] private Sprite2D _characterSprite;
	[Export] private PackedScene bulletPfb;

	// 2) User adjustable properties.
	[Export] private float moveSpeed = 200f;
	[Export] public float jumpVelocity { get; private set; } = 400.0f;
	[Export] private Vector2I bulletSpawnOffset = new Vector2I(0, -16);

	// 3) Non-user editable properties.
	private FacingDirections facingDirection = FacingDirections.right;
	public float minimumInputLength { get; private set; } = 0.05f;

	// 4) Configure states used by this state machine controller.
	protected override void ConfigureStates(StateMachine<PlatformerController> fsm) {
		fsm.AddState(new Idle());
		fsm.AddState(new Walk());
		fsm.AddState(new Jump());
		fsm.AddState(new JumpInAir());
		fsm.AddState(new Attack());
	}

	// 5) Initialize starting state.
	protected override void InitFSM() {
		_fsm.ChangeState<Idle>();
	}


	//----- Shared helpers and utilities -----//
	public Sprite2D CharacterSprite => _characterSprite;
	public void PlayAnimation(string animName) {
		if (_animPlayer != null) {
			_animPlayer.Play(animName);
		}
	}

	public Vector2 InputDirection() {
		return Input.GetVector(
			InputAxisNames.left.ToString(), InputAxisNames.right.ToString(),
			InputAxisNames.up.ToString(), InputAxisNames.down.ToString()
		);
	}

	public void FlipCharacter() {
		if (InputDirection().X < 0) {
			facingDirection = FacingDirections.left;
			_characterSprite.FlipH = true;
		} else if (InputDirection().X > 0) {
			facingDirection = FacingDirections.right;
			_characterSprite.FlipH = false;
		}
	}

	public void ApplyGravity(double delta) {
		// Apply gravity if not on the floor.
		if (IsOnFloor() == false) {
			Velocity += GetGravity() * (float)delta;
		}
	}

	public void PhysicsProcessSetVelocityMoveAndSlide(double delta) {
		Velocity = new Vector2(InputDirection().X * moveSpeed, Velocity.Y);
		MoveAndSlide();
	}

	public void SpawnBullet() {
		if (bulletPfb == null) { return; }
		FSM.Bullet.ExampleBullet bullet = bulletPfb.Instantiate<FSM.Bullet.ExampleBullet>();
		GetTree().Root.AddChild(bullet);
		bullet.Position = new Vector2(Position.X + bulletSpawnOffset.X, Position.Y + bulletSpawnOffset.Y);
		bullet.Init(facingDirection);
	}

}
