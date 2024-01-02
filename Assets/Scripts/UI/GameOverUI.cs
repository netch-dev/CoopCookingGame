using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI recipesDeliveredText;
	[SerializeField] private Button retryButton;
	[SerializeField] private Button menuButton;

	private void Start() {
		KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
		retryButton.onClick.AddListener(() => {
			Loader.Load(Loader.Scene.GameScene);
		});
		menuButton.onClick.AddListener(() => {
			Loader.Load(Loader.Scene.MainMenuScene);
		});


		Hide();
	}

	private void KitchenGameManager_OnStateChanged(object sender, EventArgs e) {
		if (KitchenGameManager.Instance.IsGameOver()) {
			Show();
			recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
		} else {
			Hide();
		}
	}

	private void Show() {
		gameObject.SetActive(true);
		retryButton.Select();
	}

	private void Hide() {
		gameObject.SetActive(false);
	}
}
