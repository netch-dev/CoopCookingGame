using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {

	[SerializeField] private ClearCounter clearCounter;
	[SerializeField] private GameObject visualGameObject;
	private void Start() {
		Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
	}

	private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
		if (e.selectedCounter == clearCounter) {
			ToggleVisual(true);
		} else {
			ToggleVisual(false);
		}
	}

	private void ToggleVisual(bool enable) {
		visualGameObject.SetActive(enable);
	}
}
