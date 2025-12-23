using Godot;

namespace FSM.EmptyExample;

public partial class FirstState : State<EmptyController> {

	public override void EnterState() {
		GD.Print("Entered First State in the EmptyController FSM.");
	}

	public override void ProcessState(double delta) {
	}

	public override void PhysicsProcessState(double delta) {
	}

	public override void ExitState() {
	}

}
