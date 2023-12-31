using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
	public static GameInput Instance { get; private set; }

	public event EventHandler OnInteract;
	public event EventHandler OnInteractAlternate;
	public event EventHandler OnPauseAction;

	private PlayerInputActions playerInputActions;
	private void Awake() {
		Instance = this;

		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();

		playerInputActions.Player.Interact.performed += Interact_Performed;
		playerInputActions.Player.InteractAlternate.performed += InteractAlternate_Performed;
		playerInputActions.Player.Pause.performed += Pause_Performed;
	}

	private void OnDestroy() {
		playerInputActions.Player.Interact.performed -= Interact_Performed;
		playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_Performed;
		playerInputActions.Player.Pause.performed -= Pause_Performed;

		playerInputActions.Dispose();
	}

	private void Pause_Performed(InputAction.CallbackContext context) {
		OnPauseAction?.Invoke(this, EventArgs.Empty);
	}

	private void InteractAlternate_Performed(InputAction.CallbackContext context) {
		OnInteractAlternate?.Invoke(this, EventArgs.Empty);
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
