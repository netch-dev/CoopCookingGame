using System;
using UnityEngine;

public class Player : MonoBehaviour {
	public static Player Instance { get; private set; }
	
	public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
	public class OnSelectedCounterChangedEventArgs : EventArgs {
		public ClearCounter selectedCounter;
	}

	[SerializeField] private float MoveSpeed = 5f;
	[SerializeField] private float RotateSpeed = 10f;
	[SerializeField] private LayerMask CountersLayerMask;

	private GameInput gameInput;


	private bool isWalking;
	private Vector3 lastInteractDir;
	private ClearCounter selectedCounter;

	private void Awake() {
		gameInput = GetComponent<GameInput>();

		if (Instance != null) {
			Debug.LogError("There is more than one player instance");
		} else Instance = this;
	}

	private void Start() {
		gameInput.OnInteract += GameInput_OnInteract;
	}
	private void Update() {
		HandleMovement();
		HandleInteractions();
	}
	private void GameInput_OnInteract(object sender, System.EventArgs e) {
		if (selectedCounter != null) {
			selectedCounter.Interact();
			Debug.Log("Interact with counter");
		}
	}

	private void HandleInteractions() {
		Vector2 inputVector = gameInput.GetMovementVectorNormalized();
		Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

		if (moveDir != Vector3.zero) {
			lastInteractDir = moveDir;
		}

		float interactDistance = 2f;
		if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hit, interactDistance, CountersLayerMask)) {
			if (hit.transform.TryGetComponent(out ClearCounter clearCounter)) {
				// Has clear counter
				if (clearCounter != selectedCounter) {
					SetSelectedCounter(clearCounter);
				}
			} else {
				SetSelectedCounter(null);
			}
		} else {
			SetSelectedCounter(null);
		}

	}
	private void HandleMovement() {
		Vector2 inputVector = gameInput.GetMovementVectorNormalized();
		Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

		float moveDistance = MoveSpeed * Time.deltaTime;
		float playerRadius = 0.7f;
		float playerHeight = 2f;
		bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

		if (!canMove) {
			// Can't move
			// Attempt to slide along the wall, x movement first
			Vector3 slideDir = new Vector3(moveDir.x, 0f, 0f).normalized;
			canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, slideDir, moveDistance);

			if (canMove) {
				moveDir = slideDir;
			} else {
				// cannot move on the x

				// attempt to slide along the wall, Z movement
				slideDir = new Vector3(0f, 0f, moveDir.z).normalized;
				canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, slideDir, moveDistance);

				if (canMove) {
					moveDir = slideDir;
				} else {
					// cannot move in any direction
				}
			}
		}
		if (canMove) {
			transform.position += moveDir * MoveSpeed * Time.deltaTime;
		}

		isWalking = inputVector != Vector2.zero;

		transform.forward = Vector3.Slerp(transform.forward, moveDir, RotateSpeed * Time.deltaTime);
	}

	public bool IsWalking() {
		return isWalking;
	}

	private void SetSelectedCounter(ClearCounter newCounter) {
		selectedCounter = newCounter;
		OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
	}
}
