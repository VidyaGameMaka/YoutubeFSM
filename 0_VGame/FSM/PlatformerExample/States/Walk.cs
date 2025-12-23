using Godot;

namespace FSM.PlatformerExample;

public partial class Walk : State<PlatformerController> {

	public override void EnterState() {
		// Play Animation on state entry
		Actor.PlayAnimation(GetType().Name.ToString());
	}


	public override void ProcessState(double delta) {
		// Flip character based on input
		Actor.FlipCharacter();

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

		// Input not present transition to -> Idle
		if (Actor.InputDirection().Length() < Actor.minimumInputLength) {
			Actor.GetStateMachine().ChangeState<Idle>();
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
