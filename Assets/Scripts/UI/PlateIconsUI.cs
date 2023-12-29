using UnityEngine;

public class PlateIconsUI : MonoBehaviour {
	[SerializeField] private PlateKitchenObject plateKitchenObject;
	[SerializeField] private GameObject iconTemplate;

	private void Awake() {
		iconTemplate.SetActive(false);
	}
	private void Start() {
		plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
	}

	private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
		UpdateVisual();
	}

	private void UpdateVisual() {
		Debug.Log($"Destroying {transform.childCount - 1} children and spawning {plateKitchenObject.GetKitchenObjectSOList().Count} icons");
		foreach (Transform child in transform) {
			if (child.gameObject == iconTemplate) continue;
			Debug.Log($"Destroying {child.gameObject.name}");
			Destroy(child.gameObject);
		}

		foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
			GameObject icon = Instantiate(iconTemplate, transform);
			icon.SetActive(true);
			icon.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
		}
	}
}
