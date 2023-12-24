using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
	
	[SerializeField] private CuttingCounter cuttingCounter;
	[SerializeField] private Image barImage;

	private void Start() {
		cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
		barImage.fillAmount = 0;

		ToggleBar(enable: false);
	}

	private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e) {
		barImage.fillAmount = e.progressNormalized;

		if (e.progressNormalized == 0 || e.progressNormalized == 1) {
			ToggleBar(enable: false);

		} else ToggleBar(enable: true);
	}

	public void ToggleBar(bool enable) {
		gameObject.SetActive(enable);
	}
}
