public static class Loader {

	public enum Scene {
		MainMenuScene,
		GameScene,
		LoadingScene
	}
	private static Scene targetScene;

	public static void Load(Scene targetScene) {
		Loader.targetScene = targetScene;

		UnityEngine.SceneManagement.SceneManager.LoadScene(Scene.LoadingScene.ToString());
	}

	public static void LoaderCallback() {
		UnityEngine.SceneManagement.SceneManager.LoadScene(targetScene.ToString());
	}
}
