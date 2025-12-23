using Godot;

namespace FSM;
/// <summary>
/// Base Controller with only the minimum code needed to perform FSM functions.
/// </summary>
/// <typeparam name="TState"></typeparam>
/// <typeparam name="TController"></typeparam>
public abstract partial class StateMachineController<TController> : CharacterBody2D
	where TController : StateMachineController<TController> {
	protected StateMachine<TController> _fsm;
	protected abstract void ConfigureStates(StateMachine<TController> fsm);
	public override void _Ready() {
		_fsm = new StateMachine<TController>();
		AddChild(_fsm);
		ConfigureStates(_fsm);
		InitFSM();
	}
	protected virtual void InitFSM() { }
	public override void _Process(double delta) => _fsm?.Process(delta);
	public override void _PhysicsProcess(double delta) => _fsm?.PhysicsProcess(delta);
	public StateMachine<TController> GetStateMachine() => _fsm;

}
