using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField] private float MoveSpeed = 5f;
	[SerializeField] private float RotateSpeed = 10f;

	private GameInput gameInput;


	private bool isWalking;

	private void Awake() {
		gameInput = GetComponent<GameInput>();
	}

	private void Update() {

		Vector2 inputVector = gameInput.GetMovementVectorNormalized();
		Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
		transform.position += moveDir * MoveSpeed * Time.deltaTime;

		isWalking = inputVector != Vector2.zero;
		transform.forward = Vector3.Slerp(transform.forward, moveDir, RotateSpeed * Time.deltaTime);

		Debug.Log(inputVector);
	}

	public bool IsWalking() {
		return isWalking;
	}
}
