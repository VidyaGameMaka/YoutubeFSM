using Godot;

namespace FSM.PlatformerExample;

public partial class Jump : State<PlatformerController> {

	public override void EnterState() {
		// Play animation on state entry
		Actor.PlayAnimation(GetType().Name.ToString());

		// Apply Jump Force
		Actor.Velocity = new Vector2(Actor.Velocity.X, -Actor.jumpVelocity);

		//Transition to In-Air
		Actor.GetStateMachine().ChangeState<JumpInAir>();
	}

	public override void ProcessState(double delta) { }

	public override void PhysicsProcessState(double delta) {
		// Apply Gravity
		Actor.ApplyGravity(delta);
		// Move character based on input, uses Physics system
		Actor.PhysicsProcessSetVelocityMoveAndSlide(delta);
	}

	public override void ExitState() { }
}
