using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private float RotateSpeed = 10f;

    private bool isWalking;

    private void Update() {
        Vector3 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W)) {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x += 1;
        }

        inputVector.Normalize();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * MoveSpeed * Time.deltaTime;

        isWalking = inputVector != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, RotateSpeed * Time.deltaTime);

        Debug.Log(inputVector);
    }

    public bool IsWalking() {
        return isWalking;
    }
}
