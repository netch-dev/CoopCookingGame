using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
	
	[SerializeField] private GameObject hasProgressGameObject;
	[SerializeField] private Image barImage;

	private IHasProgress hasProgress;

	private void Start() {
		hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
		if (hasProgress == null) {
			Debug.LogError(hasProgressGameObject.name + " does not have a component that implements IHasProgress");
			return;
		}
		hasProgress.OnProgressChanged += CuttingCounter_OnProgressChanged;
		barImage.fillAmount = 0;

		ToggleBar(enable: false);
	}

	private void CuttingCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
		barImage.fillAmount = e.progressNormalized;

		if (e.progressNormalized == 0 || e.progressNormalized == 1) {
			ToggleBar(enable: false);
		} else ToggleBar(enable: true);
	}

	public void ToggleBar(bool enable) {
		gameObject.SetActive(enable);
	}
}
