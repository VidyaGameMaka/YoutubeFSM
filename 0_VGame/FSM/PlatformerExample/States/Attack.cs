using Godot;

namespace FSM.PlatformerExample;

public partial class Attack : State<PlatformerController> {

	public async override void EnterState() {
		// Play animation on state entry
		Actor.PlayAnimation(GetType().Name.ToString());

		// Spawn a bullet
		Actor.SpawnBullet();

		// Simulate attack duration
		double lifeTime = 0.5;
		await ToSignal(Actor.GetTree().CreateTimer(lifeTime), "timeout");

		// Return to idle after attacking
		Actor.GetStateMachine().ChangeState<Idle>();
	}

	public override void ProcessState(double delta) { }

	public override void PhysicsProcessState(double delta) { }

	public override void ExitState() { }

}
