using System;
using UnityEngine;

public class GameInput : MonoBehaviour {
	public event EventHandler OnInteract;

	private PlayerInputActions playerInputActions;
	private void Awake() {
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();

		playerInputActions.Player.Interact.performed += Interact_Performed;
	}

	private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		OnInteract?.Invoke(this, EventArgs.Empty);
	}

	public Vector2 GetMovementVectorNormalized() {
		Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

		inputVector.Normalize();
		return inputVector;
	}
}
