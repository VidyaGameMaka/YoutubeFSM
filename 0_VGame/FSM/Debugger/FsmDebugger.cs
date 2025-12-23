using Godot;
using System;


namespace FSM.Debugger;
/// <summary>
/// FSM Debugger displays current and previous state of a State Machine.
/// This is generic and will work with with any FSM Controller.
/// </summary>
public partial class FsmDebugger : CanvasLayer {

	[Export] private Label myLabel;
	[Export] public Node fsmNode;

	private string trackingText = "";

	public override void _Ready() {
		Node stateMachine = fsmNode;
		if (stateMachine == null) {
			GD.PrintErr("FSM Debugger: fsmNode not assigned.");
			return;
		}

		// Set tracking text to the name of the FSM node
		trackingText = fsmNode?.Name ?? "<None>";

		// If fsmNode is not the StateMachine, try to find it as a child
		if (!stateMachine.GetType().Name.Contains("StateMachine")) {
			foreach (Node child in stateMachine.GetChildren()) {
				if (child.GetType().Name.Contains("StateMachine")) {
					stateMachine = child;
					break;
				}
			}
		}

		if (stateMachine.HasSignal("StateChanged")) {
			// Connect Godot Signal to State Machine
			stateMachine.Connect("StateChanged", new Callable(this, nameof(OnStateChanged)));
			// Immediately get and display the current state
			var currentState = stateMachine.Call("GetCurrentStateName");
			myLabel.Text = $"{trackingText}\nPrevious: <None>\nCurrent: {currentState}";
		} else {
			GD.PrintErr("FSM Debugger: StateMachine node does not have the expected signal.");
		}
	}

	private void OnStateChanged(string newState, string previousState) {
		myLabel.Text = $"{trackingText}\nPrevious: {previousState}\nCurrent: {newState}";
	}



}
