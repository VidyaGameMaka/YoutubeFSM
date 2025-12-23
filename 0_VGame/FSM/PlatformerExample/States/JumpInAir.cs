using Godot;

namespace FSM.PlatformerExample;

public partial class JumpInAir : State<PlatformerController> {

	public override void EnterState() {
		// Play animation on state entry
		Actor.PlayAnimation(GetType().Name.ToString());
	}

	public override void ProcessState(double delta) {
		if (Actor.IsOnFloor() == true) {
			Actor.GetStateMachine().ChangeState<Idle>();
		}
	}

	public override void PhysicsProcessState(double delta) {
		// Apply Gravity
		Actor.ApplyGravity(delta);
		// Move character based on input, uses Physics system
		Actor.PhysicsProcessSetVelocityMoveAndSlide(delta);
	}

	public override void ExitState() { }
}
