using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private Button playButton;
	[SerializeField] private Button quitButton;

	private void Awake() {
		playButton.onClick.AddListener(() => {
			Loader.Load(Loader.Scene.GameScene);
		});
		quitButton.onClick.AddListener(() => {
			Application.Quit();
		});
		playButton.Select();

		// Reset the time scale in case the player quit the game while it was paused
		Time.timeScale = 1f;
	}
}
