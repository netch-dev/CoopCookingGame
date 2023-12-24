using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {

	[SerializeField] private BaseCounter baseCounter;
	[SerializeField] private GameObject[] visualGameObject;
	private void Start() {
		Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
	}

	private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
		if (e.selectedCounter == baseCounter) {
			ToggleVisual(enable: true);
		} else {
			ToggleVisual(enable: false);
		}
	}

	private void ToggleVisual(bool enable) {
		foreach (GameObject visualGO in visualGameObject) {
			visualGO.SetActive(enable);
		}
	}
}
