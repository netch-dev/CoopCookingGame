using UnityEngine;

public class LootAtCamera : MonoBehaviour {

	private enum Mode {
		LookAt,
		LootAtInverted,
		CameraForward,
		CameraForwardInverted
	}
	[SerializeField] private Mode mode;
	private void LateUpdate() {
		switch (mode) {
			case Mode.LookAt:
				transform.LookAt(Camera.main.transform);
			break;

			case Mode.LootAtInverted:
				transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
			break;

			case Mode.CameraForward:
				transform.forward = Camera.main.transform.forward;
			break;

			case Mode.CameraForwardInverted:
				transform.forward = -Camera.main.transform.forward;
			break;
		}
	}
}
