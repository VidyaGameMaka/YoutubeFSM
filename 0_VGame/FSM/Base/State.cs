using Godot;
using System;

namespace FSM;
/// <summary>
/// State class defines state csontruction.
/// Please note that the namespace "FSM.Base" is implied but is not used as part of the namespace.
/// </summary>
public abstract partial class State<TActor> : Node
	where TActor : Node {
	public TActor Actor;
	public virtual void EnterState() { }
	public virtual void ExitState() { }
	public virtual void ProcessState(double delta) { }
	public virtual void PhysicsProcessState(double delta) { }
}
