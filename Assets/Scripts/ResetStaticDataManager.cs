using UnityEngine;


// public static events for classes don't clear themselves when the scene changes so we have to reset them manually
// example: public static event EventHandler OnAnyCut;

public class ResetStaticDataManager : MonoBehaviour {
	private void Awake() {
		CuttingCounter.ResetStaticData();
		BaseCounter.ResetStaticData();
		TrashCounter.ResetStaticData();
	}
}
