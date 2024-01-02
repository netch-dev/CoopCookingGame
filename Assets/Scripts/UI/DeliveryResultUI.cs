using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour {
	private const string POPUP_TRIGGER = "Popup";

	[SerializeField] private Image backgroundImage;
	[SerializeField] private Image iconImage;
	[SerializeField] private TextMeshProUGUI messageText;

	[SerializeField] private Color successColor;
	[SerializeField] private Color failedColor;
	[SerializeField] private Sprite successSprite;
	[SerializeField] private Sprite failedSprite;

	private Animator animator;

	private void Awake() {
		animator = GetComponent<Animator>();
	}
	private void Start() {
		DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
		DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

		gameObject.SetActive(false);
	}

	private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e) {
		gameObject.SetActive(true);
		backgroundImage.color = successColor;
		iconImage.sprite = successSprite;
		messageText.text = "DELIVERY\nSUCCESS";
		animator.SetTrigger(POPUP_TRIGGER);
	}
	private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e) {
		gameObject.SetActive(true);
		backgroundImage.color = failedColor;
		iconImage.sprite = failedSprite;
		messageText.text = "DELIVERY\nFAILED";
		animator.SetTrigger(POPUP_TRIGGER);
	}
}
