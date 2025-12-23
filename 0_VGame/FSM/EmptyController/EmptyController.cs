using Godot;

namespace FSM.EmptyExample;
/// <summary>
/// Empty FSM Controller.
/// </summary>
public partial class EmptyController : StateMachineController<EmptyController> {

	// 1) Nodes used by this state machine controller.

	// 2) User adjustable properties.

	// 3) Non-user editable properties.

	// 4) Configure states used by this state machine controller.

	protected override void ConfigureStates(StateMachine<EmptyController> fsm) {
		fsm.AddState(new FirstState());
	}

	// 5) Initialize starting state.
	protected override void InitFSM() {
		_fsm.ChangeState<FirstState>();
	}

	//----- Shared helpers and utilities -----//


}
