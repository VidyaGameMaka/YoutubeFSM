using Godot;

namespace FSM.PlatformerExample;

public partial class Idle : State<PlatformerController> {

	public override void EnterState() {
		// Play animation on state entry
		Actor.PlayAnimation(GetType().Name.ToString());

		// *Edge Case: If in not on ground on entry: transition to -> JumpInAir
		if (Actor.IsOnFloor() == false) {
			Actor.GetStateMachine().ChangeState<JumpInAir>();
		}
	}

	public override void ProcessState(double delta) {
		// Jump button pressed: transition to -> Jump
		if (Input.IsActionJustPressed(InputButtonNames.jump.ToString()) && Actor.IsOnFloor()) {
			Actor.GetStateMachine().ChangeState<Jump>();
			return;
		}

		// Attack button pressed: transition to -> Attack
		if (Input.IsActionJustPressed(InputButtonNames.attack.ToString())) {
			Actor.GetStateMachine().ChangeState<Attack>();
			return;
		}

		// Input detected: transition to -> Walk
		if (Actor.InputDirection().Length() >= Actor.minimumInputLength) {
			Actor.GetStateMachine().ChangeState<Walk>();
			return;
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
