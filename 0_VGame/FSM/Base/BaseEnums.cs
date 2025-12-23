/// <summary>
/// Enums used by every State Machine. These Enums are not part of the FSM system itself, but are provided
/// as a convenient starting point for defining common input and direction types.
/// Change the values here using your IDE's rename symbol function to match your project's input map.
/// </summary>
namespace FSM;

public enum InputAxisNames {
	up,
	down,
	left,
	right
}

public enum InputButtonNames {
	attack,
	jump
}

public enum FacingDirections {
	left,
	right
}
