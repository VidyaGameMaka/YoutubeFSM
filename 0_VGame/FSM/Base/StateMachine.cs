using Godot;
using System;
using System.Collections.Generic;

namespace FSM;
/// <summary>
/// Base State Machine Logic.
/// </summary>
/// <typeparam name="TStateType"></typeparam>
/// <typeparam name="TActor"></typeparam>
public partial class StateMachine<TActor> : Node where TActor : Node {

	private readonly Dictionary<Type, State<TActor>> _statesDict = new();
	public State<TActor> _currentState { get; private set; }
	public Type CurrentStateType => _currentState != null ? _currentState.GetType() : null;
	public TActor Actor => GetParent<TActor>();

	public void AddState(State<TActor> state) {
		var type = state.GetType();
		if (!_statesDict.ContainsKey(type)) {
			_statesDict.Add(type, state);
		}
	}

	public void ChangeState<TNewState>() {
		var type = typeof(TNewState);
		if (!_statesDict.ContainsKey(type)) {
			GD.PrintErr($"{Actor.Name}: State {type.Name} not found, cannot change state.");
			return;
		}
		var previous = _currentState;
		previous?.ExitState();
		_currentState = _statesDict[type];
		_currentState.Actor = Actor;
		_currentState.EnterState();

		// Emit Godot signal when state changes
		EmitSignal(SignalName.StateChanged, _currentState.GetType().Name.ToString(), previous?.GetType().Name.ToString() ?? "<null>");
	}

	public void Process(double delta) => _currentState?.ProcessState(delta);
	public void PhysicsProcess(double delta) => _currentState?.PhysicsProcessState(delta);
	public bool HasState<TQueryState>() where TQueryState : State<TActor> => _statesDict.ContainsKey(typeof(TQueryState));

	// Godot signal delegate for debugging state
	[Signal] public delegate void StateChangedEventHandler(string newState, string previousState);

	// Debugging helper
	public string GetCurrentStateName() => _currentState.ToString() != null ? _currentState.GetType().Name.ToString() : "<null>";

}
